using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Areas.Images.Models
{
    public class UploadImageModel
    {
        [Required]
        public string ImageURL { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public string ImageDescription { get; set; }

        public bool Success { get; set; }

        public bool NSFW { get; set; }

        public List<string> Tags { get; set; }

        public List<string> AllTags { get; set; } 

    }
}