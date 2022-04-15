using System.Linq.Expressions;

namespace Domain.Interfaces.Finders
{
    public interface IFinder<T, K>
        where T : class
    {
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(K id);
        Task<T> GetByIdNoTrackingAsync(K id);
    }
}
