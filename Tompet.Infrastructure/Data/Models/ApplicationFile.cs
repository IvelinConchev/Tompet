namespace Tompet.Infrastructure.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationFile
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? FileName { get; set; }

        public byte[] Content { get; set; }
    }
}
