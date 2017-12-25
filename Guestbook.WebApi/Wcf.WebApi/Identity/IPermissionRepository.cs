using System.Collections.Generic;

namespace GuestBook.WebApi.Identity
{
    public interface IPermissionRepository
    {
        bool HasAccess(string role, string url, string method, string scope);
        bool HasAccess(string url, string method, string scope);
        List<string> GetModelEditableFields(string userRole, object model);
        List<string> GetRestrictedFields(string userRole, object model);
    }
}
