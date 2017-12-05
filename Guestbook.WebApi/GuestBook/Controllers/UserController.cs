using GuestBook.Models;
using GuestBook.Models.Contracts;
using GuestBook.Services;
using GuestBook.Services.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.Controllers
{
    [Route("users")]
    public class UserController : ApiControllerBase<UserContract, EditUserContract, UserFilterContract, User>
    {
        public UserController(IEndpointService<UserContract, EditUserContract, UserFilterContract, User> endpointService) : base(endpointService)
        {

        }
    }

}

