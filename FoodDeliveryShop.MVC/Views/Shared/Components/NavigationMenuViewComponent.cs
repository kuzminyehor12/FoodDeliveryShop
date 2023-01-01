using FoodDeliveryShop.MVC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryShop.MVC.Views.Shared.Components
{
	public class NavigationMenuViewComponent : ViewComponent
	{
        private IProductRepository _repository;
        public NavigationMenuViewComponent(IProductRepository repo)
        {
            _repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_repository.Products
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x));
        }

    }
}
