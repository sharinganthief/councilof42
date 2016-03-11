using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;

namespace WebApplicationData.WebApplication
{
	public class UserInfo
	{
		public int Id { get; set; }
        public string UserId { get; set; }
        public System.Guid ExternalID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

		public AdditionalProperties AdditionalProperties { get; set; }

		public UserInfo()
		{
			
		}

		public UserInfo(UserInformation userInfo)
		{
			this.Id = userInfo.Id;
			this.UserId = userInfo.UserId;
			this.ExternalID = userInfo.ExternalID;
			this.FirstName = userInfo.FirstName;
			this.LastName = userInfo.LastName;

			AdditionalProperties props = JsonConvert.DeserializeObject < AdditionalProperties > (userInfo.SerializedUserInfo) ?? new AdditionalProperties();
			this.AdditionalProperties = props;
		}
	}

	public class AdditionalProperties
	{
		public bool NSFW { get; set; }

		public Guid MovieAPIUserID { get; set; }
	}
}
