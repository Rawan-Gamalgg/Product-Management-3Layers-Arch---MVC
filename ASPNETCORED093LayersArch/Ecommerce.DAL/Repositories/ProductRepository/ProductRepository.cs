

using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetAllWithCategories()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public Product? GetByIdWithCategory(int id)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

        }

        /*------------------------------------------------------------------*/
        //get all categories to fill dropdownlist in create and update product
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();

        }
    }
}
