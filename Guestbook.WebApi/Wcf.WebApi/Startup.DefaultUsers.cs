using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GuestBook.WebApi.Identity;

namespace GuestBook.WebApi
{
    public partial class Startup
    {
        public static async void CreateDefaultUsers(IApplicationBuilder app)
        {
            await CreateRoles(app);
            await CreateDefaultUser(app);
        }

        private static async Task CreateRoles(IApplicationBuilder app)
        {
            var userRoles = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();

            var roles = UserRoles.RoleList
                    .Select(r => new IdentityRole { Id = r.ToLower(), Name = r })
                    .ToArray();

            foreach (var role in roles)
            {
                if (userRoles.Roles.All(r => r.Id != role.Id))
                {
                    await userRoles.CreateAsync(role);
                }
            }
        }

        private static async Task CreateDefaultUser(IApplicationBuilder app)
        {
            var userManager = app.ApplicationServices.GetService<UserManager<User>>();

            var user = userManager.Users
                .FirstOrDefault(u => u.UserName == SystemUser.UserName);
            if (user == null)
            {
                user = new User() { UserName = SystemUser.UserName };
                var result = await userManager.CreateAsync(user, SystemUser.Password);
                if (!result.Succeeded)
                {
                    var error = string.Join(", ", result.Errors);
                    throw new Exception($"Unable to created system user: {error}");
                }

                await userManager.AddClaimAsync(user, new Claim(Claims.ClientUserId, user.Id));
                await userManager.AddClaimAsync(user, new Claim(Claims.ClientRole, UserRoles.SuperAdmin));
                await userManager.AddToRoleAsync(user, UserRoles.SuperAdmin);
            }
        }
    }
}
