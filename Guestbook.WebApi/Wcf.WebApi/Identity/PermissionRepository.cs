using System.Collections.Generic;

namespace GuestBook.WebApi.Identity
{
    public class PermissionRepository : APermissionRepository
    {
        private static List<RoleEndpointPermission> _adminArea = new List<RoleEndpointPermission>
        {
            #region User
            
            new RoleEndpointPermission("/profiles", EndpointMethod.All, UserRoles.User),
            //new RoleEndpointPermission("/users", EndpointMethod.All, UserRoles.User),
            new RoleEndpointPermission("/restaurants", EndpointMethod.All, UserRoles.User),
            //new RoleEndpointPermission("/reviews", EndpointMethod.All, UserRoles.User),

            #endregion

            #region Superadmin
            new RoleEndpointPermission("/profiles", EndpointMethod.All, UserRoles.SuperAdmin),
            new RoleEndpointPermission("/users", EndpointMethod.All, UserRoles.SuperAdmin),
            new RoleEndpointPermission("/restaurants", EndpointMethod.All, UserRoles.SuperAdmin),
            new RoleEndpointPermission("/reviews", EndpointMethod.All, UserRoles.SuperAdmin)
            #endregion

            //Add list of permissions here
        };

        private Dictionary<string, List<RoleEndpointPermission>> _scopesPermissions;

        protected override Dictionary<string, List<RoleEndpointPermission>> ScopesPermissions => _scopesPermissions;

        public PermissionRepository()
        {
            _scopesPermissions = new Dictionary<string, List<RoleEndpointPermission>>
            {
                { Scopes.AdminScope.Name, _adminArea },
            };
        }
    }
}
