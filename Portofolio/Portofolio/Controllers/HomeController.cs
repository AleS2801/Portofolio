using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Portofolio.Models;
using Microsoft.AspNetCore.Hosting;

namespace Portofolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment, AppDbContext appDbContext)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Latest()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UploadFiles()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadFiles(UploadFilesModel item)
        {
            if (item.Path.Length > 0)
            {
                var path = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                var filePath = Path.Combine(path, item.Path.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    item.Path.CopyTo(stream);
                }

                Image image = new Image();
                image.Title = item.Title;
                image.Description = item.Description;
                image.Other = item.Other;
                image.Path = filePath;

                _appDbContext.Add(image);
                _appDbContext.SaveChanges();
            }


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

/* View Bag!!!!
 * StronglyTight View
 * View Models - pt clasa + clasa
 * View IMports -namespace + tagHelper
 * librarii
 * Code First DB:
 *  adaug librarii nuGet Sql Serer, Tools
 *  StartUp ServiceAdd DBContext  in void ConfigurSerice 
 *  add migration nume - pt orice modif in baza de date
 *  update database. - pt fiecare modificare
 *  create new class  dervivata din DbContext
 *  constructor speical obligatoriu cu ..
 *  
 *  ctrl+. error help 
 *  
 *  
 *  
 *  pt tabela din bd => Add new class care are numele tabelei
 *  rooting e imp se gaseste in startup legat de patern de url pot fi mai mule
 *  htmlActionLink
 *  tag helper - cele cdin  <a asp-controller=.. asp-action> ami este si asp-root/asp-root-id/asp-root-NUMEPATH
 *  partial view se  foloseste cu partial nume se pune in shared
 *      shared add reazor view
 *      stergi info 
 *      bagi ceva
 *      la inc sa fie @{ModelCore...}
 *      acolo unde sa se afle: <partial name="Nume" model="@Model"...>
 * session:
 *      tine minte de la un req la altul
 * view cmponents:
 *      pt continut partial - mai complex
 *      se fol de dependence injection
 *      ex: form de login
 *      e o cls deriv din viewcomponent
 *      are met invoke
 *      se face in viewImports
 * alte tag helpers: 
 *      pot fi create de tine
 *      ex pt email
 *      clasa care sa derie din TagHelper
 *      folder nou tagHelper
 * forms:
 *      se fol de tag helper ex pt submit e de tip POST
 *      Get din url, Post transmiti info
 *      inputs ai tag helpers: asp-for
 *      data binding exemplu in create de la proiectu 1
 *      vaidation
 *              se folosesc attributes: ex [required]
 *              poti da place holder [DIsplayName...]
 *       identity:
 *              Autentificare in pagina
 *              e o chestie implicita
 *              >Microsoft.AspNetCore.Identity.Entity Framework
 *              >Microsoft.AspNetCore.Identity.Ui
 *              >in DbContext in loc sa fie deriv din DbContext sa derive din Identity...DbContext
 *              >dupa pag de registerin startup dupa rooting use Authentification
 *              add migration nume[ex identity] 
 *              updte database
 *              pt a crea pag de register si logare new scaffold
 *              Add > new scaffold> identity>identity
 *                >log in  log out  register
 *              sa foloseasca contxtul tau
 *              in startup la config services trebuie adaugat ceva ca sa faca legatura cu acele pagini
 *              
 *              la inceputul namespace-ului in controller pui [Authorise]
 *              pt o metoda fara logare: [AllowAnonymus]
 *              [Authorise] se poate pune pe o anumita actiune
 *              se pot suprascrie
 *              
 *          -----PE MAIL VEZI TOT -----
 *                  
 */

/* @ Render section
* Procedura stocata pt baza de date - mai mult pt database first
* Trigger
* with no lock - sql hint pt select care ae update in curs
* [Column("nume"] inainte de coloana care vrem sa apara altfel
* [Column('typename' = varchar..]
* [MaxLwngth(300)]
* [Required]
* 
* interogare baza de date:
*      2 metode: linq to entities (1)
*                (2)
*          (2) where, order by, select..AsNotTracking,Joins,FromSqlRow,...
* 
* Async & Await
*      public Async Task...
*      {
*          return Await ...
*      }
*      
*      optimizare - sql profiler se inregistreaza tot "ce misca"  Tipul Tuneing
*                 - in special pentru tranzactii
*                 
* Save Picture
*      folderul images trebuie pus in wwwroot
* 
* 
* conext e legatura dintre cod si bd
*/
