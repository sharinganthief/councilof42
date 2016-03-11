using System.Collections.Generic;
using WebApplicationData.Movie;

namespace WebApplication1.Areas.Movie.Models
{
	public class ManageUserMoviesModel
	{
		public List<MovieResult> MoviesList { get; set; }
		public List<MovieResult> UserMovies { get; set; }
	}
}