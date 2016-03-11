using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using RestSharp;

namespace WebApplicationData
{
	public abstract class BaseRepository
	{
		public abstract string BaseURLForFormat { get; }
		private string FormatUrl(string requestPath)
		{
			if(string.IsNullOrWhiteSpace(BaseURLForFormat))
				throw new ApplicationException("Error with base URL: base URL was empty");
			try
			{
				return string.Format(BaseURLForFormat, requestPath);
			}
			catch (Exception e)
			{
				throw new ApplicationException(string.Format("Error with URL format: {0}", e.Message));
			}
			
		}


		public BaseResult<T> Get<T>(string requestPath) where T : class, new()
		{
			return this.Get<BaseResult<T>, T>(requestPath);
		}

		public BaseResult<T> Post<T>(string requestPath, object dataToPost) where T : class, new()
		{
			return this.Post<BaseResult<T>, T>(requestPath, dataToPost);
		}

		public BaseResult<T> Get<T>(string requestPath, Dictionary<string,string> headers) where T : class, new()
		{
			return this.Get<BaseResult<T>, T>(requestPath, headers);
		}

		public BaseResult<T> Post<T>(string requestPath, object dataToPost, Dictionary<string,string> headers) where T : class, new()
		{
			return this.Post<BaseResult<T>, T>(requestPath, dataToPost);
		}
		

		private T Get<T,TK>(string requestPath, Dictionary<string,string> headers = null) 
			where TK : class, new()
			where T : BaseResult<TK>, new()
		{

			string url = FormatUrl(requestPath);
			IRestResponse<TK> response = RestHelpers.Get<TK>(url, headers);
			TK returnVal = (TK) response.Data;
			T result = new T() { StatusCode = response.StatusCode, Data = returnVal};
			return result;
		}

		private T Post<T,TK>(string requestPath, object dataToPost, Dictionary<string,string> headers = null)
			where TK : class, new()
			where T : BaseResult<TK>, new()
		{
			string url = FormatUrl(requestPath);
			IRestResponse<TK> response = RestHelpers.Post<TK>(url, dataToPost, headers);
			TK returnVal = (TK) response.Data;
			T result = new T() { StatusCode = response.StatusCode, Data = returnVal};
			return result;
		}
	}
}
