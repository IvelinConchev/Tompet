﻿namespace Tompet.Models.Techniques
{
    using System.ComponentModel.DataAnnotations;
    using Tompet.Core.Services.Techniques.Models;
    using static Tompet.Infrastructure.Data.DataConstants.Technique;
    public class TechniqueFormModel
    {
        [Required]
        [Display(Name = "Техника")]
        [StringLength(TechniqueNameMaxLength, MinimumLength = TechniqueNameMinLength, ErrorMessage = "Maximum {0}")]
        public string Name { get; init; }

        [Required]
        [Display(Name = "Тип на тениката")]
        [StringLength(TechniqueTypeMaxLength, MinimumLength =TechniqueTypeMinLength, ErrorMessage = "Maximum {0}")]
        public string Type { get; init; }

        [Display(Name = "Снимка")]
        public string ImageUrl { get; init; }

        [Display(Name = "Услуга")]
        public Guid ServiceId { get; init; }

        public IEnumerable<TechniqueServiceServiceModel> Services { get; set; } 
    }
}
