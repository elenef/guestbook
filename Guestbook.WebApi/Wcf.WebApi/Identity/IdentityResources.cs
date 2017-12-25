using IdentityServer4.Models;
using System.Collections.Generic;

namespace GuestBook.WebApi.Identity
{
    public class IdentityResources
    {
        public static List<IdentityResource> Get()
        {
            return new List<IdentityResource>
            {
                new IdentityResource
                {
                    Name = Claims.UserId,
                    UserClaims = new List<string>{Claims.UserId},
                    Emphasize = true,
                    DisplayName = "Unique user id"
                }
            };
        }
    }
}
