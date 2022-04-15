using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository<T, K>
    {
        T Edit(T entity);
        Task<T> EditByIdAsync(T entity, K id, List<string> properties = null);
        Task<K> DeleteByIdAsync(K id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        K Delete(T entity);
    }
}
