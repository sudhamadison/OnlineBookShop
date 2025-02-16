using System.Linq.Expressions;

namespace RealTimeProject.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        IEnumerable<T> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);

        T Get(Expression<Func<T,bool>>filter,string? includeProperties=null);
        IEnumerable<T> GetList(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);

    }
   
}
