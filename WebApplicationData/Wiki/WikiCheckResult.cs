using System.Collections.Generic;

namespace WebApplicationData.Wiki
{
	public class WikiCheckResult
	{
		public bool Succeeded { get; set; }
		public List<string> Error { get; set; }
		public string OriginalURL { get; set; }
		public List<string> HopList { get; set; }
		public int Hops { get; set; }
	}
}