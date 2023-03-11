namespace Tompet.Models.Home
{
    public class IndexViewModel
    {
        public int TotalTechniques { get; init; }

        public int TotalUsers { get; init; }

        public int TotalOrders { get; init; }

        public List<TechniqueIndexViewModel> Techniques { get; init; }
    }
}
