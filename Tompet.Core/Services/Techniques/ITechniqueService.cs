namespace Tompet.Core.Services.Techniques
{
    using Tompet.Models;

    public interface ITechniqueService
    {
        TechniquesQueryServiceModel All(
            string name,
            string searchTerm,
            TechniqueSorting sorting,
            int currentPage,
            int techniquesPerPage);

        IEnumerable<TechniqueServiceModel> ByUser(string userId);

        IEnumerable<string> AllTechniqueNames();
    }
}
