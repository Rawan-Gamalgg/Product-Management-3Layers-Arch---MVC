using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository ProductRepository{ get; }

        public ICategoryRepository CategoryRepository { get; }
        private readonly AppDbContext _context;
        public UnitOfWork(ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            AppDbContext context)
        {
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            _context = context;

        }

        public int Save()
        {
           return _context.SaveChanges();        }
    }
}
