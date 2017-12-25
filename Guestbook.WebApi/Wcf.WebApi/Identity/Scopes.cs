using IdentityServer4.Models;
using System.Collections.Generic;

namespace GuestBook.WebApi.Identity
{
    public class Scopes
    {
        public static Scope AdminScope = new Scope
        {
            Name = "admin_area",
            DisplayName = "Admin Area",
            UserClaims = new List<string>
            {
                Claims.UserId,
                Claims.Role
            }
        };

        public static List<Scope> Get()
        {
            return new List<Scope>
            {
                AdminScope,
            };
        }
    }
}
