using Ecommerce.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.PL.Controllers
{
    public class CategoryProductsController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categoryManager; // if you have one, otherwise use _productManager.GetAllCategories()

        
        public CategoryProductsController(IProductManager productManager, ICategoryManager categoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
        }

        public IActionResult Index()
        {
            var vm = new ProductCategoryVM
            {
                Categories = GetCategoriesSelectList(),
                Products = GetProductsSelectList()
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult GetProductsByCategory(int categoryId)
        {
            var products = _productManager.GetByCategory(categoryId);
            return PartialView("_CategoryProductsPartial", products);
        }

        [HttpGet]
        public IActionResult GetProductById(int id)
        {
            var product = _productManager.GetById(id);
            if (product == null)
                return RedirectToAction(nameof(Index));

            return PartialView("_ProductDetailsPartial", product);
        }

        public IActionResult CategoryProducts()
        {
            var categories = GetCategoriesSelectList();
            return View(categories);
        }

        /*──────────── Private Helpers ────────────*/

        private List<SelectListItem> GetCategoriesSelectList()
        {
            return _productManager.GetAllCategories()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
        }

        private List<SelectListItem> GetProductsSelectList()
        {
            return _productManager.GetAll()
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = p.ProductName
                }).ToList();
        }
    }
}