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

            return View();
        }

        public ActionResult Graphs()
        {
            ViewBag.Title = "Graphs";
            return View();
        }

        public ActionResult MapChart()
        {
            DataAccess data = new DataAccess();
            ViewBag.Title = "MapChart";
            ViewBag.value = 0;
            ViewBag.value = data.GetProducts();
            return View();
        }

    }
}
