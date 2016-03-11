using System.Collections.Generic;

namespace WebApplicationData.Movie
{
	public class MoviesInfo
	{
		public List<string> Actors { get; set; }
		public List<string> Writers { get; set; }
		public List<string> Directors { get; set; }
		public List<string> Genres { get; set; }
        //public List<string> AvailableUsers { get; set; }
		public List<string> Ratings { get; set; }
		public List<string> Titles { get; set; }
		public Range Length { get; set; }
        public Range Year { get; set; }
	    public List<string> Tags { get; set; }
	    public List<string> Characters { get; set; }
	}
}