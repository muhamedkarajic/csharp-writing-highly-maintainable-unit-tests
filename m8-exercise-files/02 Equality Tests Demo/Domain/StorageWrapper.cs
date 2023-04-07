using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class StorageWrapper<T> : IRepository<T>
        where T : class
    {
        private IStorage<T> Storage { get; }
        private Func<T, int> GetId { get; }
        private IdentityMap<T> IdMap { get; }
        private IList<T> NewObjects { get; } = new List<T>();
        private HashSet<int> RemovedIds { get; } = new HashSet<int>();

        public StorageWrapper(IStorage<T> storage, Func<T, int> getId)
        {
            this.Storage = storage;
            this.GetId = getId;

            this.IdMap = new IdentityMap<T>(getId, this.Storage.GetAll, this.LoadById);
        }

        public IEnumerable<T> GetAll() =>
            this.IdMap
                .GetAll()
                .Where(obj => !this.RemovedIds.Contains(this.GetId(obj)))
                .Concat(this.NewObjects);

        public void Add(T obj) => this.NewObjects.Add(obj);

        public void Remove(T obj)
        {
            int id = this.GetId(obj);
            if (id > 0)
                this.RemovedIds.Add(id);
            else
                this.NewObjects.Remove(obj);
        }

        private T LoadById(int id) =>
            this.Storage.GetAll().SingleOrDefault(obj => this.GetId(obj) == id);

        public T Find(int id) => this.IdMap.GetById(id);

        public void Save()
        {
            foreach (T newObj in this.NewObjects)
                this.Storage.Add(newObj);

            foreach (T deletedObj in this.RemovedIds.Select(id => this.Find(id)))
                this.Storage.Remove(deletedObj);

            this.Storage.Save();
        }

        public void Dispose()
        {
        }
    }
}
