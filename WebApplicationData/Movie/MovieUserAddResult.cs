using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace WebApplicationData.Movie
{
	public class MovieUserAddResult : BaseResult<Guid>
	{
		public Guid MovieAPIUserID { get; set; }
		public List<string> Errors { get; set; }
		public bool Successful
		{
			get { return this.Errors == null || !this.Errors.Any(); }
		}
	
	}

	
}