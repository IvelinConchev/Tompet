namespace Tompet.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Tompet.Infrastructure.Data.DataConstants.Manager;

    public class Manager
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(ManagerNameMaxLength)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? UserId { get; set; }

        public IEnumerable<Technique> Techniques { get; set; } = new List<Technique>();
    }
}
