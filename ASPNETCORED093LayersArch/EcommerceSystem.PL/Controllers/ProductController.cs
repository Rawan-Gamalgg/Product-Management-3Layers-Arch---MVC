using Ecommerce.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IActionResult Index()
        {
            var products = _productManager.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var product = _productManager.GetById(id);
            if (product == null) return RedirectToAction(nameof(Index));
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new ProductCreateVM
            {
                Categoryies = GetCategoriesSelectList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCreateVM productCreateVM)
        {
            if (!ModelState.IsValid)
            {
                productCreateVM.Categoryies = GetCategoriesSelectList();
                return View(productCreateVM);
            }

            string fileName = SaveImage(productCreateVM.Image);
            _productManager.Create(productCreateVM, fileName);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _productManager.GetById(id);
            if (product == null) return RedirectToAction(nameof(Index));

            var vm = new ProductEditVM
            {
                Id = product.ProductId,
                Name = product.ProductName,
                Count = product.Count,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category,
                Description = product.ProductDescription,
                ImageURL = product.ImageURL,
                ExpiryDate = product.ExpiryDate,
                Categories = GetCategoriesSelectList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProductEditVM productEditVM)
        {
            if (!ModelState.IsValid)
            {
                productEditVM.Categories = GetCategoriesSelectList();
                return View(productEditVM);
            }

            string? newFileName = null;

            if (productEditVM.Image != null && productEditVM.Image.Length > 0)
            {
                // Delete old image from disk before saving the new one
                DeleteImageFile(productEditVM.ImageURL);
                newFileName = SaveImage(productEditVM.Image);
            }

            _productManager.Update(productEditVM, newFileName);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _productManager.Delete(id, out string? imageFileName);

            // File deletion stays in PL — BLL only returns the filename
            DeleteImageFile(imageFileName);

            return RedirectToAction(nameof(Index));
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

        private string SaveImage(IFormFile imageFile)
        {
            var uniqueName = $"{Guid.NewGuid()}_{Path.GetExtension(imageFile.FileName)}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "Images", "Products");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, uniqueName);
            using (var stream = new FileStream(filePath, FileMode.Create))
                imageFile.CopyTo(stream);

            return uniqueName;
        }

        private void DeleteImageFile(string? imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName)) return;

            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "Images", "Products", imageFileName);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
    }
}