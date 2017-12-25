using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Identity.Policies
{
    public class DefaultEndpointHandler : AuthorizationHandler<DefaultEndpointRequirement>
    {
        public const string EndpointRequirementFailedField = "EndpointRequirementFailed";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DefaultEndpointRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;

            //Костыль. Добавление запросов на регистранцию провайдеров должно обрабатываться только в политике ProviderRegistrationHandler
            var actionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor?.ControllerName == "RegisteredUser" && actionDescriptor?.ActionName == "Add")
            {
                //Сделать авторизацию пользователя не обращая на результаты
                AuthorizeUser(context, filterContext);
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (AuthorizeUser(context, filterContext))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            filterContext.HttpContext.Items[EndpointRequirementFailedField] = true;
            context.Fail();

            return Task.CompletedTask;
        }

        protected static bool AuthorizeUser(AuthorizationHandlerContext context, AuthorizationFilterContext filterContext)
        {
            if (filterContext == null)
            {
                return false;
            }

            if (!context.User.IsAuthenticated())
            {
                return false;
            }

            var method = filterContext.HttpContext.Request.Method;
            var url = filterContext.HttpContext.Request.Path.ToUriComponent();
            var userClaims = context.User.Claims.ToList();

            var role = userClaims.FirstOrDefault(c => c.Type == Claims.Role);
            var userId = userClaims.FirstOrDefault(c => c.Type == Claims.UserId);
            var scope = userClaims.FirstOrDefault(c => c.Type == Claims.Scope)?.Value;

            var userContext = (UserContext)filterContext.HttpContext.RequestServices.GetService<IUserContext>();
            userContext.UserId = userId?.Value;
            userContext.UserRole = role?.Value;

            var permissionRepository =
                filterContext.HttpContext.RequestServices.GetService<IPermissionRepository>();

            if (role == null && permissionRepository.HasAccess(url, method, scope))
            {
                return true;
            }

            if (role != null && permissionRepository.HasAccess(role.Value, url, method, scope))
            {
                return true;
            }

            return false;
        }
    }
}
