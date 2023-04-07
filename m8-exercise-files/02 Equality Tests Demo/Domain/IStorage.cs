using System.Collections.Generic;

namespace Domain
{
    public interface IStorage<T>
    {
        IEnumerable<T> GetAll();
        void Add(T obj);
        void Save();
        void Remove(T deletedObj);
    }
}