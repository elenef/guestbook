using GuestBook.Data;
using GuestBook.TestCore;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Tests.Facts
{
    public class ExampleFixture
    {
        private IRepository<ExampleModel> _repository;

        public ExampleFixture(IRepository<ExampleModel> repository)
        {
            _repository = repository;
        }

        public async Task<ExampleModel> AddModelAsync(string name = "Default name", int age = 10)
        {
            var model = CreateModel(name, age);
            await _repository.AddAsync(model);
            return model;
        }

        public ExampleModel CreateModel(string name = "Default name", int age = 10)
        {
            return new ExampleModel { Name = name, Age = age };
        }
    }
}
