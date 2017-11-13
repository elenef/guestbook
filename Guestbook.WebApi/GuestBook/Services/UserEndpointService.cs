using GuestBook.Mapper;
using GuestBook.Models;
using GuestBook.Models.Contracts;
using GuestBook.Repositories;
using GuestBook.Services.Filters;

namespace GuestBook.Services
{
    public class UserEndpointService : EndpointService<UserContract, EditUserContract, UserFilterContract, User, UserEndpointFilter>
    {
        //private IRepository<Provider> _providersRepository;

        public UserEndpointService(
            IRepository<User> repository,
            IContractMapper mapper,
            UserEndpointFilter filter)
            : base(repository, mapper, filter)
        {
        }
    }
}
