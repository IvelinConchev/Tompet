﻿namespace Tompet.Infrastructure.Data.Identity
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.ApplicationUser;

    public class ApplicationUser : IdentityUser
    {
        [StringLength(ApplicationUserFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [StringLength(ApplicationUserLastNameMaxLength)]
        public string? LastName { get; set; }
    }
}
