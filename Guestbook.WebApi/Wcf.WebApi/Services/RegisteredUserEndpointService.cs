using System;
using GuestBook.Data;
using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Identity;
using GuestBook.WebApi.Mapper;
using GuestBook.WebApi.Services.Filters;
using System.Data.Entity;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services
{
    public class RegisteredUserEndpointService : EndpointService<
        RegisteredUserContract,
        EditRegisteredUserContract,
        RegisteredUserFilterContract,
        RegisteredUser,
        RegisteredUserEndpointFilter>
    {
        private IUserService _userService;

        public RegisteredUserEndpointService(
            IRepository<RegisteredUser> repository,
            IContractMapper mapper,
            RegisteredUserEndpointFilter filter,
            IUserService userService)
            : base(repository, mapper, filter)
        {
            _userService = userService;
        }

        public async override Task<RegisteredUserContract> AddAsync(EditRegisteredUserContract model)
        {
            var createdUser = await _userService.AddAsync(new UserContract()
            {
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone,
                Role = UserRoles.User,
                UserName = model.Login,
                Password = model.Password
            });

            await ValidateModel(model);
            var item = _mapper.Map<EditRegisteredUserContract, RegisteredUser>(model);
            await BeforeUpdate(model, item);

            item.Id = createdUser.Id;

            await _repository.AddAsync(item);

            var result = _mapper.Map<RegisteredUser, RegisteredUserContract>(item);
            await AfterMapAsync(item, result);
            return result;
        }

        public override async Task DeleteAsync(string id)
        {
            var creditor = await _repository.Items
                .FirstOrDefaultAsync(c => c.Id == id);
            if (creditor == null)
            {
                throw new Exception(typeof(RegisteredUser) + id);
            }


            await BeforeDeleteItem(creditor);

            await _repository.RemoveAsync(creditor);
        }

        public override async Task<RegisteredUserContract> UpdateAsync(string id, EditRegisteredUserContract model)
        {
            await ValidateModel(model);
            var item = await GetListQuery()
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                throw new Exception(typeof(RegisteredUser) + id);
            }

            await _userService.UpdateAsync(id, new EditUserContract()
            {
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone,
                Role = UserRoles.User,
                UserName = model.Login,
                Password = model.Password
            });

            _mapper.Map(model, item);
            await BeforeUpdate(model, item);

            await _repository.UpdateAsync(item);

            var result = _mapper.Map<RegisteredUser, RegisteredUserContract>(item);
            await AfterMapAsync(item, result);
            return result;
        }
    }
}
