using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationData
{
	public static class Settings
	{
		public static readonly string UserRole = ConfigurationManager.AppSettings["Role.User"];
		public static readonly string FriendRole = ConfigurationManager.AppSettings["Role.Friend"];
		public static readonly string MovieAdminRole = ConfigurationManager.AppSettings["Role.MovieAdmin"];
		public static readonly string DeveloperRole = ConfigurationManager.AppSettings["Role.Developer"];
		public static readonly string ImageCollectionBaseUrlForFormat = ConfigurationManager.AppSettings["ImageCollectionBaseUrlForFormat"];

	    private static string imageString = ConfigurationManager.AppSettings["ImagePerPage"];

	    public static readonly int ImagePerPage = int.Parse(string.IsNullOrWhiteSpace(imageString) ? "40" : imageString);
	}
}

