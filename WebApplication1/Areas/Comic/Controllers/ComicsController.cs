using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using WebApplication1.Areas.Comic.Models.Comic;
using WebApplication1.Attributes;
using WebApplicationBusiness;
using WebApplicationData.Comic;

namespace WebApplication1.Areas.Comic.Controllers
{
	public class ComicsController : Controller
	{
		public ComicsController()
		{
			this.attributes = this.GetType().Assembly
				.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(BaseAuthorizeAttribute)))
				.Select(t => t.FullName).ToList();
		}

		private const string comicActionsCacheKey = "comicActions";

		private List<string> attributes;

		public List<ComicInfo> _actions { get; set; }
		public Dictionary<string, ComicInfo> ActionList
		{
			get
			{
				ComicBusiness cb = new ComicBusiness(attributes);
				if (_actions == null)
				{
					_actions = (cb.GetComicInfosForController(Assembly.GetCallingAssembly(), this.GetType().Name, typeof(ComicActionMethodAttribute).FullName));

				}

				Dictionary<string, ComicInfo> actionList = cb.GetItemFromCache<Dictionary<string, ComicInfo>>(comicActionsCacheKey);
				if (actionList == null)
				{
					actionList = new Dictionary<string, ComicInfo>();
					foreach (ComicInfo action in _actions)
					{
						actionList.Add(action.Name, action);
					}
					cb.AddItemToCache(comicActionsCacheKey, actionList);
				}

				return actionList;
			}
		}

		//
		// GET: /Comic/
		[AuthorizeUser]
		public ActionResult Index()
		{
			ComicIndexModel model = new ComicIndexModel();
			model.ActionsList = new Dictionary<string, ComicInfo>();
			Dictionary<string, ComicInfo> actionList = ActionList;
			foreach (KeyValuePair<string, ComicInfo> action in actionList)
			{
				bool authorized = false;
				ComicInfo info = action.Value;
				authorized = info.Roles.Any(User.IsInRole);
				if (!authorized)
				{
					continue;
				}
				model.ActionsList.Add(action.Key, action.Value);
			}
			return View(model);
		}

		[AuthorizeUserAndFriend]
		[ComicActionMethod]
		public ActionResult Xkcd(bool loadPartial, string comicURL = null)
		{
			XkcdModel model = new XkcdModel(comicURL);
			if (loadPartial)
			{
				PartialViewResult partial = PartialView("_Xkcd", model);
				return partial;
			}
			return View("_Xkcd", model);

		}

		[AuthorizeUserAndFriend]
		[ComicActionMethod]
		public ActionResult SMBC(bool loadPartial, string comicURL = null)
		{
			SmbcModel model = new SmbcModel(comicURL);

			if (loadPartial)
			{
				PartialViewResult partial = PartialView("_SMBC", model);
				return partial;
			}

			ViewResult view = View("_SMBC", model);
			return view;
		}

		[AuthorizeUserAndFriend]
		[ComicActionMethod]
		public ActionResult BlastWave(bool loadPartial, string comicURL = null)
		{
			BlastWaveModel model = new BlastWaveModel(comicURL);

			if (loadPartial)
			{
				PartialViewResult partial = PartialView("_BlastWave", model);
				return partial;
			}

			ViewResult view = View("_BlastWave", model);
			return view;
		}

		[AuthorizeFriend]
		[ComicActionMethod]
		public ActionResult Oglaf(bool loadPartial)
		{
			OglafModel model = new OglafModel();

			if (loadPartial)
			{
				PartialViewResult partial = PartialView("_Oglaf", model);
				return partial;
			}

			ViewResult view = View("_Oglaf", model);
			return view;
		}


	}
}