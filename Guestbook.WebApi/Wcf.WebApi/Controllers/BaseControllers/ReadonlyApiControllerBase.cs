using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Services;
using GuestBook.WebApi.Services.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Controllers.BaseControllers
{
    /// <summary>
    /// Базовый контроллер, позволяющий создать ресурс, доступный только для чтения документов
    /// </summary>
    [Authorize]
    public class ReadonlyApiControllerBase<TContract, TEditContract, TFilterContract, TDataModel>
        where TContract : class
        where TEditContract : class
        where TFilterContract : IFilterContract
    {
        protected IEndpointService<TContract, TEditContract, TFilterContract, TDataModel> _endpointService;

        public ReadonlyApiControllerBase(
            IEndpointService<TContract, TEditContract, TFilterContract, TDataModel> endpointService)
        {
            _endpointService = endpointService;
        }

        [HttpGet]
        public virtual async Task<ItemList<TContract>> List([FromQuery] TFilterContract filter)
        {
            return await _endpointService.ListAsync(filter);
        }

        [HttpGet("{id}")]
        public virtual async Task<TContract> Get(string id)
        {
            return await _endpointService.GetAsync(id);
        }
    }
}

