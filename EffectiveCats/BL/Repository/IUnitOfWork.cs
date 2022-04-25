namespace BL.Repository
{
    public interface IUnitOfWork
    {
        Task<int> Complete();
    }
}
