using GuestBook.Data;
using GuestBook.TestCore;
using Ninject;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GuestBook.WebApi.Tests.Facts
{
    public class RepositoryFacts : AFacts
    {
        //[Fact]
        public async Task ModelCanBeAdded()
        {
            var fixture = GetFixture<ExampleFixture>();
            var model = fixture.CreateModel();

            var repository = _kernel.Get<Repository<ExampleModel>>();
            await repository.AddAsync(model);

            var addedModel = await repository.Items
                .Where(m => m.Id == model.Id)
                .FirstOrDefaultAsync();
            Assert.NotNull(addedModel);
            Assert.Equal(model.Age, addedModel.Age);
            Assert.Equal(model.Name, addedModel.Name);
        }

        //[Fact]
        public async Task ModelCanBeRemoved()
        {
            var fixture = GetFixture<ExampleFixture>();
            var model = await fixture.AddModelAsync();

            var repository = _kernel.Get<Repository<ExampleModel>>();
            await repository.RemoveAsync(model);

            var modelList = await repository.Items
                .Where(m => m.Id == model.Id)
                .ToListAsync();
            Assert.Empty(modelList);
        }

        //[Fact]
        public async Task ModelCanBeUpdated()
        {
            var fixture = GetFixture<ExampleFixture>();
            var model = await fixture.AddModelAsync();

            var repository = _kernel.Get<Repository<ExampleModel>>();
            model.Name = "new name";
            await repository.UpdateAsync(model);

            var addedModel = await repository.Items
                .Where(m => m.Id == model.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            Assert.NotNull(addedModel);
            Assert.Equal(model.Name, addedModel.Name);
        }

        //[Fact]
        public async Task ItemListCanBeFiltered()
        {
            var fixture = GetFixture<ExampleFixture>();
            var first = await fixture.AddModelAsync(name: "First name");
            var second = await fixture.AddModelAsync(name: "Second name");
            var third = await fixture.AddModelAsync(name: "Vasya");

            var repository = _kernel.Get<Repository<ExampleModel>>();
            var resultList = await repository.Items
                .Where(m => m.Name.Contains("NaME"))
                .ToListAsync();

            Assert.NotNull(resultList);
            Assert.Equal(2, resultList.Count);
            Assert.True(resultList.Any(m => m.Id == first.Id));
            Assert.True(resultList.Any(m => m.Id == second.Id));
        }
    }
}
