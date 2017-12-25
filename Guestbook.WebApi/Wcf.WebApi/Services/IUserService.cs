using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Services.Filters;
using System.Threading.Tasks;
using GuestBook.WebApi.Identity;

namespace GuestBook.WebApi.Services
{
    public interface IUserService : IEndpointService<UserContract, EditUserContract, UserFilterContract, User>
    {
        Task<UserContract> GetByIdAsync(string id);

        Task<UserContract> GetByEmailAsync(string email);
    }
}
