using GuestBook.Data;
using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Mapper;
using GuestBook.WebApi.Services.Filters;

namespace GuestBook.WebApi.Services
{
    public class UserEndpointService : EndpointService<UserContract, EditUserContract, UserFilterContract, User, UserEndpointFilter>
    {
        public UserEndpointService(
            IRepository<User> repository,
            IContractMapper mapper,
            UserEndpointFilter filter)
            : base(repository, mapper, filter)
        {
        }
    }
}
