using WebApplicationData.Wiki;

namespace WebApplication1.Areas.Projects.Models
{
	public class WikiModel
	{
		public bool GetRandom { get; set; }
		public string WikipediaPageURL { get; set; }

		public WikiCheckResult Result { get; set; }
	}
}