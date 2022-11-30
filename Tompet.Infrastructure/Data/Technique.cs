namespace Tompet.Infrastructure.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Technique;

    public class Technique
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(TechniqueNameMaxLength)]
        public string? Name { get; set; }

        [Required]
        [StringLength(TechniqueTypeMaxLength)]
        public string? Type { get; set; }

        public string ImageUrl { get; set; }
    }
}
