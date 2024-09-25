using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Add(T entity)
        {
            using var _context = new Context();
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            using var _context = new Context();
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            using var _context = new Context();
            _context.Update(entity);
            _context.SaveChanges();
        }

        public T GetByID(int id)
        {
            using var _context = new Context();
            return _context.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            using var _context = new Context();
            return _context.Set<T>().ToList();

        }

    }
}
