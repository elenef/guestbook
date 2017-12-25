using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Controllers.BaseControllers;
using GuestBook.WebApi.Services;
using GuestBook.WebApi.Services.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.WebApi.Controllers
{
    [Route("/restaurants")]
    public class RestaurantController : ApiControllerBase<RestaurantContract, EditRestaurantContract, RestaurantFilterContract, Restaurant>
    {
        public RestaurantController(IEndpointService<RestaurantContract, EditRestaurantContract, RestaurantFilterContract, Restaurant> endpointService) : base(endpointService)
        {

        }
    }
}
