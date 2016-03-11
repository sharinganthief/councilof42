using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationData.Image
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string ImageName { get; set; }
        public string ImageDescription { get; set; }
        public bool NSFW { get; set; }

        public List<string> Tags { get; set; }
    }
}
