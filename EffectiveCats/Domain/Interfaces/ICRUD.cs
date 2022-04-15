namespace Domain.Interfaces
{
    public interface ICRUD<T, K>
        where T : class
    {
        Task<K> Create(T entity);
        Task<T> Get(K id);
        Task<List<T>> GetAll();
        Task<T> Update(T entity);
        Task<K> Delete(K id);
    }
}
