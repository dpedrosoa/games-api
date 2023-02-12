using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GamesAPI.Data.Interfaces
{
    /// <summary>
    /// Interface for the generic Repository
    /// </summary>
    public interface IRepository<T> where T : class
    {
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool disabledTracking = true);


        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
