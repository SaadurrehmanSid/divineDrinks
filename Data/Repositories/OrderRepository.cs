using DivineDrinks.Data.Interfaces;
using DivineDrinks.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineDrinks.Data.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private AppDbContext _context;
        private ShoppingCart _shoppingCart;

        public OrderRepository(AppDbContext  context,ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _context.Orders.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            foreach (var item in shoppingCartItems)
            {
                var orderDetails = new OrderDetail
                {
                    Amount = item.Amount,
                    DrinkId = item.Drink.DrinkId,
                    OrderId = order.OrderId,
                    Price = item.Drink.Price

                };
                _context.OrderDetails.Add(orderDetails);

            }
            _context.SaveChanges();
          
        }
    }
}
