using LuomuTila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace LuomuTila.Controllers
{
    public class ApplicationController : Controller
    {
        

        // GET: Application
        public ActionResult SummerApplicationForm() {
            return View(); 
        }

        [HttpPost]
        public ActionResult SummerApplicationForm(JobForm jobform) {
            DateTime date = DateTime.Now;

            JobForm sendFormEmail = new JobForm();
            sendFormEmail.SendJobForm(jobform);

            return RedirectToAction("Index", "Home");
            
        }

        public ActionResult PricesTable() {
            return View();
        }

        public JsonResult PricesTableGet() {

            KoskenkorvanLuomutilaEntities3 entity = new KoskenkorvanLuomutilaEntities3();
            

            var pricesList = (from p in entity.Prices
                              select p).ToList();

            string json = JsonConvert.SerializeObject(pricesList);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBerryValues(string name) {

            KoskenkorvanLuomutilaEntities3 entity = new KoskenkorvanLuomutilaEntities3();

            var pricesList = (from p in entity.Prices
                              where p.Berry == name
                              select p).FirstOrDefault();

            string json = JsonConvert.SerializeObject(pricesList);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult UpdateBerryPrice(Prices prices) {

            KoskenkorvanLuomutilaEntities3 entity = new KoskenkorvanLuomutilaEntities3();

            string berry = prices.Berry;

            bool success = false;

            Price oldPrice = (from p in entity.Prices
                               where p.Berry == berry
                               select p).FirstOrDefault();

            if(oldPrice != null) {
                oldPrice.Berry = prices.Berry;
                oldPrice.Price_per_litre = prices.Price_Per_Litre;

                entity.SaveChanges();
                success = true;
            }

            entity.Dispose();

            return Json(success);
        }

        public JsonResult DeleteBerry(Prices prices) {

            KoskenkorvanLuomutilaEntities3 entity = new KoskenkorvanLuomutilaEntities3();

            string berry = prices.Berry;

            bool success = false;

            var delete = (from p in entity.Prices
                              where p.Berry == berry
                              select p).FirstOrDefault();

            if(delete != null) {
                ObjectContext oc = ((IObjectContextAdapter)entity).ObjectContext;
                oc.DeleteObject(delete);
                oc.SaveChanges();

                success = true;
            }
           
            

            entity.Dispose();

            return Json(success);
        }

        public JsonResult AddNewItem(Prices prices) {

            KoskenkorvanLuomutilaEntities3 entity = new KoskenkorvanLuomutilaEntities3();

            bool success = false;
            Price price = new Price();
            price.Berry = prices.Berry;
            price.Price_per_litre = prices.Price_Per_Litre;
            entity.Prices.Add(price);
            entity.SaveChanges();

            entity.Dispose();

            return Json(success);
        }

        public ActionResult GeneralKnowledge() {
            //change server to whatever local computer name you are connecting to 
            //use this when on local sql datasource
            //string connectionString = @"Server=DESKTOP-PK94DMM\SQLEXPRESS;Database=KoskenkorvanLuomutila;Trusted_Connection=yes;";

            //use this when connecting to azure ( live ) datasource
            string connectionString = @"Server=tcp:luomutila.database.windows.net,1433;Initial Catalog=KoskenkorvanLuomutila;Persist Security Info=False;User ID=luomutila;Password=Pointcollege123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            SqlConnection conn = new SqlConnection(connectionString);

            List<BerryKnowledge> knowledge = new List<BerryKnowledge>();

            try {
                string longDescription = "SELECT Berry, Titleusage, Titlecrowth, Titleknowledge, UsageKnowledge, CrowthKnowledge FROM GeneralKnowledge";

                SqlCommand command = new SqlCommand(longDescription, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read()) {
                    BerryKnowledge berryKnowledge = new BerryKnowledge();
                    berryKnowledge.Berry = reader.GetString(0);
                    berryKnowledge.Titleusage = reader.GetString(1);
                    berryKnowledge.Titlecrowth = reader.GetString(2);
                    berryKnowledge.TitleKnowledge = reader.GetString(3);
                    berryKnowledge.UsageKnowledge = reader.GetString(4);
                    berryKnowledge.CrowthKnowledge = reader.GetString(5);

                    knowledge.Add(berryKnowledge);
                }

                conn.Close();

            } catch (Exception e) {
                ViewBag.ErrorMessage = "Virhe tapahtui tietokanta käsittelussä: " + e.Message;
            }
            return View(knowledge);
        }

        public ActionResult AppointmentForm() {
            return View();
        }

        public ActionResult AppointmentFormPost(AppointmentForm appointmentForm) {

            AppointmentForm sendFormEmail = new AppointmentForm();
            sendFormEmail.SendEmail(appointmentForm);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Openinghours() {

            string connectionString = @"Server=tcp:luomutila.database.windows.net,1433;Initial Catalog=KoskenkorvanLuomutila;Persist Security Info=False;User ID=luomutila;Password=Pointcollege123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            SqlConnection conn = new SqlConnection(connectionString);

            List<Openingtimes> openinghours = new List<Openingtimes>();

            try {
                string sqlopen = "SELECT Dayofweek, Openingtime, closingtime from Openinghours";

                SqlCommand command = new SqlCommand(sqlopen, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read()) {
                    Openingtimes openhours = new Openingtimes();
                    openhours.DayOfWeek = reader.GetString(0);
                    openhours.Openinghours = reader.GetString(2);
                    openhours.Closinghours = reader.GetString(1);

                    openinghours.Add(openhours);
                }

                conn.Close();

            } catch(Exception e) {
                ViewBag.ErrorMessage = "Virhe tapahtui tietokanta käsittelussä: " + e.Message;
            }

            return View(openinghours);
        }
    }
    
}