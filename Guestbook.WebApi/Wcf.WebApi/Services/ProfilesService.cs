using GuestBook.Data;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Extensions;
using GuestBook.WebApi.Identity;
using GuestBook.WebApi.Mapper;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services
{
    //!!! Здесь не получиться использовать async методы контекста для EF6 и EF7 одновременно
    //Используется System.Data.Entity для EF6!!!

    /// <summary>
    /// Сервис профилей.
    /// </summary>
    public class ProfilesService : IProfilesService
    {
        //private IRepository<RegisteredUserContract> _registeredUserRepository;
        private IRepository<User> _userRepository;
        private UserManager<User> _userManager;
        private IUserContext _userContext;

        private IContractMapper _mapper;

        public ProfilesService(
            //IRepository<RegisteredUserContract> registeredUserRepository,
            IRepository<User> userRepository,
            UserManager<User> userManager,
            IUserContext userContext,
            IContractMapper contractMapper)
        {
            //_registeredUserRepository = registeredUserRepository;
            _userRepository = userRepository;

            _userManager = userManager;

            _userContext = userContext;

            _mapper = contractMapper;
        }

        /// <summary>
        /// Возвращает профиль по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор профиля.</param>
        /// <returns>Контрак профиля.</returns>
        public async Task<ProfileContract> GetProfile()
        {
            var id = _userContext.UserId;
            var role = _userContext.UserRole;

            // Create serializer temporary here
            var serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            
            var user = _userRepository
                .Items
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new Exception(typeof(User)+ id);
            }

            var value = _mapper.Map<User, RegisteredUserContract>(user);

            var usersRole = (await _userManager
                    .GetRolesAsync(user))
                .FirstOrDefault();

            if (usersRole == null)
            {
                throw new Exception("");
            }

            var profile = new ProfileContract
            {
                Id = id,
                Role = usersRole,
                Value = value.ToContractObject()
            };

            return profile;
        }

        /// <summary>
        /// Обновляет профиль.
        /// </summary>
        /// <param name="id">Идентификатор профиля.</param>
        /// <param name="contract">Контракт профиля.</param>
        /// <returns>Контракт обновленного профиля.</returns>
        public async Task<ProfileContract> UpdateProfile(EditProfileContract contract)
        {
            if (contract.Role == null || contract.Value == null)
            {
                throw new Exception("");
            }

            var id = _userContext.UserId;
            
            var model = _userRepository
                .Items
                .FirstOrDefault(u => u.Id == id);

            if (model == null)
            {
                throw new Exception(typeof(User)+ id);
            }

            var value = contract.Value.ToObject<RegisteredUserContract>();

            _mapper.Map<RegisteredUserContract, User>(value, model);

            await _userManager.UpdateAsync(model);

            var resultValue = _mapper.Map<User, RegisteredUserContract>(model);

            var result = new ProfileContract
            {
                Id = id,
                Role = contract.Role,
                Value = resultValue.ToContractObject()
            };

            return result;
        }
    }
}
