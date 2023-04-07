using System.Collections.Generic;
using System.Linq;
using Domain;
using Xunit;

namespace DomainTests
{
    public abstract class RepositoryTests<T>
    {
        protected RepositoryTests()
        {
            this.EmptyStorage();
            this.ResetIdSequence();
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
        public void GetAll_ReturnsSameReferenceAsFind()
        {
            this.InitializeStorage(this.SampleData.Take(1));

            IRepository<T> repo = this.CreateSut();
            int idToFind = 1;
            T expectedObj = repo.Find(idToFind);
            T actualObj = repo.GetAll().First();

            Assert.Same(expectedObj, actualObj);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(17)]
        public void GetAll_ObjectPassedToAdd_ResultContainsThatObject(int initialCount)
        {
            IEnumerable<T> sequence = this.SampleData.Take(initialCount + 1).ToList();
            this.InitializeStorage(sequence.Take(initialCount));

            IRepository<T> repo = this.CreateSut();

            T expectedObject = sequence.ElementAt(initialCount);
            repo.Add(expectedObject);

            IEnumerable<T> actualObject = repo.GetAll().Where(obj => object.ReferenceEquals(obj, expectedObject));

            Assert.True(actualObject.Any());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void GetAll_AddCalledOnce_SequenceContainsOneElementMoreThanBefore(int initialCount)
        {
            IEnumerable<T> data = this.SampleData.Take(initialCount + 1).ToList();
            this.InitializeStorage(data.Take(initialCount));
            T newObj = data.ElementAt(initialCount);

            IRepository<T> repo = this.CreateSut();
            repo.Add(newObj);
            Assert.Equal(initialCount + 1, repo.GetAll().Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void GetAll_AddedOneObjectAndSaved_NewRepoReturnsThatObject(int initialCount)
        {
            IEnumerable<T> data = this.SampleData.Take(initialCount + 1);
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
        public void GetAll_RemovedOneObject_DoesNotReturnThatObject(int storageCount, int indexToRemove)
        {
            T[] data = this.SampleData.Take(storageCount).ToArray();
            this.InitializeStorage(data);

            int idToRemove = indexToRemove + 1;

            IRepository<T> repo = this.CreateSut();
            T objToRemove = repo.Find(idToRemove);
            repo.Remove(objToRemove);
            IEnumerable<T> actualObj = repo.GetAll().Where(x => this.MemberwiseEqual(x, objToRemove));

            Assert.False(actualObj.Any());
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void GetAll_RemovedOneObject_ReturnsSequenceShorterByOneElement(int storageCount, int indexToRemove)
        {
            T[] data = this.SampleData.Take(storageCount).ToArray();
            this.InitializeStorage(data);

            int idToRemove = indexToRemove + 1;

            IRepository<T> repo = this.CreateSut();
            T objToRemove = repo.Find(idToRemove);
            repo.Remove(objToRemove);

            Assert.Equal(storageCount - 1, repo.GetAll().Count());
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void GetAll_RemovedOneObjectAndSaved_NewRepoDoesNotReturnThatObject(int storageCount, int indexToRemove)
        {
            T[] data = this.SampleData.Take(storageCount).ToArray();
            this.InitializeStorage(data);

            int idToRemove = indexToRemove + 1;

            IRepository<T> repo = this.CreateSut();
            T obj = repo.Find(idToRemove);
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
        public void Find_ReturnsExpectedObject(int storageCount, int targetIndex)
        {
            T[] data = this.SampleData.Take(storageCount).ToArray();
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
        public void Find_TwoCallsWithSameIdReturnSameReference(int storageCount, int targetIndex)
        {
            T[] data = this.SampleData.Take(storageCount).ToArray();
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
        public void Find_ReturnsSameReferenceAsGetAll(int storageCount, int targetIndex)
        {
            T[] data = this.SampleData.Take(storageCount).ToArray();
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
        public void Save_ExistingObjectModified_NewRepositoryGetAllReturnsModifiedObject(int storageCount, int targetIndex)
        {
            T[] data = this.SampleData.Take(storageCount).ToArray();
            this.InitializeStorage(data);

            T targetObj = data[targetIndex];

            IRepository<T> repo = this.CreateSut();
            T expectedObj = repo.GetAll().Single(x => this.MemberwiseEqual(x, targetObj));

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
        public void Save_ExistingObjectModified_NewRepositoryFindReturnsModifiedObject(int storageCount, int targetIndex)
        {
            T[] data = this.SampleData.Take(storageCount).ToArray();
            this.InitializeStorage(data);

            int id = targetIndex + 1;

            IRepository<T> repo = this.CreateSut();
            T expectedObj = repo.Find(id);

            this.Mutate(expectedObj);
            repo.Save();

            IRepository<T> newRepo = this.CreateSut();
            T actualObj = newRepo.Find(id);

            Assert.True(this.MemberwiseEqual(expectedObj, actualObj));
        }

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

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void Remove_AddThenRemove_GetAllDoesNotContainAddedObject(int initialCount)
        {
            IEnumerable<T> data = this.SampleData.Take(initialCount + 1).ToList();
            this.InitializeStorage(data.Take(initialCount));
            T newObj = data.ElementAt(initialCount);

            IRepository<T> repo = this.CreateSut();
            repo.Add(newObj);
            repo.Remove(newObj);

            IEnumerable<T> actualObject = repo.GetAll().Where(obj => object.ReferenceEquals(obj, newObj));

            Assert.False(actualObject.Any());
        }

        protected abstract IRepository<T> CreateSut();

        protected abstract void EmptyStorage();
        protected abstract void ResetIdSequence();

        protected abstract IEnumerable<T> SampleData { get; }

        protected abstract void InitializeStorage(IEnumerable<T> data);
        protected abstract bool MemberwiseEqual(T a, T b);
        protected abstract void Mutate(T expectedObj);
        protected abstract int GetId(T newObj);
    }
}
