using System;
using System.Collections.Generic;

namespace Domain
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll();
        void Add(T obj);
        void Remove(T obj);
        T Find(int id);
        void Save();
    }
}
