using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationData
{
	public class BaseResult<T>
	{
		public HttpStatusCode StatusCode { get; set; }
		public T Data { get; set; }
	}
}
