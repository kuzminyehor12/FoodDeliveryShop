using FoodDeliveryShop.MVC.Interfaces;
using FoodDeliveryShop.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FoodDeliveryShop.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        public int PageSize { get; set; }
        public ProductController(IProductRepository repo)
        {
            _repository = repo;
            PageSize = 4;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult List(string category, int page = 1)
        {
            var products = _repository.Products
                   .Where(p => category == null || p.Category == category)
                   .OrderBy(p => p.ProductID)
                   .Skip((page - 1) * PageSize)
                   .Take(PageSize);

            return View(new ProductListViewModel
            {
                Products = products,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? _repository.Products.Count() :
                                                _repository.Products
                                                .Where(e => e.Category == category)
                                                .Count()

                },
                CurrentCategory = category
            });
        } 
    }
}
