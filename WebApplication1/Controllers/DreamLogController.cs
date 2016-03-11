using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Attributes;
using WebApplicationData;
using WebApplicationData.DreamLogData;

namespace WebApplication1.Controllers
{
	public class DreamLogController : Controller
	{
		//
		// GET: /DreamLog/
		[AuthorizeFriend]
		public ActionResult Index()
		{

			DreamLogBusiness business = new DreamLogBusiness();
			List<DreamLog> logs = business.GetLogs();



			return View(logs);
		}

		[AuthorizeDeveloper]
		public ActionResult AddDreamLog()
		{

			return View(new DreamLog());
		}

		[AuthorizeDeveloper]
		public ActionResult EditDreamLog(Guid dreamLogID)
		{
			DreamLogBusiness business = new DreamLogBusiness();

			DreamLog log = business.GetLog(dreamLogID);
			return View(log);
		}

		[HttpPost]
		[AuthorizeDeveloper]
		public ActionResult EditDreamLog(DreamLog logToEdit)
		{
			DreamLogBusiness business = new DreamLogBusiness();
			business.AddEditLog(logToEdit);
			return View();
		}

		[HttpPost]
		[AuthorizeDeveloper]
		public ActionResult AddDreamLog(DreamLog logToAdd)
		{
			logToAdd.Id = Guid.NewGuid();

			DreamLogBusiness business = new DreamLogBusiness();
			business.AddEditLog(logToAdd);

			return View();
		}
	}
}