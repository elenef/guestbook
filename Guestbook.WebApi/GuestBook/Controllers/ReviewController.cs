using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Services;
using GuestBook.WebApi.Services.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.WebApi.Controllers
{
    [Route("/reviews")]
    public class ReviewController : ApiControllerBase<ReviewContract, EditReviewContract, ReviewFilterContract, Review>
    {
        public ReviewController(IEndpointService<ReviewContract, EditReviewContract, ReviewFilterContract, Review> endpointService) : base(endpointService)
        {

        }
    }
}
