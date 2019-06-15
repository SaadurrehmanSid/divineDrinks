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
    public class ShoppingCartController : Controller
    {
        private IDrinkRepository _drinkRepository;
        private ShoppingCart _cart;

        public ShoppingCartController(IDrinkRepository drinkRepository,ShoppingCart cart)
        {
            _drinkRepository = drinkRepository;
            _cart = cart;
        }

        public IActionResult Index()
        {
            var items = _cart.GetShoppingCartItems();
            _cart.ShoppingCartItems = items;
            var scvm = new ShoppingCartViewModel
            {
                ShoppingCart = _cart,
                ShoppingCartTotal = _cart.GetShoppingCartTotal()
            };

            return View(scvm);
        }

        public IActionResult AddToShoppingCart(int drinkId)
        {
            var selectedItem = _drinkRepository.Drinks.FirstOrDefault(d => d.DrinkId == drinkId);
            if (selectedItem != null)
            {
                _cart.AddToCart(selectedItem, 1);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromShoppingCart(int drinkId)
        {
            var selectedItem = _drinkRepository.Drinks.FirstOrDefault(d => d.DrinkId == drinkId);
            if (selectedItem != null)
            {
                _cart.RemoveFromCart(selectedItem);
            }
            return RedirectToAction("Index");
        }
    }
}