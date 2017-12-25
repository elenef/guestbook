using GuestBook.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace GuestBook.WebApi.Identity
{
    public class UserRoles : IdentityRole<string>, IModel
    {
        public const string SuperAdmin = "superadmin";
        public const string User = "user";

        public static List<string> RoleList { get; } = new List<string>
        {
            SuperAdmin,
            User
        };
    }
}
