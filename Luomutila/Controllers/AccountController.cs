using LuomuTila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace LuomuTila.Controllers
{
    public class AccountController : Controller {
        // GET: Account
              
        [HttpPost]
        public ActionResult Login(User user) {

            KoskenkorvanLuomutilaEntities3 entity = new KoskenkorvanLuomutilaEntities3();

            var usr = entity.Users.Single(u => u.Username == user.Username && u.Password == user.Password);

            if(usr != null) {
                Session["Username"] = usr.Username.ToString();
                return RedirectToAction("Index", "Home");
            } else {
                ModelState.AddModelError("", "Käyttäjätunnus tai salasana virheellinen");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout() {
            Session.Abandon();
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        /*
        public ActionResult LoggedIn() {
            if(Session["Username"] != null) {
                return RedirectToAction("Index", "Home");
            } else {
                return RedirectToAction("Index", "Home");
            }
        }
        */
        public JsonResult LoggedInOrNot() {

            Boolean IsLogged = false;
            if(Session["Username"] != null) {
                IsLogged = true;
            } else {
                IsLogged = false;
            }
            return Json(IsLogged, JsonRequestBehavior.AllowGet);
        }
    }
}