using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /*------------------------------------------------------------------*/
        protected readonly AppDbContext _context;
        /*------------------------------------------------------------------*/
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        /*------------------------------------------------------------------*/
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        /*------------------------------------------------------------------*/
        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        /*------------------------------------------------------------------*/
        public void Create(T entity)
        {
            _context.Add(entity);
        }
        /*------------------------------------------------------------------*/

        public void Update(T entity)
        {
           
        }
        /*------------------------------------------------------------------*/

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }
        /*------------------------------------------------------------------*/

        //public int Save()
        //{
        //    return _context.SaveChanges();
        //}
        /*------------------------------------------------------------------*/
    }
}
