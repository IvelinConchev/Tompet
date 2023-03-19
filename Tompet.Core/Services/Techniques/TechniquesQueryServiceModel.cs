namespace Tompet.Core.Services.Techniques
{
    public class TechniquesQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int TechniquesPerPage { get; init; }

        public int TotalTechniques { get; set; }

        public IEnumerable<TechniqueServiceModel> Techniques { get; init; }
    }
}
