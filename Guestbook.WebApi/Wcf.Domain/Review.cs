using GuestBook.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace GuestBook.Domain
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
        public string UserName { get; set; }

        [Required]
        public int ReviewRating { get; set; }

        [Required]
        public int Like { get; set; }

        public Review()
        {
            Id = IdentityGenerator.NewId();
            Created = DateTime.UtcNow;
        }
    }
}
