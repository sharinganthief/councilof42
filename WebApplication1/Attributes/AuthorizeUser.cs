using System;
using System.Collections.Generic;
using WebApplicationData;

namespace WebApplication1.Attributes
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class AuthorizeUser : BaseAuthorizeAttribute
	{
		private readonly List<string> _allowedRoles = new List<string>(){Settings.UserRole};
											

		public override List<string> AllowedRoles
		{
			get
			{
				return _allowedRoles;
			}
		}
	}
}