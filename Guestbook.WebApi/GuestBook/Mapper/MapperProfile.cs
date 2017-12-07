using AutoMapper;
using GuestBook.Helpers;
using GuestBook.Models;
using GuestBook.Models.Contracts;

namespace GuestBook.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserContract>();

            CreateMap<Restaurant, RestaurantContract>()
                .ForMember(d => d.ReviewIds, opt => opt.Ignore());

            CreateMap<Review, ReviewContract>()
                .ForMember(d => d.Created, opt => opt.MapFrom(r => TimeConverter.ToUnixTime(r.Created)));

            CreateMap<EditUserContract, User>();

            CreateMap<EditRestaurantContract, Restaurant>();

            CreateMap<EditReviewContract, Review>();
        }
    }
}
