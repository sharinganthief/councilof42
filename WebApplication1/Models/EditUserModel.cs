using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Models
{
	public class EditUserModel
	{
		public ApplicationUser User { get; set; }
		public List<RoleDTO> Roles { get; set; }
		public List<RoleDTO> UserRoles { get; set; }

		public PostedRoles PostedRoles { get; set; }
	}

	public class RoleDTO
	{
		public string ID { get; set; }
		public string Name { get; set; }
	}

	public class PostedRoles
	{
		public string[] RoleIDs { get; set; }
	}
}

