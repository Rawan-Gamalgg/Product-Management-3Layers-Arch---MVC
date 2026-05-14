using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        /*------------------------------------------------------------------*/
        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /*------------------------------------------------------------------*/
        public List<CategoryReadVM> GetAll()
        {
            var categories = _unitOfWork.CategoryRepository.GetAllWithProducts();
            return categories.Select(c => new CategoryReadVM
            {
                Id = c.Id,
                Name = c.Name,
                ProductsCount = c.Products.Count
            })
                .ToList();

        }

        public CategoryReadVM? GetById(int id)
        {
            var categoryWithProducts = _unitOfWork.CategoryRepository.GetByIdWithProducts(id);
            if (categoryWithProducts == null)
            {
                return null;
            }
           
            var categoryWithProductsRVM = new CategoryReadVM
            {
                Id = categoryWithProducts.Id,
                Name = categoryWithProducts.Name,
                ProductsCount = categoryWithProducts.Products.Count,
                Products = categoryWithProducts.Products.Select(p => new ProductReadVM
                {
                    ProductName = p.Name,
                    ProductDescription = p.Description,
                    Price = p.Price,
                    Count = p.Count
                }).ToList()
            };
            return categoryWithProductsRVM;
        }
        public void Create(CategoryCreateVM categoryCreateVM)
        {
            var category = new Category
            {
                Name = categoryCreateVM.Name
            };
            _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var Category = _unitOfWork.CategoryRepository.GetById(id);
            if (Category == null)
            {
                return;
            }
            _unitOfWork.CategoryRepository.Delete(Category);
            _unitOfWork.Save();
        }

        public void Update(CategoryEditVM categoryEditVM)
        {
            var category = _unitOfWork.CategoryRepository.GetById(categoryEditVM.Id);
            if (category == null)
            {
                return;
            }

            category.Name = categoryEditVM.Name;
            _unitOfWork.Save();
        }
    }
}
