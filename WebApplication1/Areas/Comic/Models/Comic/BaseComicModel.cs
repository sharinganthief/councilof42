using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;
using WebApplicationBusiness;

namespace WebApplication1.Areas.Comic.Models.Comic
{
	public abstract class BaseComicModel
	{
		public abstract List<ComicPropertyArgs> GetPropertyArgs();

		public virtual string GetCacheKey()
		{
			return this.GetType().Name;
		}
		public string BaseUrl { get; set; }
		public string SpecificComicURL { get; set; }
		public ComicBusiness Cb { get; set; }

		protected BaseComicModel(string baseUrl, string comicURL = null)
		{
			Cb = new ComicBusiness();

			this.BaseUrl = baseUrl;

			string cleanComicUrl = comicURL;

			if (!string.IsNullOrWhiteSpace(comicURL))
			{
				cleanComicUrl = comicURL.Trim(new[] { '\'' }).Trim();
			}

			this.SpecificComicURL = cleanComicUrl;
		}

		protected BaseComicModel()
		{

		}

		public void BuildAddComicModelToCache<T>(T obj, string url = null) where T : BaseComicModel
		{
			if (obj == null) return;
			if (string.IsNullOrWhiteSpace(this.BaseUrl)) return;

			List<ComicPropertyArgs> comicPropArgs = GetPropertyArgs();

			if (comicPropArgs == null || !comicPropArgs.Any()) return;


			string urlToUse = this.SpecificComicURL;

			bool isComicUrl = !string.IsNullOrWhiteSpace(urlToUse);

			if (!isComicUrl)
			{
				urlToUse = string.Format("{0}{1}", this.BaseUrl, url);
			}

			string finalUrl = CheckForRandom(urlToUse);

			HtmlDocument doc = new HtmlWeb().Load(finalUrl);

			foreach (ComicPropertyArgs comicPropertyArg in comicPropArgs)
			{
				HtmlNode htmlNode = doc.DocumentNode.SelectSingleNode(comicPropertyArg.HtmlNodePath);

				if (htmlNode == null)
				{
					if (!string.IsNullOrWhiteSpace(comicPropertyArg.SecondaryHtmlNodePath))
						htmlNode = doc.DocumentNode.SelectSingleNode(comicPropertyArg.SecondaryHtmlNodePath);

					if (htmlNode == null) continue;
				}

				string value = comicPropertyArg.HtmlNodeAction(htmlNode);
				comicPropertyArg.PropInfo.SetValue(obj, value);
			}


			Cb.AddComicToCache(this, GetCacheKey());
		}

		public virtual string CheckForRandom(string urlToUse)
		{
			return urlToUse;
		}

		//public string Layout { get; set; }

		public string ComicURL { get; set; }
		public string ComicTitle { get; set; }

		public string NextComicURL { get; set; }

		public string PreviousComicURL { get; set; }

		public string RandomComicUrl { get; set; }
	}
}