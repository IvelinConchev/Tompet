namespace Tompet.Infrastructure.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Service;

    public class Service
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(ServiceNameMaxLength)]
        public string? Name { get; set; }

        [StringLength(ServiceDescriptionMaxLength)]
        public string? Description { get; set; }

        public IList<Technique> Techniques { get; set; } = new List<Technique>();

    }
}
