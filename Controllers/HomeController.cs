using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MyApp.Models;

namespace MyApp.Namespace
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            ViewBag.Message = "Välkommen till min MVC applikation";

            var jsonStr = System.IO.File.ReadAllText("MartialArts.json"); //Läser in JSON filen som en textsträng
            var JsonObj = JsonConvert.DeserializeObject<IEnumerable<MartialArts>>(jsonStr); //konverterar JSON textsträngen till en lista av kampsport objekt

            return View(JsonObj); //Skickar listan till vyn som användaren ser
        }

        [HttpGet]
        public ActionResult AddMartialArt()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMartialArt(MartialArts newart){


                string n = JsonConvert.SerializeObject(newart);
                HttpContext.Session.SetString("mySession", n);

               var jsonStr = System.IO.File.ReadAllText("MartialArts.json");
               var jsonObj = JsonConvert.DeserializeObject<List<MartialArts>>(jsonStr);

               newart.id = jsonObj.Max(m => m.id) + 1;
               jsonObj.Add(newart);

               System.IO.File.WriteAllText("MartialArts.json", JsonConvert.SerializeObject(jsonObj));

               return RedirectToAction("GetMartialArt");
        }

        public ActionResult GetMartialArt() {

            MartialArts? ma = new MartialArts();
            string? jsonStr = HttpContext.Session.GetString("mySession");
            ma = JsonConvert.DeserializeObject<MartialArts>(jsonStr);
          
            ViewBag.MartialArtName = ma?.MartialArtName;
            ViewBag.Link = ma?.Link; 
            ViewBag.Description = ma?.Description; 
            
            return View(ma);
        }

    }
}
