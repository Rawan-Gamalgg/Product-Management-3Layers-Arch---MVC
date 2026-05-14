using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ProductReadVM> GetAll()
        {
            var productsReadVM = _unitOfWork.ProductRepository.GetAllWithCategories()
                .Select(p => new ProductReadVM
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductDescription = p.Description,
                    Price = p.Price,
                    ImageURL = p.ImageURL,
                    Count = p.Count,
                    Category = p.Category.Name,
                     CategoryId = p.CategoryId,
                    ExpiryDate = p.ExpiryDate
                }).ToList();

            return productsReadVM.Count == 0 ? null : productsReadVM;
        }

        public ProductReadVM? GetById(int id)
        {
            var product = _unitOfWork.ProductRepository.GetByIdWithCategory(id);
            if (product == null) return null;

            return new ProductReadVM
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductDescription = product.Description,
                Price = product.Price,
                Count = product.Count,
                ImageURL = product.ImageURL,
                Category = product.Category.Name,
                 CategoryId = product.CategoryId,
                ExpiryDate = product.ExpiryDate
            };
        }
        public List<ProductReadVM> GetByCategory(int categoryId)
        {
            return _unitOfWork.ProductRepository.GetAllWithCategories()
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductReadVM
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductDescription = p.Description,
                    Price = p.Price,
                    ImageURL = p.ImageURL,
                    Category = p.Category.Name
                }).ToList();
        }

        // Controller saves the image file first, then passes the filename here
        public void Create(ProductCreateVM productCreateVM, string savedImageFileName)
        {
            var product = new Product
            {
                Name = productCreateVM.Name,
                Description = productCreateVM.Description,
                Count = productCreateVM.Count,
                Price = productCreateVM.Price,
                CategoryId = productCreateVM.CategoryId,
                ExpiryDate = productCreateVM.ExpiryDate,
                ImageURL = savedImageFileName
            };

            _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.Save();
        }

        public void Update(ProductEditVM productEditVM, string? newImageFileName)
        {
            var productInDb = _unitOfWork.ProductRepository.GetByIdWithCategory(productEditVM.Id);
            if (productInDb == null) return;

            productInDb.Name = productEditVM.Name;
            productInDb.Count = productEditVM.Count;
            productInDb.Price = productEditVM.Price;
            productInDb.CategoryId = productEditVM.CategoryId;
            productInDb.Description = productEditVM.Description;
            productInDb.ExpiryDate = productEditVM.ExpiryDate;

            // Only update image if a new one was uploaded and saved
            if (newImageFileName != null)
                productInDb.ImageURL = newImageFileName;

            _unitOfWork.ProductRepository.Update(productInDb);
            _unitOfWork.Save();
        }

        // Returns the old image filename so the controller can delete it from disk
        public void Delete(int id, out string? imageFileNameToDelete)
        {
            var productInDb = _unitOfWork.ProductRepository.GetById(id);
            imageFileNameToDelete = null;

            if (productInDb == null) return;

            imageFileNameToDelete = productInDb.ImageURL;
            _unitOfWork.ProductRepository.Delete(productInDb);
            _unitOfWork.Save();
        }

        public List<CategoryReadVM> GetAllCategories()
        {
            return _unitOfWork.CategoryRepository.GetAll()
                .Select(c => new CategoryReadVM
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
        }
    }
}