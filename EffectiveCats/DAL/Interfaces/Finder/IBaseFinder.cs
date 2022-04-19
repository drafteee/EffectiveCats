using System.Linq.Expressions;

namespace DAL.Interfaces.Finder
{
    public interface IBaseFinder<T>
        where T : class
    {
        Task<T?> GetAsync(Expression<Func<T,bool>> condition);
        Task<List<T>> GetAllAsync();
    }
}
