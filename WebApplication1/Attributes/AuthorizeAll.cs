using System;
using System.Collections.Generic;

namespace WebApplication1.Attributes
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class AuthorizeAll : BaseAuthorizeAttribute
	{
		private readonly List<string> _allowedRoles = GetAllRolesList();
											

		public override List<string> AllowedRoles
		{
			get
			{
				return _allowedRoles;
			}
		}
	}
}