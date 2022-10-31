namespace Tompet.Infrastructure.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Technique
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(50)]
        public string? Type { get; set; }
    }
}
