using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace WebApplicationData.Movie
{
	public class MovieUserUpdateMoviesResult : BaseResult<List<MovieResult>>
	{
		List<MovieResult> Movies { get; set; }
	
	}

	
}