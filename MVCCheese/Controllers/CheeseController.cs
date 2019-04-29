using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCCheese.Models;

namespace MVCCheese.Controllers
{
    public class CheeseController : Controller
    {
        static private List<string> Cheeses = new List<string>();
        static private Dictionary<string, string> CheesesDictionary = new Dictionary<string, string>();
        static private List<Cheese> ListOfCheeseClasses = new List<Cheese>();

        public IActionResult Index()
        {
            ViewBag.cheeses = Cheeses;
            ViewBag.cheesesDictionary = CheesesDictionary;
            ViewBag.cheesesListOfCheese = ListOfCheeseClasses;
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
        

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string description)
        {
            Cheeses.Add(name);
            CheesesDictionary.Add(name, description);

            Cheese cheese = new Cheese();
            cheese.Name = name;

            cheese.Description = description;
            ListOfCheeseClasses.Add(cheese);

            return Redirect("/Cheese");
        }
    }
}