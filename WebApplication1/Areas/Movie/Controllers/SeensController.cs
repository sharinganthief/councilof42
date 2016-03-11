using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Movie.Models;
using WebApplicationBusiness;
using WebApplicationData.Movie;

namespace WebApplication1.Areas.Movie.Controllers
{
	public class SeensController : Controller
	{
		//
		// GET: /Movie/Seens/
        //public ActionResult Index()
        //{
        //    MovieBusiness mb = new MovieBusiness();
        //    MoviesInfo movieInfo = mb.GetMoviesInfo();

        //    SeensModel model = new SeensModel();
        //    model.Users = movieInfo.AvailableUsers;
            

        //    return View(model);
        //}

		[HttpPost]
		public ActionResult GetSeensForUsers(string user)
		{

			MovieBusiness mb = new MovieBusiness();


			List<SeenDto> seenDtos = mb.GetSeensForUser(user);

			if (Request.IsAjaxRequest())
			{
				return PartialView("_Seens", seenDtos);
			}

			throw new ApplicationException("No calling this from not ajax, weirdo");
		}
	}
}