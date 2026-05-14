
using Ecommerce.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.PL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryManager CategoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            CategoryManager = categoryManager;
        }

        public IActionResult Index()
        {
            var categoriesReadVM = CategoryManager.GetAll();
          

            return View(categoriesReadVM);
        }

        /*----------------------------------------------------------*/
        public IActionResult GetById(int id)
        {
            //show category details with its products
            var categoryWithProductsRVM = CategoryManager.GetById(id);
            if (categoryWithProductsRVM == null)
            {
                return RedirectToAction("Index");
            }
            return View(categoryWithProductsRVM);

        }
        /*----------------------------------------------------------*/
         [HttpGet]
         public IActionResult Create()
         {
             return View();
        }
        /*----------------------------------------------------------*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryCreateVM categoryCreateVM)
        {
            CategoryManager.Create(categoryCreateVM);
            return RedirectToAction("Index");
         }
        /*----------------------------------------------------------*/
        public IActionResult Delete(int id)
        {
            var category = CategoryManager.GetById(id);

            if (category == null)
            {
                return RedirectToAction("Index");
            }
            CategoryManager.Delete(id);

            return RedirectToAction("Index");
         }
        /*----------------------------------------------------------*/
        [HttpGet]
        public IActionResult Update(int id)
        {
            var categoryEditVM = CategoryManager.GetById(id);
            if (categoryEditVM == null)
            {   
                return RedirectToAction("Index");
            }
          
            return View(categoryEditVM);
        }
        /*----------------------------------------------------------*/
        [HttpPost]
        public IActionResult Update(CategoryEditVM categoryEditVM)
        {
            var categoryinDb = CategoryManager.GetById(categoryEditVM.Id);
            if (categoryinDb == null)
            {
                return RedirectToAction(nameof(Index));
            }
            CategoryManager.Update(categoryEditVM);

            return RedirectToAction(nameof(Index));
        }
    }
}
