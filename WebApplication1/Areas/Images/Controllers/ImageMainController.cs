using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SqlServer.Server;
using WebApplication1.Areas.Images.Models;
using WebApplication1.Attributes;
using WebApplicationBusiness;
using WebApplicationData.Image;

namespace WebApplication1.Areas.Images.Controllers
{
	public class ImageMainController : Controller
	{

		//
		// GET: /Images/ImageMain/
		[AuthorizeUserAndFriend]
		
		public ActionResult Index(int? id)
		{
			return Index(id, null);
		}

		public ActionResult ImageTags()
		{
			ImageBusiness business = new ImageBusiness();

			ImageTagModel model = new ImageTagModel();
			model.ImageTags = business.GetImageTagCounts();

			return View(model);
		}

        
		[HttpGet]
		public ActionResult Index(int? page, string[] imageTags)
		{
		    return Main(page, imageTags);
		}

        [HttpPost]
        public ActionResult IndexPost(int? page, string[] imageTags)
		{
			return Main(page, imageTags);
		}

	    private ActionResult Main(int? page, string[] imageTags)
	    {
	        int _page = page ?? 0;

			ImageBusiness business = new ImageBusiness();

			List<ImageDto> results = business.GetPaginatedImages(_page, imageTags);

			if (Request.IsAjaxRequest())
			{
				return PartialView("_Images", results);
			}

			List<string> tags = business.GetAllTags();

	        List<string> selectedTags = imageTags == null ? new List<string>() : imageTags.ToList();

			IndexModel model = new IndexModel {Images = results, AllTags = tags, SelectedTags = selectedTags};

			return View(model);
	    }


		// GET: /Images/ImageMain/UploadImage

		[HttpPost]
		public ActionResult UploadImage(string imageURL, string imageName, string imageDescription, List<string> selectedTags, bool nsfw = false, bool api = false)
		{
			List<string> errorList = new List<string>();

			ImageBusiness business = new ImageBusiness();

			List<string> allTags = business.GetAllTags();


			if (new[] {imageURL, imageName, imageDescription}.Any(string.IsNullOrWhiteSpace))
			{
				const string validationError = "All Fields are required. Please fill them out appropriately";
				errorList.Add(validationError);

				if (!api)
				{
					ModelState.AddModelError(string.Empty,validationError);
					return View(new UploadImageModel()
					{
						AllTags = allTags,
					});
				}
				
			}

			

			AddImageResult result = business.UploadImage(imageURL, imageName, imageDescription, nsfw, selectedTags);

			if (!result.Success)
			{
				string submissionError = string.Format("Error Uploading Image: [ {0} ]", result.Errors.First());
				errorList.Add(submissionError);
				if (!api)
				{
					ModelState.AddModelError(string.Empty,submissionError);
					return View(new UploadImageModel()
					{
						AllTags = allTags,
					});
				}
				
			}

			return api ? Json(new {Success = ! errorList.Any(), Errors = errorList}) : Json(new { Success = true});
		}

		//[HttpPost]
		//public ActionResult FilterByTag(string[] imageTags, int? page)
		//{
			//ImageBusiness business = new ImageBusiness();
			//if(imageTags == null || !imageTags.Any())
			//{
			//    return PartialView("_Images", business.GetAllImages());
			//}

			//List<ImageDto> result = business.FilterByTags(imageTags.ToList(), page);

			//if (Request.IsAjaxRequest())
			//{
			//    return PartialView("_Images", result);
			//}

			//throw new ApplicationException("Shouldn't get here...");
		//}

		[AuthorizeUserAndFriend]
		public ActionResult UploadImage()
		{
			ImageBusiness business = new ImageBusiness();

			List<string> allTags = business.GetAllTags();

			return View(new UploadImageModel()
			{
				AllTags = allTags,
			});
		}

	}
}