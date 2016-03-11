using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using WebApplicationBusiness;

namespace WebApplication1.Areas.Comic.Models.Comic
{
	public class BlastWaveModel : BaseComicModel
	{
		private const string baseUrl = "http://www.blastwave-comic.com/";
		private static readonly string baseComicURL = string.Format("{0}{1}", baseUrl, "/index.php?p=comic&nro={0}");

		public BlastWaveModel(string comicURL = null)
			: base(baseUrl, string.IsNullOrWhiteSpace(comicURL) ? null : string.Format(baseComicURL, comicURL))
		{
			BlastWaveModel model = Cb.GetComicModelFromCache<BlastWaveModel>(string.Format(baseComicURL, comicURL));

			if (model != null)
			{
				Helpers.ReflectionHelpers.SetPropertyValues<BlastWaveModel>(this, model);
				return;
			}

			base.BuildAddComicModelToCache(this);
		}

		public override List<ComicPropertyArgs> GetPropertyArgs()
		{
			Type thisType = this.GetType();

			List<ComicPropertyArgs> comicPropArgs = new List<ComicPropertyArgs>();

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id='comic_ruutu']/center/img",
				HtmlNodeAction = (x => x.GetAttributeValue("src", null).Replace("./", "http://www.blastwave-comic.com/")),
				PropInfo = thisType.GetProperty("ComicURL")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id='comic_ruutu']/center/div[@class='comic_title']",
				HtmlNodeAction = (x => x.InnerText),
				PropInfo = thisType.GetProperty("ComicTitle")
			});

			Regex reg = new Regex(pattern: @".*nro=(\d+)");
			Func<HtmlNode, string> prevNextFunc =
				(x =>
					reg.Match(x.ParentNode.GetAttributeValue("href", null)).Groups[1].Value);

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id='comic_ruutu']/center//a/img[starts-with(@src, 'images/page/default/next')]",
				HtmlNodeAction = prevNextFunc,
				PropInfo = thisType.GetProperty("NextComicURL")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id='comic_ruutu']/center//a/img[starts-with(@src, 'images/page/default/previous')]",
				HtmlNodeAction = prevNextFunc,
				PropInfo = thisType.GetProperty("PreviousComicURL")
			});

			return comicPropArgs;
		}
	}
}