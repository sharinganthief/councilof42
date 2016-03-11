using System;
using System.Collections.Generic;
using System.Web;
using HtmlAgilityPack;
using WebApplicationBusiness;

namespace WebApplication1.Areas.Comic.Models.Comic
{
	public class OglafModel : BaseComicModel
	{
		private const string baseUrl = "http://oglaf.com/";

		public string ComicText { get; set; }

		public string ComicTitleURL { get; set; }



		public OglafModel()
			: base(baseUrl)
		{
			OglafModel model = Cb.GetComicModelFromCache<OglafModel>();

			if (model != null)
			{
				Helpers.ReflectionHelpers.SetPropertyValues<OglafModel>(this, model);
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
				HtmlNodePath = "//*[@id='strip']",
				HtmlNodeAction = (x => HttpUtility.HtmlDecode(x.GetAttributeValue("title", null))),
				PropInfo = thisType.GetProperty("ComicText")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id='strip']",
				HtmlNodeAction = (x => x.GetAttributeValue("src", null)),
				PropInfo = thisType.GetProperty("ComicURL")
			});

			comicPropArgs.Add(new ComicPropertyArgs()
			{
				HtmlNodePath = "//*[@id='tt']/img",
				HtmlNodeAction = (x => x.GetAttributeValue("src", null)),
				PropInfo = thisType.GetProperty("ComicTitleURL")
			});

			//////*[@id="comic_ruutu"]/center/table/tbody/tr[1]/td/table/tbody/tr/td[3]/a/img

			//comicPropArgs.Add(new ComicPropertyArgs()
			//{
			//	HtmlNodePath = "//*[@id='comic_ruutu']/center/table/tbody/tr[1]/td/table/tbody/tr/td[3]/a",
			//	HtmlNodeAction = (x => string.Format("{0}{1}", baseUrl, x.GetAttributeValue("href", null))),
			//	PropInfo = thisType.GetProperty("NextComicURL")
			//});

			//////*[@id="comic_ruutu"]/center/table/tbody/tr[1]/td/table/tbody/tr/td[2]/a

			//comicPropArgs.Add(new ComicPropertyArgs()
			//{
			//	HtmlNodePath = "//*[@id='comic_ruutu']/center/table/tbody/tr[1]/td/table/tbody/tr/td[2]/a",
			//	HtmlNodeAction = (x => string.Format("{0}{1}", baseUrl, x.GetAttributeValue("href", null))),
			//	PropInfo = thisType.GetProperty("PreviousComicURL")
			//});

			return comicPropArgs;
		}
	}

}