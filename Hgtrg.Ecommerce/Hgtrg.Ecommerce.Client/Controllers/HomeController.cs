using Hgtrg.Ecommerce.BusinessLayer.Services;
using Hgtrg.Ecommerce.DataLayer.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Hgtrg.Ecommerce.Client.Controllers
{
	public class HomeController : Controller
    {
        private readonly IProductServices _productServices;

		public HomeController(IProductServices productServices)
		{
			_productServices = productServices;
		}

		public IActionResult Index()
        {
            var slideProducts = _productServices.RetrieveProductsInformations(p => p.CategoryId.Equals((int)CategoryProduct.Slider)).First();
            //var slideProducts = _productServices.RetrieveProductsInformations(p => p.CategoryId.Equals((int)CategoryProduct.Slider));

            return View(slideProducts);
        }
    }
}