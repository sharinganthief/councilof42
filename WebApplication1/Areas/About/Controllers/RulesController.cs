using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApplication1.Areas.About.Models;

namespace WebApplication1.Areas.About.Controllers
{
    public class RulesController : Controller
    {
        // GET: About/Rules
        public ActionResult Index()
        {
            RulesModel rulesModel = new RulesModel();
            String json = System.IO.File.ReadAllText(String.Format("{0}\\Content\\Resources\\Rules\\Rules.json", AppDomain.CurrentDomain.BaseDirectory));
            rulesModel.RulesList = JsonConvert.DeserializeObject<List<string>>(json);
            return View(rulesModel);
        }
    }
}