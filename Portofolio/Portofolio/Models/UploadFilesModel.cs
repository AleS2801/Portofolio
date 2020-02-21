using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portofolio.Models
{
    public class UploadFilesModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Path { get; set; }
        public string Other { get; set; }
    }
}
