using BL.Repository;

namespace SQLiteDAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext _context;
        public UnitOfWork(MainContext context)
        {
            _context = context;
        }
        //Transactiobn
        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }
    }
}
