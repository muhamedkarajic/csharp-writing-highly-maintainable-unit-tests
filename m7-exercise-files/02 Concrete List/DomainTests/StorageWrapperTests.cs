using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DomainTests
{
    public class StorageWrapperTests: RepositoryTests<StorageWrapperTests.Data>
    {

        private MemoryStorage<Data> FakeStorage { get; } = 
            new MemoryStorage<Data>((obj, id) => obj.Id = id);

        public class Data
        {
            public int Id { get; set; }
            public int Content { get; set; }
        }

        protected override IRepository<Data> CreateSut() => 
            new StorageWrapper<Data>(this.FakeStorage, x => x.Id);

        protected override void InitializeStorage(IEnumerable<Data> sequence)
        {
            this.FakeStorage.Populate(sequence);
        }

        protected override void EmptyStorage()
        {
            this.FakeStorage.Clear();
        }

        protected override void ResetIdSequence()
        {
            this.FakeStorage.ResetId();
        }

        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data() { Content = content });

        protected override bool MemberwiseEqual(Data a, Data b) => a.Content == b.Content;

        protected override void Mutate(Data expectedObj) => expectedObj.Content += 1;

        protected override int GetId(Data newObj) => newObj.Id;
    }
}
