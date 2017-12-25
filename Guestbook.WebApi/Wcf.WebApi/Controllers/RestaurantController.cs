using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Controllers.BaseControllers;
using GuestBook.WebApi.Services;
using GuestBook.WebApi.Services.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Controllers
{
    [Route("/restaurants")]
    public class RestaurantController : ApiControllerBase<RestaurantContract, EditRestaurantContract, RestaurantFilterContract, Restaurant>
    {
        public RestaurantController(IEndpointService<RestaurantContract, EditRestaurantContract, RestaurantFilterContract, Restaurant> endpointService) : base(endpointService)
        {

        }

        [AllowAnonymous]
        public override async Task<ItemList<RestaurantContract>> List([FromQuery] RestaurantFilterContract filter)
        {
            return await base.List(filter);
        }
    }
}
