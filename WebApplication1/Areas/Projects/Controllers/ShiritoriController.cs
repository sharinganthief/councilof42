using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Projects.Models;
using WebApplicationBusiness;

namespace WebApplication1.Areas.Projects.Controllers
{
    public class ShiritoriController : Controller
    {
        // GET: Projects/Shiritori
        public ActionResult Index()
        {
            ShiritoriBusiness biz = new ShiritoriBusiness();
            //List<string> words = biz.GetFourLetterList("test");
            ShiritoriModel model = new ShiritoriModel();
            //model.Words = words;
            return View(model);
        }

        [System.Web.Http.HttpPost]
        public ActionResult GetPossibleWords(string baseLetters)
        {
            
            ShiritoriBusiness biz = new ShiritoriBusiness();

            List<string> words = biz.GetFourLetterList(baseLetters);

            return Json(new { Words = words });
        }
    }
}