namespace Tompet.Infrastructure.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Data.DataConstants.Technique;
    using static Data.DataConstants.DefaultLengthForKeyGuid;

    public class Technique
    {
        [Key]
        [StringLength(DefaultLength)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(TechniqueNameMaxLength)]
        public string? Name { get; set; }

        [Required]
        [StringLength(TechniqueTypeMaxLength)]
        public string? Type { get; set; }

        public string? ImageUrl { get; set; }

        public Guid ServiceId { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public Service Service { get; set; }

        public Guid ManagerId { get; init; }

        [ForeignKey(nameof(ManagerId))]
        public Manager Manager { get; init; }
    }
}
