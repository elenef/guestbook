using System;
using System.ComponentModel.DataAnnotations;

namespace GuestBook.Models
{
    public class Review : IModel
    {
        [Required]
        [StringLength(32, MinimumLength = 1)]
        public string Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Comment { get; set; }

        [Required]
        public DateTime? Created { get; set; }

        [Required]
        public Restaurant Restaurant { get; set; }

        [Required]
        public User User { get; set; }

        public Review()
        {
            Id = IdentityGenerator.NewId();
            Created = DateTime.UtcNow;
        }
    }
}
