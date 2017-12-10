using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GuestBook.Data;

namespace GuestBook.Domain
{
    public class User : IModel
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
        [StringLength(255, MinimumLength = 1)]
        public string Password { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Phone { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Email { get; set; }

        [Required]
        public List<Review> Reviews { get; set; }

        public User()
        {
            Id = IdentityGenerator.NewId();
        }
    }
}
