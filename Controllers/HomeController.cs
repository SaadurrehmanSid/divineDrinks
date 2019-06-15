using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DivineDrinks.Data.Interfaces;
using DivineDrinks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DivineDrinks.Controllers
{
    public class HomeController : Controller
    {
        private IDrinkRepository _drinkRepository;

        public HomeController(IDrinkRepository drinkRepository)
        {
            _drinkRepository = drinkRepository;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel {
                PreferredDrinks = _drinkRepository.PreferredDrinks
            };

            return View(homeViewModel);
        }
    }
}