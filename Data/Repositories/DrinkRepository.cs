using DivineDrinks.Data.Interfaces;
using DivineDrinks.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineDrinks.Data.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private AppDbContext _context;

        public DrinkRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Drink> Drinks => _context.Drinks.Include(c=>c.Category);

        public IEnumerable<Drink> PreferredDrinks => _context.Drinks.Where(p=>p.IsPreferredDrink).Include(c=>c.Category);

        public Drink GetDrinkById(int drinkId) => _context.Drinks.FirstOrDefault(d=>d.DrinkId == drinkId);
    }
}
