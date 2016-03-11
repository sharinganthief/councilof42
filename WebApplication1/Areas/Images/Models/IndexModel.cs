using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationData.Image;

namespace WebApplication1.Areas.Images.Models
{
    public class IndexModel
    {
        public List<ImageDto> Images { get; set; }

        public List<string> AllTags { get; set; }
        public List<string> SelectedTags { get; set; }
    }
}