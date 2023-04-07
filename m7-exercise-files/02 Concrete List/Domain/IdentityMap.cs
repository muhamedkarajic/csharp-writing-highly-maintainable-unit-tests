using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    internal class IdentityMap<T> where T : class
    {
        private IDictionary<int, T> IdToObject { get; } = new Dictionary<int, T>();
        private Func<T, int> GetId { get; }
        private Func<IEnumerable<T>> LoadAll { get; }
        private Func<int, T> LoadById { get; }

        public IdentityMap(Func<T, int> getId, Func<IEnumerable<T>> loadAll,
                           Func<int, T> loadById)
        {
            this.GetId = getId;
            this.LoadAll = loadAll;
            this.LoadById = loadById;
        }

        public IEnumerable<T> GetAll() =>
            this.LoadAll().Select(this.PassThrough);

        public T GetById(int id)
        {
            T existingObj = null;
            if (this.IdToObject.TryGetValue(id, out existingObj))
                return existingObj;

            return ReadThrough(id);
        }

        private T ReadThrough(int id)
        {
            T loadedObject = this.LoadById(id);
            this.IdToObject[id] = loadedObject;
            return loadedObject;
        }

        private T PassThrough(T obj) =>
            PassThrought(obj, this.GetId(obj));

        private T PassThrought(T obj, int id)
        {
            T existingObj = null;
            if (this.IdToObject.TryGetValue(id, out existingObj))
                return existingObj;

            this.IdToObject[id] = obj;
            return obj;
        }
    }
}
