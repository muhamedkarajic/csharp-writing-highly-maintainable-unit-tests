using System;
using System.Collections.Generic;

namespace Domain
{
    public interface IRepository<T> : IDisposable
    {
        bool IsDisposed { get; }

        // Precondition: IsDisposed == false
        // Postcondition: return value != null
        // Postcondition: return.All(obj => obj != null)
        IEnumerable<T> GetAll();

        // Precondition: IsDisposed == false
        // Precondition: obj != null
        // Precondition: obj.Id == 0
        // Postcondition: GetAll().Any(x => object.ReferenceEquals(x, obj))
        void Add(T obj);

        // Precondition: IsDisposed == false
        // Precondition: obj != null
        // Postcondition: obj.Id == 0 || GetAll().All(x => x.Id != obj.Id)
        // Postcondition: obj.Id > 0 || GetAll().All(x => !object.ReferenceEquals(x, obj))
        void Remove(T obj);

        // Precondition: IsDisposed == false
        // Postcondition: return == null || return.Id == id
        T Find(int id);

        // Precondition: IsDisposed == false
        // Postcondition: GetAll().All(x => x.Id > 0)
        void Save();
    }
}
