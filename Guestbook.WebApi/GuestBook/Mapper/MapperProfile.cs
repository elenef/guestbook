using AutoMapper;
using GuestBook.Domain;
using GuestBook.Domain.Helpers;
using GuestBook.WebApi.Contracts;

namespace GuestBook.WebApi.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EditRestaurantContract, Restaurant>();

            CreateMap<Restaurant, RestaurantContract>()
                .ForMember(d => d.ReviewIds, opt => opt.Ignore());

            CreateMap<EditReviewContract, Review>();

            CreateMap<Review, ReviewContract>()
                .ForMember(d => d.Created, opt => opt.MapFrom(r => TimeConverter.ToUnixTime(r.Created)))
                .ForMember(d => d.UserName, opt => opt.MapFrom(r => r.User.Name))
                .ForMember(d => d.RestaurantName, opt => opt.MapFrom(r => r.Restaurant.Name));

            CreateMap<User, UserContract>();

            CreateMap<EditUserContract, User>();
        }
    }
}
