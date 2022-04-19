namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Complete();
    }
}
