using Lab4.Models;

namespace Ecommerce.DAL
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        /*------------------------------------------------------------------*/
        IEnumerable<Category> GetAllWithProducts();
        Category? GetByIdWithProducts(int id);

    }
}

