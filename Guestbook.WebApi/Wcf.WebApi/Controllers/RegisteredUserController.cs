using System.Threading.Tasks;
using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Controllers.BaseControllers;
using GuestBook.WebApi.Services;
using GuestBook.WebApi.Services.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.WebApi.Controllers
{
    [Route("/users")]
    public class RegisteredUserController : ApiControllerBase<RegisteredUserContract, EditRegisteredUserContract, RegisteredUserFilterContract, RegisteredUser>
    {
        public RegisteredUserController(IEndpointService<RegisteredUserContract, EditRegisteredUserContract, RegisteredUserFilterContract, RegisteredUser> endpointService) : base(endpointService)
        {

        }

        public override async Task<RegisteredUserContract> Add([FromBody] EditRegisteredUserContract model)
        {
            return await _endpointService.AddAsync(model);
        }
    }
}
