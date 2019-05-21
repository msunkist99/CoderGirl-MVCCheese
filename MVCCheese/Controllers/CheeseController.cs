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
        public IActionResult Index()
        {
            ViewBag.cheeses = CheeseData.GetAll();

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(Cheese newCheese)
        {
            // Add the new cheese to my existing cheeses
            CheeseData.Add(newCheese);

            return Redirect("/Cheese");
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                CheeseData.Remove(cheeseId);
            }

            return Redirect("/");
        }

        public IActionResult Edit(int cheeseId)
        {
            ViewBag.cheese= CheeseData.GetById(cheeseId);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int cheeseId, string name, string description)
        {
            CheeseData.Remove(cheeseId);

            Cheese newCheese = new Cheese();
            newCheese.Name = name;
            newCheese.Description = description;
            CheeseData.Add(newCheese);

            return Redirect("/");
        }
    }
}