using Microsoft.EntityFrameworkCore;
using RealTimeProject.DAL.Interfaces;
using RealTimeProject.Models;
using System.Linq;
using System.Linq.Expressions;

namespace RealTimeProject.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        internal DbSet<T> dbSet;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }



        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            if(entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
                _context.Set<T>().Update(entity);
                _context.SaveChanges();
            }
            else
            { _context.Set<T>().Update(entity); }
           
        }


       public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query;
            query = dbSet;
            query=query.Where(filter);
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
                
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
           IQueryable<T> query;
            query = dbSet;
            if(filter!= null)
            {
                query = query.Where(filter);
            }
           
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }

            }
            return query.ToList();

        }


    }
}
