namespace Tompet.Infrastructure.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Company;
    using static Data.DataConstants.DefaultLengthForKeyGuid;
    public class Company
    {
        [Key]
        [StringLength(DefaultLength)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(CompanyNameMaxLength)]
        public string? Name { get; set; }

        [Required]
        [StringLength(CompanyManagerMaxLength)]
        public string? Manager { get; set; }

        [Required]
        [StringLength(CompanyPhoneNumberMaxLength)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(CompanyAddressMaxLenghth)]
        public string? Address { get; set; }

        public string ImageUrl { get; set; }
    }
}
