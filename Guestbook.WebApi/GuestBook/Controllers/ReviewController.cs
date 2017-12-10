using GuestBook.Models;
using GuestBook.Models.Contracts;
using GuestBook.Services;
using GuestBook.Services.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.Controllers
{
    [Route("/reviews")]
    public class ReviewController : ApiControllerBase<ReviewContract, EditReviewContract, ReviewFilterContract, Review>
    {
        public ReviewController(IEndpointService<ReviewContract, EditReviewContract, ReviewFilterContract, Review> endpointService) : base(endpointService)
        {

        }
    }
}
