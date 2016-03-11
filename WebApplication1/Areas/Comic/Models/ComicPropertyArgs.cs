using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using HtmlAgilityPack;

namespace WebApplication1.Areas.Comic.Models
{
	public class ComicPropertyArgs
	{
		public Func<HtmlNode, string> HtmlNodeAction { get; set; }
		public string HtmlNodePath { get; set; }

		public PropertyInfo PropInfo { get; set; }
		public string SecondaryHtmlNodePath { get; set; }
	}
}