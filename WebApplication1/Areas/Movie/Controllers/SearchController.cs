using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using RestSharp;
using WebApplication1.Areas.Movie.Models;
using WebApplication1.Models;
using WebApplicationBusiness;
using WebApplicationData;
using WebApplicationData.Movie;
using WebApplicationData.WebApplication;

namespace WebApplication1.Areas.Movie.Controllers
{
	public class SearchController : Controller
	{
		private MoviesInfo info;
		public ActionResult Index(bool bypassUserCheck = false)
		{
			Guid movieApiUserID = Guid.Empty;
			UserInfo userInfo = null;

			if (User.Identity.IsAuthenticated)
			{
				userInfo = new UserInfo(UserInformationRepository.GetUserInformation(User.Identity.GetUserId()));
				movieApiUserID = userInfo.AdditionalProperties.MovieAPIUserID;
				
				
				if (movieApiUserID.Equals(Guid.Empty) || bypassUserCheck)
				{
					return RedirectToAction("RegisterForMovieAPI");
				}
			}

			

			SearchModel model = new SearchModel();
			info = GetMoviesInfo();
			model.Length = info.Length;
			model.Year = info.Year;
			model.Categories = new List<MovieCategory>();
			model.Categories.Add(new MovieCategory(){ Name = "Actors", Values = info.Actors});
			model.Categories.Add(new MovieCategory() { Name = "Characters", Values = info.Characters }); 
			model.Categories.Add(new MovieCategory(){ Name = "Directors", Values = info.Directors});
			model.Categories.Add(new MovieCategory(){ Name = "Writers", Values = info.Writers});
			model.Categories.Add(new MovieCategory(){ Name = "Ratings", Values = info.Ratings});
			model.Categories.Add(new MovieCategory(){ Name = "Genres", Values = info.Genres});
			model.Categories.Add(new MovieCategory() { Name = "Plot_Keywords", Values = info.Tags });
			//model.Categories.Add(new MovieCategory(){ Name = "Available_Users", Values = info.AvailableUsers});
			if (userInfo != null)
				model.MovieUserID = bypassUserCheck ? Guid.Empty : userInfo.AdditionalProperties.MovieAPIUserID;
			model.Titles = info.Titles;

			return View(model);
		}

		[System.Web.Http.HttpPost]
		public ActionResult Search(SearchCriteria form)
		{

			UserInfo userInfo = new UserInfo(UserInformationRepository.GetUserInformation(User.Identity.GetUserId()));

			form.UserID = userInfo.AdditionalProperties.MovieAPIUserID;
			form.Randomize = true;
			MovieBusiness mb = new MovieBusiness();
			List<MovieResult> result = mb.SearchForMovies(form);
			return Json( new
						{
							Success = true,
							Movies = result,
						});
		}

		private MoviesInfo GetMoviesInfo()
		{
			MovieBusiness mb = new MovieBusiness();
			info = mb.GetMoviesInfo();
			return info;
		}

		public ActionResult Magnet(string title)
		{
			//string pirateBayLink = string.Format("http://thepiratebay.se/search/{0}/0/7/207", title); -hd

			string pirateBayLink = string.Format("http://thepiratebay.se/search/{0}/0/7/207", HttpUtility.HtmlEncode(title));

			HtmlDocument doc = new HtmlWeb().Load(pirateBayLink);

			HtmlNodeCollection imgTags = doc.DocumentNode.SelectNodes("/html[1]/body[1]/div[2]/div[1]/div[2]/table[1]/tr/td/a[starts-with(@href,'/user')]/img[starts-with(@alt,'VIP')]");
			if (!imgTags.Any())
			{
				string googleSearch = string.Format("http://www.google.com/#q={0}+torrent", title);

				return Redirect(googleSearch);
			}

			HtmlNode imgTag = imgTags.First();
			var torrentNode = imgTag.ParentNode.ParentNode.SelectSingleNode("//a[starts-with(@href,'/torrent')]");
			var torrentHref = torrentNode.GetAttributeValue("href", null);
			string torrentLink = string.Format("http://thepiratebay.se{0}", torrentHref);
			return Redirect(torrentLink);
			//HtmlNode imgTag = imgTags.First();
			//var magnetNode = imgTag.ParentNode.ParentNode.SelectSingleNode("//a[starts-with(@href,'magnet')]");
			//var magnetLink = magnetNode.GetAttributeValue("href", null);
			//return Redirect(magnetLink);
		}

		[HttpGet]
		public ActionResult RegisterForMovieAPI()
		{
			return View();
		}

		[HttpPost]
		public ActionResult DoRegisterForMovieAPI()
		{
			UserInformation userInformation = UserInformationRepository.GetUserInformation(User.Identity.GetUserId());
			UserInfo userInfo = new UserInfo(userInformation);

			MovieBusiness mb = new MovieBusiness();
			MovieUserAddResult result = mb.RegisterMovieAPIUser(userInfo);

			if (result.Successful)
			{
				userInfo.AdditionalProperties.MovieAPIUserID = result.MovieAPIUserID;

				userInformation.SerializedUserInfo = JsonConvert.SerializeObject(userInfo.AdditionalProperties);

				UserInformationRepository.UpdateUserInformation(userInformation);
			}
			else
			{
			
			    foreach (string error in result.Errors)
			    {
					ModelState.AddModelError("Model", error);    
			    }

				return View("RegisterForMovieAPI");

			}


			return RedirectToAction("ManageUserMovies", "Admin");

		}
	}
}
