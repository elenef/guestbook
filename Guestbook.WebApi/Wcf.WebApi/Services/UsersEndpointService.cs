using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GuestBook.Data;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Identity;
using GuestBook.WebApi.Mapper;
using GuestBook.WebApi.Services.Filters;
using Microsoft.AspNetCore.Identity;

namespace GuestBook.WebApi.Services
{
    public class UsersEndpointService : EndpointService<
            UserContract,
            EditUserContract,
            UserFilterContract,
            User,
            IEndpointFilter<UserFilterContract, User>>, IUserService
    {
        private UserManager<User> _userManager;


        public UsersEndpointService(
            IContractMapper mapper,
            UserManager<User> userManager,
            IEndpointFilter<UserFilterContract, User> filter,
            IRepository<User> repository)
            : base(repository, mapper, filter)
        {
            _userManager = userManager;
            _mapper = mapper;
            _filter = filter;
        }

        public override async Task<ItemList<UserContract>> ListAsync(UserFilterContract filterModel)
        {
            var users = _repository.Items.Include(u => u.Roles);
            var query = await _filter.ApplyAsync(users, filterModel);
            var total = query.Count();
            var list = await _filter.ApplyPagerAsync(query, filterModel);

            var models = _mapper.Map<List<User>, List<UserContract>>(list);

            var result = new ItemList<UserContract>
            {
                PageSize = filterModel.PageSize,
                Page = filterModel.Page,
                Data = models,
                Total = total
            };
            return result;
        }

        public override async Task<UserContract> GetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new Exception("Error");
            }

            var user = await _repository.FindByIdAsync(id, u => u.Roles);
            if (user == null)
            {
                throw new Exception(typeof(User) + id);
            }
            return _mapper.Map<User, UserContract>(user);
        }

        public async Task<UserContract> GetByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new Exception("Error");
            }

            var user = await _repository
                .Items
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (user == null)
            {
                return null;
            }
            return _mapper.Map<User, UserContract>(user);
        }

        public async Task<UserContract> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Error");
            }

            var user = await _repository
                .Items
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(s => s.Email == email);

            if (user == null)
            {
                return null;
            }
            return _mapper.Map<User, UserContract>(user);
        }

        public override async Task<UserContract> AddAsync(EditUserContract model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                throw new Exception("Error");
            }

            await ValidateModel(model);
            var user = _mapper.Map<EditUserContract, User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);
            CheckResultAndThrow(result);
            result = await _userManager.AddToRoleAsync(user, model.Role);
            CheckResultAndThrow(result);
            result = await _userManager.AddClaimAsync(user, new Claim(Claims.UserId, user.Id));
            CheckResultAndThrow(result);
            result = await _userManager.AddClaimAsync(user, new Claim(Claims.Role, user.Id));
            CheckResultAndThrow(result);

            var userContract = _mapper.Map<User, UserContract>(user);

            return userContract;
        }

        public override async Task<UserContract> UpdateAsync(string id, EditUserContract model)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new Exception("Error");
            }

            await ValidateModel(model);

            var user = _userManager.Users.Include(x => x.Roles).FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new Exception(typeof(User) + id);
            }

            if (!string.IsNullOrEmpty(model.Password))
            {
                var result = await _userManager.RemovePasswordAsync(user);
                CheckResultAndThrow(result);
                result = await _userManager.AddPasswordAsync(user, model.Password);
                CheckResultAndThrow(result);
            }

            _mapper.Map(model, user);
            await _userManager.UpdateAsync(user);

            return _mapper.Map<User, UserContract>(user);
        }

        public override async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new Exception("Error");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception(typeof(User) + id);
            }
            await _userManager.DeleteAsync(user);
        }

        protected override Task<bool> ValidateModel(EditUserContract model)
        {
            if (string.IsNullOrWhiteSpace(model.Role)
                || string.IsNullOrWhiteSpace(model.Name)
                || string.IsNullOrWhiteSpace(model.UserName))
            {
                throw new Exception("Error");
            }

            if (UserRoles.RoleList.All(r => r != model.Role))
            {
                throw new Exception("Error");
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                if (!model.Password.Any(u => char.IsUpper(u))
                    || !model.Password.Any(l => char.IsLower(l))
                    || model.Password.All(char.IsLetterOrDigit)
                    || !model.Password.Any(l => char.IsNumber(l)))
                {
                    throw new Exception("Error");
                }
            }

            return Task.FromResult(true);
        }

        private void CheckResultAndThrow(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var message = string.Join(", ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                throw new Exception("Error");
            }
        }
    }
}
