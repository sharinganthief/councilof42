using System;
using System.Collections.Generic;
using System.Web;
using HtmlAgilityPack;
using WebApplicationBusiness;

namespace WebApplication1.Areas.Comic.Models.Comic
{
	public class XkcdModel : BaseComicModel
	{
		public string ComicText { get; set; }
		private const string baseUrl = "http://xkcd.com/";

		public XkcdModel(string comicURL = null)
			: base(baseUrl, comicURL)
		{
			base.BuildAddComicModelToCache(this);
		}

		public override List<ComicPropertyArgs> GetPropertyArgs()
		{
			Type thisType = this.GetType();

			List<ComicPropertyArgs> comicPropArgs = new List<ComicPropertyArgs>();

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id=\"comic\"]/img",
				SecondaryHtmlNodePath = "//*[@id=\"comic\"]/a/img",
				HtmlNodeAction = (x => HttpUtility.HtmlDecode(x.GetAttributeValue("title", null))),
				PropInfo = thisType.GetProperty("ComicText")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id=\"comic\"]/img",
				SecondaryHtmlNodePath = "//*[@id=\"comic\"]/a/img",
				HtmlNodeAction = (x => x.GetAttributeValue("src", null)),
				PropInfo = thisType.GetProperty("ComicURL")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id=\"ctitle\"]",
				HtmlNodeAction = (x => x.InnerText),
				PropInfo = thisType.GetProperty("ComicTitle")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id=\"middleContainer\"]/ul/li/a[@rel=\"next\"]",
				HtmlNodeAction = (x => string.Format("{0}{1}", baseUrl, x.GetAttributeValue("href", null))),
				PropInfo = thisType.GetProperty("NextComicURL")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id=\"middleContainer\"]/ul/li/a[@rel=\"prev\"]",
				HtmlNodeAction = (x => string.Format("{0}{1}", baseUrl, x.GetAttributeValue("href", null))),
				PropInfo = thisType.GetProperty("PreviousComicURL")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id=\"middleContainer\"]/ul/li/a[text()=\"Random\"]",
				HtmlNodeAction = (x => string.Format("{0}{1}", baseUrl, x.GetAttributeValue("href", null))),
				PropInfo = thisType.GetProperty("RandomComicUrl")
			});

			return comicPropArgs;
		}
	}
}