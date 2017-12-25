using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GuestBook.WebApi.Identity
{
    public class IdentityContext : IdentityDbContext<User, IdentityRole, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
               : base(options)
        {

        }
    }
}
