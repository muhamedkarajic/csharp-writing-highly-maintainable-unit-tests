using System;
using System.Collections.Generic;

namespace Domain
{
    public interface IRepository<T>
    {
        // Returns non-null
        // On empty storage returns empty sequence
        // x in storage, Find(x.id) => GetAll() contains same x reference
        IEnumerable<T> GetAll();
        // After Add(x), GetAll() result contains x
        // N objects in storage and Add(x) => GetAll() returns N+1 objects
        // x not in storage, Add(x), Save() => new repository GetAll() contains x
        void Add(T obj);
        // x in storage, Remove(x) => GetAll() result does not contain x
        // N objects in storage and Remove(x) => GetAll() returns N-1 objects
        // x in storage, Remove(x), Save() => GetAll() on new repository does not contain x
        void Remove(T obj);
        // x in storage => Find(x.id) return object x
        // x in storage => Find(x.id) reference equals another Find(x.id)
        // x in storage, GetAll() => Find(x.id) returns same x reference as GetAll()
        T Find(int id);
        // x in storage, GetAll(), modify x, Save() => new repository GetAll() contains modified x
        // x in storage, Find(x.id), modify x, Save() => new repository GetAll() contains modified x
        // Add(x), Save() => x.id > 0
        void Save();
    }
}
