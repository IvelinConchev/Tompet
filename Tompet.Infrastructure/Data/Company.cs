namespace Tompet.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Company
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Manager { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? Address { get; set; }
    }
}
