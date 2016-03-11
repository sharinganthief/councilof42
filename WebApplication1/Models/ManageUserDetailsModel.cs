using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebApplicationData.WebApplication;

namespace WebApplication1.Models
{
	public class ManageUserDetailsModel
	{
		public ManageUserDetailsModel()
		{
			
		}
		public UserInfo UserInfo { get; set; }
	}
}