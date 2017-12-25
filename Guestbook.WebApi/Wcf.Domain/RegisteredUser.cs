using GuestBook.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuestBook.Domain
{
    public class RegisteredUser : IModel
    {
        [Required]
        [StringLength(32, MinimumLength = 1)]
        public string Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Login { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Phone { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Email { get; set; }

        public List<Restaurant> Restaurants { get; set; }

        public RegisteredUser()
        {
            Id = IdentityGenerator.NewId();
        }
    }
}
