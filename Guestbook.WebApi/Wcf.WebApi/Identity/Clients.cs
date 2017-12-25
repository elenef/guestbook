using IdentityServer4.Models;
using System.Collections.Generic;

namespace GuestBook.WebApi.Identity
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Website",
                    ClientId = "AA63134B353A4BC6BE9BCBE4961C0BF1",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("SqFKcEIRNt3a1zYZH8MUuTQUa5g7wG8fk6eMSbf9xrzTVr0xEA".Sha512())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new List<string>
                    {
                        Scopes.AdminScope.Name
                    },
                    AccessTokenLifetime = 60*60*24*30
                }
            };
        }
    }
}
