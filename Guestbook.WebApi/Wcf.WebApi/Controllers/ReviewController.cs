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
    [Route("/reviews")]
    public class ReviewController : ApiControllerBase<ReviewContract, EditReviewContract, ReviewFilterContract, Review>
    {
        public ReviewController(IEndpointService<ReviewContract, EditReviewContract, ReviewFilterContract, Review> endpointService) : base(endpointService)
        {

        }

        [AllowAnonymous]
        public override async Task<ItemList<ReviewContract>> List([FromQuery] ReviewFilterContract filter)
        {
            return await base.List(filter);
        }

        [AllowAnonymous]
        public override async Task<ReviewContract> Add([FromBody] EditReviewContract model)
        {
            return await base.Add(model);
        }

        [AllowAnonymous]
        public override async Task<ReviewContract> Update(string id, [FromBody] EditReviewContract model)
        {
            return await base.Update(id, model);
        }
    }
}
