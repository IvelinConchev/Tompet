namespace Tompet.Core.Services.Techniques
{
    using Tompet.Core.Services.Techniques.Models;
    using Tompet.Models;

    public interface ITechniqueService
    {
        TechniquesQueryServiceModel All(
            string name,
            string searchTerm,
            TechniqueSorting sorting,
            int currentPage,
            int techniquesPerPage);

        TechniqueDetailsServiceModel Details(Guid techniqueId);

        Guid Create(string name,
               string type,
               string imageUrl,
               Guid serviceId,
               Guid managerId);

        bool Edit(
             Guid techniqueId,
             string name,
             string type,
             string imageUrl,
             Guid serviceId);


        IEnumerable<TechniqueServiceModel> ByUser(string userId);

        bool IsByManager(Guid techniqueId, Guid managerId);

        IEnumerable<string> AllNames();

        IEnumerable<TechniqueServiceServiceModel> AllServices();

        bool ServiceExist(Guid serviceId);
    }
}
