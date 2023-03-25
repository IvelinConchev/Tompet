namespace Tompet.Infrastructure.Data.Identity
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.ApplicationUser;

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(ApplicationUserFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(ApplicationUserLastNameMaxLength)]
        public string? LastName { get; set; }

        //[StringLength(ApplicationUserFullNameMaxLength)]
        //public string? FullName { get; set; }
    }
}
