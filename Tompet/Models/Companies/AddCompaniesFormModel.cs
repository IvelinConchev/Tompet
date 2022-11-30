namespace Tompet.Models.Companies
{
    using System.ComponentModel.DataAnnotations;
    using static Tompet.Infrastructure.Data.DataConstants.Company;
    public class AddCompaniesFormModel
    {
        [Required]
        [Display(Name = "Име на Фирмата")]
        [StringLength(CompanyNameMaxLength, MinimumLength = CompanyNameMinLength, ErrorMessage = "Maximum {0}")]
        public string? Name { get; set; }

        [Required]
        [StringLength(CompanyManagerMaxLength, MinimumLength = CompanyManagerMinLength, ErrorMessage = "Maximum {2}")]
        [Display(Name = "Име на управителя")]
        public string? Manager { get; set; }

        [Required]
        [StringLength(CompanyPhoneNumberMaxLength, MinimumLength = CompanyPhoneNumberMinLength, ErrorMessage = "Maximum {0}")]
        [Display(Name = "Телефонен номер")]
        public string? Phone { get; set; }

        [Required]
        [MinLength(CompanyAddressMinLenghth)]
        [StringLength(CompanyAddressMaxLenghth, MinimumLength = CompanyAddressMinLenghth, ErrorMessage = "Maximum {0}")]
        [Display(Name = "Адрес")]
        public string? Address { get; set; }
    }
}
