using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Projects.Models;
using WebApplicationBusiness;
using WebApplicationData.Wiki;

namespace WebApplication1.Areas.Projects.Controllers
{
    public class WikiController : Controller
    {
        //
        // GET: /Projects/Wiki/
	    public ActionResult Index(WikiModel indexModel)
	    {
		    if (string.IsNullOrWhiteSpace(indexModel.WikipediaPageURL) && ! indexModel.GetRandom)
		    {
			    indexModel = new WikiModel();
			    return View(indexModel);
		    }
			WikiBusiness wb = new WikiBusiness();
		    bool emptyUrl = string.IsNullOrWhiteSpace(indexModel.WikipediaPageURL);
		    string urlToTry = emptyUrl || indexModel.GetRandom ? null : indexModel.WikipediaPageURL;
		    WikiCheckResult checkResult = wb.GetCheckResult(urlToTry);
		    indexModel.Result = checkResult;
		    return View(indexModel);
	    }
    }
}