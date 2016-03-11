using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using HtmlAgilityPack;
using Microsoft.Ajax.Utilities;
using WebApplicationBusiness;

namespace WebApplication1.Areas.Comic.Models.Comic
{
	public class SmbcModel : BaseComicModel
	{
		public string AfterComicURL { get; set; }

		private const string baseUrl = "http://www.smbc-comics.com/";

		public SmbcModel(string comicURL = null)
			: base(baseUrl, comicURL)
		{
			SmbcModel model = Cb.GetComicModelFromCache<SmbcModel>(comicURL);

			if (model != null)
			{
				Helpers.ReflectionHelpers.SetPropertyValues<SmbcModel>(this, model);
				return;
			}

			base.BuildAddComicModelToCache(this, "?#comic");
		}

		public SmbcModel()
		{
		}

		public override List<ComicPropertyArgs> GetPropertyArgs()
		{
			Type thisType = this.GetType();

			List<ComicPropertyArgs> comicPropArgs = new List<ComicPropertyArgs>();

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id='comicbody']//img[1]",
				HtmlNodeAction = (x => string.Format("{0}{1}", baseUrl, x.GetAttributeValue("src", null))),
				PropInfo = thisType.GetProperty("ComicURL")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id='aftercomic']/img",
				HtmlNodeAction = (x => x.GetAttributeValue("src", null)),
				PropInfo = thisType.GetProperty("AfterComicURL")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@class='prev']",
				HtmlNodeAction = (x => string.Format("{0}{1}", baseUrl, x.GetAttributeValue("href", null))),
				PropInfo = thisType.GetProperty("PreviousComicURL")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@class='next']",
				HtmlNodeAction = (x => string.Format("{0}{1}", baseUrl, x.GetAttributeValue("href", null))),
				PropInfo = thisType.GetProperty("NextComicURL")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@class='random']",
				HtmlNodeAction = (x => string.Format("{0}{1}", baseUrl, x.GetAttributeValue("href", null))),
				PropInfo = thisType.GetProperty("RandomComicUrl")
			});

			return comicPropArgs;
		}

		public override string GetCacheKey()
		{
			return string.IsNullOrWhiteSpace(this.ComicURL) ? this.GetType().Name : this.ComicURL;
		}
	}
}