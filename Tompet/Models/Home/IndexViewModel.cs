namespace Tompet.Models.Home
{
    using Tompet.Core.Services.Techniques.Models;

    public class IndexViewModel
    {
        public int TotalTechniques { get; init; }

        public int TotalUsers { get; init; }

        public int TotalOrders { get; init; }

        public IList<TechniqueIndexViewModel> Techniques { get; init; }
        //public IList<LatestTechniqueServiceModel> Techniques { get; init; }
    }
}
