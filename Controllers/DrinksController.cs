using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DivineDrinks.Data.Interfaces;
using DivineDrinks.Data.Models;
using DivineDrinks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DivineDrinks.Controllers
{
    public class DrinksController : Controller
    {
        public DrinksController(IDrinkRepository drinkRepository,ICategoryRepository categoryRepository)
        {
            _drinkRepository = drinkRepository;
            _categoryRepository = categoryRepository;
        }

        public IDrinkRepository _drinkRepository { get; }
        public ICategoryRepository _categoryRepository { get; }

        public IActionResult List(string category)
        {
            var _category = category;
            IEnumerable<Drink> drinks;
            string currentCategory = String.Empty;

            if (string.IsNullOrEmpty(category))
            {
                drinks = _drinkRepository.Drinks.OrderBy(d => d.DrinkId);
                currentCategory = "All Drinks";
            }
            else
            {
                if (string.Equals("Alcoholic", _category, StringComparison.OrdinalIgnoreCase))
                {
                    drinks = _drinkRepository.Drinks.Where(d => d.Category.CategoryName.Equals("Alcoholic"));
                    
                }
                else {
                    drinks = _drinkRepository.Drinks.Where(d=>d.Category.CategoryName.Equals("Non-alcoholic"));
                   
                }

                currentCategory = _category;
            }

            var drinkListViewModel = new DrinkListViewModel {
                Drinks = drinks,
                CurrentCategory = currentCategory
            };

            return View(drinkListViewModel);
        }

        public IActionResult Details(int drinkId)
        {
            var drink = _drinkRepository.Drinks.FirstOrDefault(d => d.DrinkId == drinkId);
            if (drink == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }

            return View(drink);
        }
    }
}