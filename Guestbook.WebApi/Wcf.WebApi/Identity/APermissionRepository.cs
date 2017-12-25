using System;
using System.Collections.Generic;
using System.Linq;

namespace GuestBook.WebApi.Identity
{
    /// <summary>
    /// Permission repository allow to set role-based permissions
    /// </summary>
    public abstract class APermissionRepository : IPermissionRepository
    {
        protected virtual List<RoleFieldEditPermission> FieldEditPermissions { get; }

        protected virtual List<RoleFieldReadRestrictions> FieldReadRestrictions { get; }

        protected virtual Dictionary<string, List<RoleEndpointPermission>> ScopesPermissions { get; }

        public APermissionRepository()
        {
            FieldEditPermissions = new List<RoleFieldEditPermission>();
            FieldReadRestrictions = new List<RoleFieldReadRestrictions>();
            ScopesPermissions = new Dictionary<string, List<RoleEndpointPermission>>();
        }

        public bool HasAccess(string role, string url, string method, string scope)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Parameter can't be null", "url");
            }
            if (role == UserRoles.SuperAdmin)
            {
                return true;
            }

            var methodValue = (EndpointMethod)Enum.Parse(typeof(EndpointMethod), method, ignoreCase: true);
            var permissions = GetScopePermissions(scope);
            return permissions.Any(p => p.IsMatch(url, methodValue, role));
        }

        public bool HasAccess(string url, string method, string scope)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Parameter can't be null", "url");
            }

            var methodValue = (EndpointMethod)Enum.Parse(typeof(EndpointMethod), method);
            var permissions = GetScopePermissions(scope);
            return permissions.Any(p => p.IsMatch(url, methodValue));
        }

        private List<RoleEndpointPermission> GetScopePermissions(string scope)
        {
            var permissions = new List<RoleEndpointPermission>();
            if (ScopesPermissions.ContainsKey(scope))
            {
                permissions = ScopesPermissions[scope];
            }
            return permissions;
        }

        public List<string> GetModelEditableFields(string userRole, object model)
        {
            if (model == null)
            {
                return new List<string>();
            }

            var modelName = model.GetType().Name;

            var permission = FieldEditPermissions
                .FirstOrDefault(p => p.ModelName == modelName && p.Role == userRole);

            return permission?.Fields;
        }

        public List<string> GetRestrictedFields(string userRole, object model)
        {
            if (model == null)
            {
                return new List<string>();
            }

            var modelName = model.GetType().Name;

            var permission = FieldReadRestrictions
                .FirstOrDefault(p => p.ModelName == modelName && p.Role == userRole);

            return permission?.Fields ?? new List<string>();
        }
    }
}
