using System.Linq.Expressions;

namespace HM.Product.Data
{
    public interface IProductRepository<T> where T : class
    {
        Task<bool> Save(T entity);
        Task<T> Get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? orderByDesc = null);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? orderByDesc = null);
    }
}