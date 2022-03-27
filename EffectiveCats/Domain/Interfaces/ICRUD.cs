using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    internal interface ICRUD<T>
        where T : class
    {
        abstract Task<bool> Create(T entity);
        abstract Task<T> Get(long id);
        Task<List<T>> GetAll();
        Task<T> Update(T entity);
        Task<bool> Delete(long id);
    }
}
