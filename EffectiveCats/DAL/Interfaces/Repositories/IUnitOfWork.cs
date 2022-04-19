namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> Complete();
    }
}
