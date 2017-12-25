using GuestBook.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GuestBook.TestCore
{
    /// <summary>
    /// Фальшивый репозиторий для работы с Identity.
    /// </summary>
    public class FakeRepository<T> : IRepository<T>
        where T : class
    {
        public IQueryable<T> Items { get; private set; }

        public Action<T> AddAction { get; set; }

        private List<T> _itemsSource;

        public List<T> ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                SetItemSource(value);
            }
        }

        public bool SaveChangesCalled { get; private set; }

        public bool UpdateCalled { get; private set; }

        public FakeRepository(List<T> items)
        {
            SetItemSource(items);
        }

        public FakeRepository()
        {
            SetItemSource(new List<T>());
        }

        private void SetItemSource(List<T> items)
        {
            _itemsSource = new List<T>(items);
            var data = _itemsSource.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IDbAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(data.Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Include(It.IsAny<string>()))
                .Returns<string>(path => mockSet.Object);

            Items = mockSet.Object;
        }

        public async Task AddAsync(T item)
        {
            ItemsSource.Add(item);
            if (AddAction != null)
            {
                AddAction(item);
            }
        }

        public async Task UpdateAsync(T item)
        {
            UpdateCalled = true;
        }

        public async Task RemoveAsync(T item)
        {
            ItemsSource.Remove(item);
        }

        public async Task<T> FindByIdAsync(string id, params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }
    }
}
