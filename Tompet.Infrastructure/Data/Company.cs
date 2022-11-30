namespace Tompet.Infrastructure.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Company;
    public class Company
    {
        [Key]
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
    }
}
