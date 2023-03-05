namespace Tompet.Models.Services
{
    using System.ComponentModel.DataAnnotations;


    using static Tompet.Infrastructure.Data.DataConstants.Service;
    public class AddServicesFormModel
    {
        [Required]
        [Display(Name = "Услуга")]
        [StringLength(ServiceNameMaxLength, MinimumLength = ServiceNameMinLength, ErrorMessage = "Maximum {0}")]
        public string? Name { get; init; }

        [Display(Name = "Описание")]
        [Required]
        [StringLength(int.MaxValue,
            MinimumLength = ServiceNameMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string? Description { get; init; }

    }
}
