namespace Tompet.Core.Services.Techniques
{
    using System.Collections.Generic;
    using Tompet.Infrastructure.Data;
    using Tompet.Infrastructure.Data.Models;
    using Tompet.Models;

    public class TechniqueService : ITechniqueService
    {
        private readonly TompetDbContext data;

        public TechniqueService(TompetDbContext data)
            => this.data = data;

        public TechniquesQueryServiceModel All(
            string name,
            string searchTerm,
            TechniqueSorting sorting,
            int currentPage,
            int techniquesPerPage)
        {
            var techniquesQuery = this
                .data.Techniques.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                techniquesQuery = techniquesQuery.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                techniquesQuery = techniquesQuery.Where(c =>
                (c.Name + " " + c.Type).ToLower().Trim().Contains(searchTerm.ToLower())
                || (c.Type + " " + c.Name).ToLower().Trim().Contains(searchTerm.ToLower()));
            }

            techniquesQuery = sorting switch
            {
                TechniqueSorting.NameAndType => techniquesQuery.OrderByDescending(c => c.Name).ThenBy(c => c.Type),
                TechniqueSorting.NameAndType or _ => techniquesQuery.OrderByDescending(c => c.Name)

            };

            var totalTechniques = techniquesQuery.Count();

            var tecniques = techniquesQuery
                .Skip((currentPage - 1) * techniquesPerPage)
                .Take(techniquesPerPage)
                .Select(c => new TechniqueServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    ImageUrl = c.ImageUrl,
                    Service = c.Service.Name
                })
                .ToList();

            return new TechniquesQueryServiceModel
            {
                TotalTechniques = totalTechniques,
                CurrentPage = currentPage,
                TechniquesPerPage = techniquesPerPage,
                Techniques = tecniques
            };
        }

        //public IEnumerable<TechniqueServiceModel> ByUser(string userId)
        //   => this.data
        //    .Techniques
        //    .Where(m => m.Manager.UserId = userId)
        //    .Select(c => new TechniqueServiceModel
        //    {
        //        Id = c.Id,
        //        Name = c.Name,
        //        Type = c.Type,
        //        ImageUrl = c.ImageUrl,
        //        Service = c.Service.Name
        //    })
        //        .ToList();

        public IEnumerable<string> AllTechniqueNames()
              => this.data
                .Techniques
                .Select(c => c.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

        public IEnumerable<TechniqueServiceModel> ByUser(string userId)
        {
            throw new NotImplementedException();
        }

        //private IEnumerable<TechniqueServiceModel> GetTechniques(IQueryable<Technique>) techniqueQuery)
    }
}
