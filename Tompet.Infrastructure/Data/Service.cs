namespace Tompet.Infrastructure.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Service
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        public IList<Technique> Techniques { get; set; } = new List<Technique>();

    }
}
