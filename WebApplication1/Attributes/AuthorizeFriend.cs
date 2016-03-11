using System;
using System.Collections.Generic;
using WebApplicationData;

namespace WebApplication1.Attributes
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class AuthorizeFriend : BaseAuthorizeAttribute
	{
		private readonly List<string> _allowedRoles = new List<string>(){Settings.FriendRole};
											

		public override List<string> AllowedRoles
		{
			get
			{
				return _allowedRoles;
			}
		}
	}
}