using FoodDeliveryShop.MVC.DataManagement;
using FoodDeliveryShop.MVC.Interfaces;
using FoodDeliveryShop.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryShop.MVC.Repositories
{
	public class EFOrderRepository : IOrderRepository
	{
        private FoodDeliveryShopContext context;

        public EFOrderRepository(FoodDeliveryShopContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));

            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }

            context.SaveChanges();
        }
    }
}
