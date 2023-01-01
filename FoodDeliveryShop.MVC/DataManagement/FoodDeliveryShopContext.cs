using FoodDeliveryShop.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryShop.MVC.DataManagement
{
    public class FoodDeliveryShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public FoodDeliveryShopContext(DbContextOptions<FoodDeliveryShopContext> options) : base(options)
        {

        }
    }
}
