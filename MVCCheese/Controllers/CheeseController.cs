using Microsoft.AspNetCore.Mvc;
using MVCCheese.Models;
using System.Collections.Generic;
using MVCCheese.ViewModels;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Cheese> cheeses = CheeseData.GetAll();

            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing cheeses
                Cheese newCheese = addCheeseViewModel.CreateCheese();

                CheeseData.Add(newCheese);

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int cheeseId)
        {
            ViewBag.cheese = CheeseData.GetById(cheeseId);
            Cheese cheese = CheeseData.GetById(cheeseId);

            EditCheeseViewModel editCheese = new EditCheeseViewModel();

            editCheese.CheeseId = cheese.CheeseId;
            editCheese.Name = cheese.Name;
            editCheese.Description = cheese.Description;
            editCheese.Type = cheese.Type;

            return View(editCheese);
        }

        [HttpPost]
        public IActionResult Edit(EditCheeseViewModel editCheeseViewModel)
        {
            Cheese cheese = CheeseData.GetById(editCheeseViewModel.CheeseId);

            cheese.Name = editCheeseViewModel.Name;
            cheese.Description = editCheeseViewModel.Description;
            cheese.Type = editCheeseViewModel.Type;

            CheeseData.Remove(editCheeseViewModel.CheeseId);
            CheeseData.Add(cheese);

            return Redirect("Index");
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
    }
}