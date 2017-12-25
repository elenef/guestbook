using System.Linq;
using AutoMapper;
using GuestBook.Domain;
using GuestBook.Domain.Helpers;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Identity;

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
                .ForMember(d => d.RestaurantName, opt => opt.MapFrom(r => r.Restaurant.Name));

            CreateMap<RegisteredUser, RegisteredUserContract>();

            CreateMap<EditRegisteredUserContract, RegisteredUser>();

            CreateMap<User, RegisteredUserContract>();

            CreateMap<EditUserContract, User>();

            CreateMap<User, UserContract>()
                .ForMember(d => d.Role, opt => opt.MapFrom(s => s.Roles.FirstOrDefault().RoleId));

            CreateMap<EditRegisteredUserContract, User>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Login));

            CreateMap<User, ProfileContract>();

            CreateMap<ProfileContract, User>();
        }
    }
}
