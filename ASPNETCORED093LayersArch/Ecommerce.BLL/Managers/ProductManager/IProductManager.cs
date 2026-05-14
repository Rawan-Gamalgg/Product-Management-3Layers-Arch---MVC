using Ecommerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ecommerce.BLL
{
    public interface IProductManager
    {
        List<ProductReadVM> GetAll();
        ProductReadVM? GetById(int id);
        void Create(ProductCreateVM productCreateVM, string savedImageFileName);
        void Update(ProductEditVM productEditVM, string? newImageFileName);
        void Delete(int id, out string? imageFileNameToDelete);
        /*----------------------------------------------------------*/
        List<ProductReadVM> GetByCategory(int categoryId);  // for the partial view

        List<CategoryReadVM> GetAllCategories();
    }
}