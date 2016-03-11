using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationData;
using WebApplicationData.Wiki;

namespace WebApplicationBusiness
{
	public class WikiBusiness
	{
		private WikiRepository wikiRepo = new WikiRepository();
		
		public WikiCheckResult GetCheckResult( string url)
		{
			WikiCheckResult result = wikiRepo.GetCheckResult(url);
		    return result;
		}
	}
}
