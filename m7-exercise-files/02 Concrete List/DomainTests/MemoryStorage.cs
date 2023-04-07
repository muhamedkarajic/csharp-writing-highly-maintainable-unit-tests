using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DomainTests
{
    internal class MemoryStorage<T>: IStorage<T>
    {

        private IList<T> Data { get; set; } = new List<T>();

        private int LastId { get; set; }
        private Action<T, int> SetId { get; }

        public MemoryStorage(Action<T, int> setId)
        {
            this.SetId = setId;
        }

        public IEnumerable<T> GetAll() => this.Data;

        public void Add(T obj)
        {
            this.LastId += 1;
            this.SetId(obj, this.LastId);
            this.Data.Add(obj);
        }

        public void Save() { }

        public void Remove(T deletedObj)
        {
            int position = 
                this.Data
                    .Select((obj, index) => new
                        {
                            Index = index,
                            Equal = object.ReferenceEquals(obj, deletedObj)
                        })
                    .Single(item => item.Equal)
                    .Index;
            this.Data.RemoveAt(position);
        }

        public void Populate(IEnumerable<T> sequence)
        {
            this.Data = new List<T>();
            foreach (T obj in sequence)
                this.Add(obj);
            this.Save();
        }

        public void Clear()
        {
            this.Data = new List<T>();
        }

        public void ResetId()
        {
            this.LastId = 0;
        }
    }
}