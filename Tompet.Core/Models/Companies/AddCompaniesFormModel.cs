namespace Tompet.Models.Companies
{
    using System.ComponentModel.DataAnnotations;

    using static Tompet.Infrastructure.Data.DataConstants.Company;

    public class AddCompaniesFormModel
    {
        [Required]
        [StringLength(CompanyNameMaxLength)]
        [Display(Name="Име на Фирмата")]
        public string? Name { get; set; }

        [Display(Name="blue")]
        public string? Manager { get; set; }

        [Display(Name = "Телефонен номер")]
        public string? Phone { get; set; }

        [Display(Name = "Адрес")]
        public string? Address { get; set; }
    }
}
