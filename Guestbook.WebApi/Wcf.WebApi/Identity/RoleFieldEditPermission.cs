using System.Collections.Generic;
using System.Linq;

namespace GuestBook.WebApi.Identity
{
    public class RoleFieldEditPermission
    {
        public string ModelName { get; set; }

        public List<string> Fields { get; set; }

        public string Role { get; set; }

        public RoleFieldEditPermission(string role, string model, params string[] fields)
        {
            Role = role;
            ModelName = model;
            Fields = fields.ToList();
        }
    }
}
