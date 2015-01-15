using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapMvcDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MenuOne()
        {
            return View();
        }

        public ActionResult MenuTwo()
        {
            ViewBag.Progress = 50.00;
            return View();
        }

        [HttpPost]
        public ActionResult MenuTwo(Object viewModel)
        {
            ViewBag.Progress = new Random().Next(0, 100);
            return View();
        }
    }
}