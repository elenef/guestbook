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
            CreateMap<User, UserContract>()
                .ReverseMap();

            CreateMap<Restaurant, RestaurantContract>()
                .ReverseMap();

            CreateMap<Review, ReviewContract>()
                .ForMember(d => d.Created, opt => opt.MapFrom(r => TimeConverter.ToUnixTime(r.Created)))
                .ForMember(d => d.UserId, opt => opt.MapFrom(r => r.User.Id))
                .ForMember(d => d.RestaurantId, opt => opt.MapFrom(r => r.Restaurant.Id))
                .ReverseMap();

            CreateMap<EditUserContract, User>();

            CreateMap<EditRestaurantContract, Restaurant>();

            CreateMap<EditReviewContract, Review>();
        }
    }
}
