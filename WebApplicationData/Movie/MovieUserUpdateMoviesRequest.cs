using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationData.Movie
{
	public class MovieUserUpdateMoviesRequest
	{
		public List<int> Movies { get; set; }
		public Guid UserID { get; set; }
	}
}
