using FoodDeliveryShop.MVC.DataManagement;
using FoodDeliveryShop.MVC.Interfaces;
using FoodDeliveryShop.MVC.Models;

namespace FoodDeliveryShop.MVC.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly FoodDeliveryShopContext _context;
        public EFProductRepository(FoodDeliveryShopContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Products => _context.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }

            _context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = _context.Products.FirstOrDefault(p => p.ProductID == productID);

            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
