using Bogles.Charts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bogles.Charts.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            DataAccess data = new DataAccess();
            ViewBag.Languages = data.GetLanguages();
            ViewBag.Status = "Connesso";
            ViewBag.MapData = data.GetMapData();
            return View();

        }

        public ActionResult Graphs(string id)
        {
            DataAccess data = new DataAccess();
            ViewBag.Title = "Graphs";
            ViewBag.id = id;
            ViewBag.Country = data.GetCountryName(id);
            return View();
        }

        public ActionResult MapChart()
        {
            DataAccess data = new DataAccess();
            ViewBag.Title = "MapChart";
            
            ViewBag.value = "Connesso";
            return View();
        }

    }
}
