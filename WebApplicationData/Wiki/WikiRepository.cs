using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationData.Wiki
{
	public class WikiRepository : BaseRepository
	{
		public override string BaseURLForFormat
		{
			get { return _baseMovieUrlForFormat; }
		}

		private readonly string _baseMovieUrlForFormat = ConfigurationManager.AppSettings["baseWikiUrlForFormat"] ??
		                                                "http://wiki.local/api/wiki/{0}";


		public WikiCheckResult GetCheckResult(string url)
		{
			WikiCheckResult result = Post<WikiCheckResult>("CheckWikiPage", url).Data;
			return result;
		}
	}
}
