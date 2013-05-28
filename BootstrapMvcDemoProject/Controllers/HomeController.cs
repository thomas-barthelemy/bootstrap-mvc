using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapMvcDemoProject.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuOne()
        {
            return View();
        }

        public ActionResult MenuTwo()
        {
            return View();
        }
    }
}
