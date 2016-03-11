using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using WebApplicationData;

namespace WebApplication1.Attributes
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public abstract class BaseAuthorizeAttribute : AuthorizeAttribute
	{
		/// <summary>
		/// Pull the allowed roles for this attribute from the current config 
		/// </summary>
		//public List<string> AllowedRoles { get; set; }
		public abstract List<string> AllowedRoles { get; }

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			base.OnAuthorization(filterContext);

			if (AllowedRoles == null)
			{
				throw new ApplicationException("AllowedRoles was not set in inherited class");
			}
			
			IPrincipal user = filterContext.HttpContext.User;

			if (AllowedRoles.Any(user.IsInRole)) return;

			if (user.IsInRole(Settings.DeveloperRole)) return;

			// If we haven't returned yet, the user doesn't have the required privileges.
			filterContext.Result = new HttpUnauthorizedResult();
		}

		public static List<string> GetRolesList(params string[] roleStrings)
		{
			if (roleStrings == null)
			{
				throw new ArgumentNullException("roleStrings", "GetRolesList requires an array of roles to retrieve");
			}

			return roleStrings.Select(s => (ConfigurationManager.AppSettings[string.Format("Role.{0}", s)]).ToLower()).ToList();
		}
		
		public static List<string> GetAllRolesList()
		{
			string[] allRoleKeys = ConfigurationManager.AppSettings.AllKeys.Where(o => o.Contains("Role.")).Select(o => o.Split('.').Last()).ToArray();

			return GetRolesList(allRoleKeys);
		}
	}
}