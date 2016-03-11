using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication1.Areas.Movie.Models;
using WebApplication1.Attributes;
using WebApplicationBusiness;
using WebApplicationData.Movie;
using WebApplicationData.WebApplication;

namespace WebApplication1.Areas.Movie.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Movie/Admin/
		[AuthorizeMovieAdmin]
        public ActionResult Index()
        {
			MovieBusiness biz = new MovieBusiness();
	        MoviesInfo info = biz.GetMoviesInfo();
			AdminModel model = new AdminModel();
	        model.Titles = info.Titles;

            return View(model);
        }
		
		[AuthorizeMovieAdmin]
	    public ActionResult AddMovies(FormCollection collection)
	    {
		    if (collection == null || !collection.HasKeys())
			    return View();

		    string movieVal = collection["moviesToAdd"];

			if(string.IsNullOrWhiteSpace(movieVal))
				return View();

		    List<string> moviesToAdd = movieVal.Split(new [] {'\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			MovieBusiness biz = new MovieBusiness();
		    MovieAddResult result = biz.AddMovies(moviesToAdd);

		    if (!result.Successful)
		    {
			    foreach (string error in result.Errors)
			    {
					ModelState.AddModelError("Model", error);    
			    }
			    
		    }

		    return View();
	    }

		[HttpGet]
		public ActionResult ManageUserMovies()
		{
			UserInfo userInfo = new UserInfo(UserInformationRepository.GetUserInformation(User.Identity.GetUserId()));

			ManageUserMoviesModel model = new ManageUserMoviesModel();

			MovieBusiness mb = new MovieBusiness();
			List<MovieResult> info = mb.GetAllFilms();
			List<MovieResult> userMovies = mb.GetAllUserFilms(userInfo.AdditionalProperties.MovieAPIUserID);

			model.MoviesList = info;
			model.UserMovies = userMovies;

			return View(model);
		}

		[System.Web.Http.HttpPost]
		public ActionResult UpdateMoviesForUser(List<int> movieIDs)
		{
			UserInfo userInfo = new UserInfo(UserInformationRepository.GetUserInformation(User.Identity.GetUserId()));

			MovieBusiness mb = new MovieBusiness();
			var result = mb.UpdateMoviesForUser(movieIDs, userInfo.AdditionalProperties.MovieAPIUserID);
			return Json( new
						{
							Success = true,
							Movies = result,
						});
		}

		
    }
}