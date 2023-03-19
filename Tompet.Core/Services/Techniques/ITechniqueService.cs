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

        IEnumerable<string> AllTechniqueNames();
    }
}
