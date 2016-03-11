using System.Linq;

namespace WebApplicationData.WebApplication
{
	public class UserInformationRepository
	{
		public static UserInformation GetUserInformation(string user)
		{
			using (WebApplication1Entities entities = new WebApplication1Entities())
	        {
		        UserInformation info = entities.UserInformations.FirstOrDefault(o => o.UserId == user);
		        return info;
	        }
		}

		public static void UpdateUserInformation(UserInformation userInformation)
		{


			using (WebApplication1Entities entities = new WebApplication1Entities())
			{
				entities.UserInformations.Attach(userInformation);
				entities.Entry(userInformation).Property(o => o.SerializedUserInfo).IsModified = true;
				entities.SaveChanges();
			}
		}
	}
}
