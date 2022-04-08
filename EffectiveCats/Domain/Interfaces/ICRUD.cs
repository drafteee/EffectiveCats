namespace Domain.Interfaces
{
    public interface ICRUD<T>
        where T : class
    {
        Task<bool> Create(T entity);
        Task<T> Get(long id);
        Task<List<T>> GetAll();
        Task<T> Update(T entity);
        Task<long> Delete(long id);
    }
}
