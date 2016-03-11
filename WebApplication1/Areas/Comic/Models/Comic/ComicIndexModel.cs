using System.Collections.Generic;
using WebApplicationData.Comic;

namespace WebApplication1.Areas.Comic.Models.Comic
{
	public class ComicIndexModel
	{
		public Dictionary<string, ComicInfo> ActionsList { get; set; }
	}
}