using GuestBook.Models;
using GuestBook.Models.Contracts;
using GuestBook.Services;
using GuestBook.Services.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.Controllers
{
    [Route("restaurants")]
    public class RestaurantController : ApiControllerBase<RestaurantContract, EditRestaurantContract, RestaurantFilterContract, Restaurant>
    {
        public RestaurantController(IEndpointService<RestaurantContract, EditRestaurantContract, RestaurantFilterContract, Restaurant> endpointService) : base(endpointService)
        {

        }
    }
}
