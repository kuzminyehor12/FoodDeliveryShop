using FoodDeliveryShop.MVC.Models;

namespace FoodDeliveryShop.MVC.Interfaces
{
	public interface IOrderRepository
	{
        IEnumerable<Order> Orders { get; }
        void SaveOrder(Order order);

    }
}
