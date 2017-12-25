using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Services;
using GuestBook.WebApi.Services.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Controllers.BaseControllers
{
    public class ApiControllerBase<TContract, TEditContract, TFilterContract, TDataModel> :
        ReadonlyApiControllerBase<TContract, TEditContract, TFilterContract, TDataModel>
        where TContract : class
        where TEditContract : class
        where TFilterContract : IFilterContract
    {
        public ApiControllerBase(
            IEndpointService<TContract, TEditContract, TFilterContract, TDataModel> endpointService)
            : base(endpointService)
        {
        }

        [HttpPost]
        public virtual async Task<TContract> Add([FromBody] TEditContract model)
        {
            return await _endpointService.AddAsync(model);
        }

        [HttpPut("{id}")]
        public virtual async Task<TContract> Update(string id, [FromBody] TEditContract model)
        {
            return await _endpointService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public virtual async Task<RemovedItemContract> Delete(string id)
        {
            await _endpointService.DeleteAsync(id);
            return new RemovedItemContract { Id = id };
        }
    }
}

