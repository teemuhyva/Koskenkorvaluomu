using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuomuTila.Controllers
{
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Title="Koskenkorvan luomutila";
            return View();
        }

        public ActionResult Contact() {
            return View();
        }

        public ActionResult About() {
            //better implementation for word files and other documentation
            return View();
        }

        public ActionResult Maps() {
            return View();
        }
    }
}