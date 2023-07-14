namespace Tompet.Core.Services.Techniques.Models
{
    using System;
    using Tompet.Core.Contracts;

    public class LatestTechniqueServiceModel : ITechniqueModel
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Type { get; init; }

        public string ImageUrl { get; init; }

        public string ServiceName { get; init; }
    }
}
