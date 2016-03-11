using System;
using System.Collections.Generic;
using System.Web.Helpers;

namespace WebApplicationData.Movie
{
	public class SearchCriteria
	{
		public List<string> Actors { get; set; }
		public List<string> Writers { get; set; }
		public List<string> Directors { get; set; }
		public List<string> Genres { get; set; }
		//public List<string> Available_Users { get; set; }
		public List<string> Ratings { get; set; }
		public List<string> Plot_Keywords { get; set; }
		public List<string> Characters { get; set; }
		public int MinLength { get; set; }
		public int MaxLength { get; set; }
		public int MinYear { get; set; }
		public int MaxYear { get; set; }
		public Guid UserID { get; set; }
		public bool Randomize { get; set; }
	}
}