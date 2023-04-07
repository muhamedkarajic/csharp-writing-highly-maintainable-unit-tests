using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Contracts;

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

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> result = this.IdMap
                .GetAll()
                .Where(obj => !this.RemovedIds.Contains(this.GetId(obj)))
                .Concat(this.NewObjects);

            Contract.Ensures(() => result != null, "Returned null.");
            result.EnsuresAll(x => x != null, "Returned sequence containing null.");

            return result;
        }

        public void Add(T obj)
        {
            Contract.Requires(() => obj != null, "Adding null.");
            Contract.Requires(() => this.GetId(obj) == 0, "Adding persisted object.");

            this.NewObjects.Add(obj);

            GetAll().EnsuresAny(x => object.ReferenceEquals(x, obj), "Object not added.");
        }

        public void Remove(T obj)
        {
            Contract.Requires(() => obj != null, "Removing null.");

            int id = this.GetId(obj);
            if (id > 0)
                this.RemovedIds.Add(id);
            else
                this.NewObjects.Remove(obj);

            GetAll().EnsuresAll(x => GetId(obj) == 0 || GetId(x) != GetId(obj), "Persisted object not removed.");
            GetAll().EnsuresAll(x => GetId(obj) > 0 || !object.ReferenceEquals(x, obj), "New object not removed.");
        }

        private T LoadById(int id) =>
            this.Storage.GetAll().SingleOrDefault(obj => this.GetId(obj) == id);

        public T Find(int id)
        {
            T result = this.IdMap.GetById(id);

            Contract.Ensures(() => result == null || GetId(result) == id, "Returned incorrect object.");
            return result;
        }

        public void Save()
        {
            foreach (T newObj in this.NewObjects)
                this.Storage.Add(newObj);

            foreach (T deletedObj in this.RemovedIds.Select(id => this.Find(id)))
                this.Storage.Remove(deletedObj);

            this.Storage.Save();

            GetAll().EnsuresAll(x => GetId(x) > 0, "Identity not set on persisted object.");
        }

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            this.IsDisposed = true;
        }
    }
}
