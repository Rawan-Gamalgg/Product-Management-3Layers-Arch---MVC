using Lab4.Models;

namespace Ecommerce.DAL
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        /*------------------------------------------------------------------*/
        IEnumerable<Product> GetAllWithCategories();
        Product? GetByIdWithCategory(int id);


    }
}
