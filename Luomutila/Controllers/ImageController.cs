using LuomuTila.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuomuTila.Controllers {
    public class ImageController : Controller {
        // GET: Image
        public ActionResult ImageGallery(LuomuTilaImages obj) {

            obj.Images = Directory.EnumerateFiles(Server.MapPath("~/Images/")).Select(fn => "~/Images/" + Path.GetFileName(fn));
            var objFile = new DirectoryInfo(Server.MapPath("~/Images/"));
            var file = objFile.GetFiles("*.*");

            obj.Videos = Directory.EnumerateFiles(Server.MapPath("~/mp4/")).Select(fn => "~/mp4/" + Path.GetFileName(fn));
            var videoFile = new DirectoryInfo(Server.MapPath("~/mp4/"));
            var video = videoFile.GetFiles("*.*");
        
            return View(obj);
        }

        public ActionResult FileUpload(HttpPostedFileBase file) {
            if(file != null) {
                string pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/"), pic);

                file.SaveAs(path);
            }

            return RedirectToAction("ImageGallery", "Image");
        }

        [HttpPost]
        public ActionResult VideoUpload(HttpPostedFileBase video) {
            
            if(video != null) {
                string pic = Path.GetFileName(video.FileName);
                string path = Path.Combine(Server.MapPath("~/mp4/"), pic);

                video.SaveAs(path);
            }

            return RedirectToAction("ImageGallery", "Image");


        }
    }
}