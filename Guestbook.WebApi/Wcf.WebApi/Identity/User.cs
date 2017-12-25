using GuestBook.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace GuestBook.WebApi.Identity
{
    public class User : IdentityUser, IModel
    {
        public DateTime Created { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Phone { get; set; }

        public User()
        {
            Id = IdentityGenerator.NewId();
            Created = DateTime.UtcNow;
        }
    }
}
