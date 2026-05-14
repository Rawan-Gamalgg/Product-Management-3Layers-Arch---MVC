using Ecommerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL
{
    public interface ICategoryManager
    {
        public List<CategoryReadVM> GetAll();
        public CategoryReadVM? GetById(int id);
        public void Create(CategoryCreateVM category);
        public void Update(CategoryEditVM category);
        public void Delete(int id);
    }
}
