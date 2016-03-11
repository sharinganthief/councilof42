using System;
using System.Collections.Generic;
using WebApplicationData.Movie;

namespace WebApplication1.Areas.Movie.Models
{
	public class SearchModel
	{
		public List<string> Titles { get; set; }

		//public List<string> Actors { get; set; }
		//public List<string> Directors { get; set; }
		//public List<string> Ratings { get; set; }
		//public List<string> AvailableUsers { get; set; }
		//public List<string> Genres { get; set; }
		//public List<string> Writers { get; set; }

		public List<MovieCategory> Categories { get; set; }
		public Range Length { get; set; }
	    public Range Year { get; set; }

		public Guid MovieUserID { get; set; }
	}
}