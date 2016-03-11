using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Helpers
{
	public static class RestHelpers
	{

		private static IRestResponse<T> WebCall<T>(Method method, string url, Dictionary<string,string> headers = null, object dataToPost = null) where T : class, new()
		{
			RestClient client = new RestClient();
			RestRequest request = new RestRequest(url, method);
			
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "application/json");

			if (headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					request.AddHeader(keyValuePair.Key, keyValuePair.Value);
				}
			}

			request.RequestFormat = DataFormat.Json;

			if (dataToPost != null)
			{
				request.AddBody(dataToPost);
			}
			
			IRestResponse<T> response2 = client.Execute<T>(request);
			//T name = response2.Data;
			return response2;
		}

		public static IRestResponse<T> Get<T>(string url) where T: class, new()
		{
			return WebCall<T>(Method.GET, url);
		}
		public static IRestResponse<T> Post<T>(string url, object dataToPost) where T: class, new()
		{
			return WebCall<T>(Method.POST, url, null, dataToPost);
		}

		public static IRestResponse<T> Get<T>(string url, Dictionary<string,string> headers) where T: class, new()
		{
			return WebCall<T>(Method.GET, url, headers);
		}
		public static IRestResponse<T> Post<T>(string url, object dataToPost, Dictionary<string,string> headers) where T: class, new()
		{
			return WebCall<T>(Method.POST, url, headers, dataToPost);
		}

	}
}
