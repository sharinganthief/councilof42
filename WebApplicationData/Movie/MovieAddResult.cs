using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace WebApplicationData.Movie
{
	public class MovieAddResult : BaseResult<MovieAddRestResult>
	{
		public List<string> Errors { get; set; }
		public bool Successful
		{
			get { return this.Errors == null || !this.Errors.Any(); }
		}
	
	}
}