﻿using FoodDeliveryShop.MVC.Models;

namespace FoodDeliveryShop.MVC.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}
