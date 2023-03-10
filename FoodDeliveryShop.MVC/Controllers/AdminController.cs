using FoodDeliveryShop.MVC.Interfaces;
using FoodDeliveryShop.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryShop.MVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
	{
        private IProductRepository _repository;

        public AdminController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index()
        { 
            return View(_repository.Products);
        }

        public ViewResult Edit(int productId)
        { 
            return View(_repository.Products.FirstOrDefault(p => p.ProductID == productId));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        { 
            return View("Edit", new Product());
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = _repository.DeleteProduct(productId);

            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }

            return RedirectToAction("Index");
        }

    }
}
