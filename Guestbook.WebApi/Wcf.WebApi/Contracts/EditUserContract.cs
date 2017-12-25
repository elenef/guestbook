using System.ComponentModel.DataAnnotations;

namespace GuestBook.WebApi.Contracts
{
    public class EditUserContract
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        [MaxLength(256)]
        public string Password { get; set; }
    }
}
