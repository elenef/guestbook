using GuestBook.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuestBook.Domain
{
    public class Restaurant : IModel
    {
        [Required]
        [StringLength(32, MinimumLength = 1)]
        public string Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Description { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Address { get; set; }

        [Required]
        public List<Review> Reviews { get; set; }

        public Restaurant()
        {
            Id = IdentityGenerator.NewId();
        }
    }
}
