using System.Collections.Generic;

namespace WebApplicationData.Movie
{
	public class MovieAddRestResult
	{
		public bool Suceeded { get; set; }
		public List<string> Errors { get; set; }
	}
}