﻿using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        void Edit(T entity);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Delete(T entity);
    }
}
