using Lab4.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) 
        {
        }

        public IEnumerable<Category> GetAllWithProducts()
        {
            return _context.Categories.Include(c => c.Products).ToList();
        }

        public Category? GetByIdWithProducts(int id)
        {
            return _context.Categories.Include(c => c.Products).FirstOrDefault(c=>c.Id == id);
        }

        /*------------------------------------------------------------------*/
    

       


    }
}
