using System;
using System.Reflection;
using System.Web;
using HtmlAgilityPack;
using Microsoft.Ajax.Utilities;
using WebApplicationBusiness;

namespace WebApplication1.Areas.Comic.Models.Comic
{
	public class SmbcModel : BaseComicModel
	{
		private const string BaseUrl = "http://www.smbc-comics.com/";
		public string AfterComicURL { get; set; }


		public SmbcModel(string comicURL = null)
		{
			ComicBusiness cb = new ComicBusiness();

			if (!string.IsNullOrWhiteSpace(comicURL))
			{
				PopulateFromUrl(cb, BaseUrl, comicURL, true);
			}

			SmbcModel model = cb.GetComicModelFromCache<SmbcModel>(comicURL);

			if (model != null)
			{
				//this.ComicTitle = null;
				//this.ComicURL = model.ComicURL;
				//this.AfterComicURL = model.AfterComicURL;
				//this.PreviousComicURL = model
				Helpers.ReflectionHelpers.SetPropertyValues(this, model);

				return;
			}


			PopulateFromUrl(cb, BaseUrl, "?#comic");



		}

		public void PopulateFromUrl(ComicBusiness cb, string baseUrl, string comicUrl, bool isComicUrl = false)
		{
			string url = comicUrl;

			if (!isComicUrl)
			{
				url = string.Format("{0}{1}", baseUrl, comicUrl);
			}

			HtmlDocument doc = new HtmlWeb().Load(url);

			HtmlNode imgTag = doc.DocumentNode.SelectSingleNode("//*[@id='comicimage']/img");

			if (imgTag == null)
			{
				return;
			}

			this.ComicURL = imgTag.GetAttributeValue("src", null);

			// After comic population
			HtmlNode afterComic = doc.DocumentNode.SelectSingleNode("//*[@id='aftercomic']/img");

			//If we don't have one, fuck it. move on
			if (afterComic != null)
			{
				this.AfterComicURL = afterComic.GetAttributeValue("src", null);
			}

			HtmlNode prevTag = doc.DocumentNode.SelectSingleNode("//*[@class='backRollover']");

			if (prevTag != null)
			{
				this.PreviousComicURL = string.Format("{0}{1}", baseUrl, prevTag.GetAttributeValue("href", null));

			}

			HtmlNode nextTag = doc.DocumentNode.SelectSingleNode("//*[@class='nextRollover']");

			if (nextTag != null)
			{
				this.NextComicURL = string.Format("{0}{1}", baseUrl, nextTag.GetAttributeValue("href", null));

			}

			if (isComicUrl)
			{
				cb.AddComicToCache(this, comicUrl);
				return;
			}

			cb.AddComicToCache(this);
		}


	}
}