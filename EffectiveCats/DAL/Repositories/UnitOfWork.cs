using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext _context;
        public UnitOfWork(MainContext context)
        {
            _context = context;
        }
        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
