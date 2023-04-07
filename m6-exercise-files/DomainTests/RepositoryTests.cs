using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Xunit;

namespace DomainTests
{
    public abstract class RepositoryTests<T>
    {
        public RepositoryTests(Action emptyStorage, Action resetIdSequence)
        {
            emptyStorage();
            resetIdSequence();
        }

        [Fact]
        public void GetAll_ReturnsNonNull()
        {
            Assert.NotNull(this.CreateSut().GetAll());
        }

        [Fact]
        public void GetAll_EmptyStorage_ReturnsEmptySequence()
        {
            Assert.False(this.CreateSut().GetAll().Any());
        }

        [Fact]
        public void Find_ReturnsSameReferenceAsGetAll()
        {
            T obj = this.SampleData.First();
            this.InitializeStorage(new[] {obj});
            IRepository<T> setupRepo = this.CreateSut();
            setupRepo.Add(obj);
            setupRepo.Save();

            IRepository<T> repo = this.CreateSut();
            T findObj = repo.Find(1);
            T getAllObj = repo.GetAll().First();

            Assert.Same(findObj, getAllObj);
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void GetAll_ObjectPassedToAdd_ResultContainsThatObject(int sequenceLength, int targetIndex)
        {
            T[] sequence = this.SampleData.Take(sequenceLength).ToArray();
            this.InitializeStorage(sequence);

            IRepository<T> repo = this.CreateSut();

            T targetObj = sequence[targetIndex];
            IEnumerable<T> actualObject = repo.GetAll().Where(obj => object.ReferenceEquals(obj, targetObj));

            Assert.True(actualObject.Any());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void GetAll_AddCalledOnce_SequenceContainsOneElementMoreThanBefore(int sequenceLength)
        {
            this.InitializeStorage(this.SampleData.Take(sequenceLength));

            IRepository<T> repo = this.CreateSut();
            Assert.Equal(sequenceLength + 1, repo.GetAll().Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void GetAll_AddedOneObjectAndSaved_NewRepoReturnsThatObject(int initialCount)
        {
            IEnumerable<T> data = this.SampleData.Take(initialCount + 1).ToList();
            this.InitializeStorage(data.Take(initialCount));

            T obj = data.ElementAt(initialCount);

            IRepository<T> repo = this.CreateSut();
            repo.Add(obj);
            repo.Save();

            IRepository<T> newRepo = this.CreateSut();
            IEnumerable<T> actualObj = newRepo.GetAll().Where(x => this.MemberwiseEqual(x, obj));

            Assert.True(actualObj.Any());
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void GetAll_RemovedOneObject_DoesNotReturnThatObject(int count, int indexToRemove)
        {
            T[] data = this.SampleData.Take(count).ToArray();
            this.InitializeStorage(data);

            T obj = data[indexToRemove];

            IRepository<T> repo = this.CreateSut();
            repo.Remove(obj);
            IEnumerable<T> actualObj = repo.GetAll().Where(x => this.MemberwiseEqual(x, obj));

            Assert.False(actualObj.Any());
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void GetAll_RemovedOneObject_ReturnsSequenceShorterByOneElement(int count, int indexToRemove)
        {
            T[] data = this.SampleData.Take(count).ToArray();
            this.InitializeStorage(data);

            T obj = data[indexToRemove];

            IRepository<T> repo = this.CreateSut();
            repo.Remove(obj);

            Assert.Equal(count - 1, repo.GetAll().Count());
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void GetAll_RemovedOneObjectAndSaved_NewRepoDoesNotReturnThatObject(int count, int indexToRemove)
        {
            T[] data = this.SampleData.Take(count).ToArray();
            this.InitializeStorage(data);

            T obj = data[indexToRemove];

            IRepository<T> repo = this.CreateSut();
            repo.Remove(obj);
            repo.Save();

            IRepository<T> newRepo = this.CreateSut();
            IEnumerable<T> actualObj = newRepo.GetAll().Where(x => this.MemberwiseEqual(x, obj));

            Assert.False(actualObj.Any());
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void Find_ReturnsExpectedObject(int count, int targetIndex)
        {
            T[] data = this.SampleData.Take(count).ToArray();
            this.InitializeStorage(data);

            T expectedObj = data[targetIndex];
            int id = targetIndex + 1;

            IRepository<T> repo = this.CreateSut();
            T actualObj = repo.Find(id);

            Assert.True(this.MemberwiseEqual(expectedObj, actualObj));
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void Find_TwoCallsWithSameIdReturnSameReference(int count, int targetIndex)
        {
            T[] data = this.SampleData.Take(count).ToArray();
            this.InitializeStorage(data);

            int id = targetIndex + 1;

            IRepository<T> repo = this.CreateSut();
            T expectedObj = repo.Find(id);
            T actualObj = repo.Find(id);

            Assert.Same(expectedObj, actualObj);
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void Find_ReturnsSameReferenceAsGetAll(int count, int targetIndex)
        {
            T[] data = this.SampleData.Take(count).ToArray();
            this.InitializeStorage(data);

            T targetObj = data[targetIndex];
            int id = targetIndex + 1;

            IRepository<T> repo = this.CreateSut();
            T expectedObj = repo.GetAll().Single(x => this.MemberwiseEqual(x, targetObj));
            T actualObj = repo.Find(id);

            Assert.Same(expectedObj, actualObj);
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void Save_ExistingObjectModified_NewRepositoryGetAllReturnsModifiedObject(int count, int targetIndex)
        {
            T[] data = this.SampleData.Take(count).ToArray();
            this.InitializeStorage(data);

            T targetObj = data[targetIndex];
            int id = targetIndex + 1;

            IRepository<T> repo = this.CreateSut();
            T expectedObj = repo.Find(id);

            this.Mutate(expectedObj);
            repo.Save();

            IRepository<T> newRepo = this.CreateSut();
            IEnumerable<T> actualObj = newRepo.GetAll().Where(x => this.MemberwiseEqual(x, expectedObj));

            Assert.True(actualObj.Any());
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void Save_ExistingObjectModified_NewRepositoryFindReturnsModifiedObject(int count, int targetIndex)
        {
            T[] data = this.SampleData.Take(count).ToArray();
            this.InitializeStorage(data);

            T targetObj = data[targetIndex];
            int id = targetIndex + 1;

            IRepository<T> repo = this.CreateSut();
            T expectedObj = repo.Find(id);

            this.Mutate(expectedObj);
            repo.Save();

            IRepository<T> newRepo = this.CreateSut();
            T actualObj = newRepo.Find(id);

            Assert.True(this.MemberwiseEqual(expectedObj, actualObj));
        }
        // Add(x), Save() => x.id > 0
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(17)]
        public void Save_ObjectAdded_IdBecomesPositiveAfterSave(int count)
        {
            IEnumerable<T> data = this.SampleData.Take(count + 1).ToList();
            this.InitializeStorage(data.Take(count));
            T newObj = data.ElementAt(count);

            IRepository<T> repo = this.CreateSut();
            repo.Add(newObj);
            repo.Save();

            Assert.True(this.GetId(newObj) > 0);
        }

        protected abstract IRepository<T> CreateSut();

        private void InitializeStorage(IEnumerable<T> sequence)
        {
            IRepository<T> repo = this.CreateSut();
            foreach (T obj in sequence)
                repo.Add(obj);
            repo.Save();
        }

        protected abstract IEnumerable<T> SampleData { get; }
        protected abstract bool MemberwiseEqual(T a, T b);
        protected abstract void Mutate(T expectedObj);
        protected abstract int GetId(T newObj);
    }
}
