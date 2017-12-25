using IdentityServer4.Models;
using System.Collections.Generic;

namespace GuestBook.WebApi.Identity
{
    public class ApiResources
    {
        public static List<ApiResource> Get()
        {
            return new List<ApiResource>
            {
                new ApiResource("Main")
                {
                    Scopes = Scopes.Get()
                }
            };
        }
    }
}
