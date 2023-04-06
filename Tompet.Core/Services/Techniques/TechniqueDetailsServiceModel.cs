namespace Tompet.Core.Services.Techniques
{
    public class TechniqueDetailsServiceModel : TechniqueServiceModel
    {
        public string? Description { get; init; }

        public Guid ServiceId { get; init; }

        public Guid ManagerId { get; init; }

        public string? ManagerName { get; init; }

        public string? UserId { get; init; }
     }
}
