using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Services;
using GuestBook.WebApi.Services.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.WebApi.Controllers
{
    [Route("/users")]
    public class UserController : ApiControllerBase<UserContract, EditUserContract, UserFilterContract, User>
    {
        public UserController(IEndpointService<UserContract, EditUserContract, UserFilterContract, User> endpointService) : base(endpointService)
        {

        }
    }

}

