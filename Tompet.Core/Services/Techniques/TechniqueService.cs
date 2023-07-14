namespace Tompet.Core.Services.Techniques
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
    using Tompet.Core.Services.Techniques.Models;
    using Tompet.Infrastructure.Data;
    using Tompet.Infrastructure.Data.Models;
    using Tompet.Models;

    public class TechniqueService : ITechniqueService
    {
        private readonly TompetDbContext data;
        private readonly IConfigurationProvider mapper;

        public TechniqueService(TompetDbContext data, 
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        } 

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

            var tecniques = GetTechniques(techniquesQuery
                .Skip((currentPage - 1) * techniquesPerPage)
                .Take(techniquesPerPage));


            return new TechniquesQueryServiceModel
            {
                TotalTechniques = totalTechniques,
                CurrentPage = currentPage,
                TechniquesPerPage = techniquesPerPage,
                Techniques = tecniques
            };
        }

        public TechniqueDetailsServiceModel Details(Guid id)
           => this.data
            .Techniques
            .Where(t => t.Id == id)
            .ProjectTo<TechniqueDetailsServiceModel>(this.mapper)
            .FirstOrDefault();

        public Guid Create(string name, string type, string imageUrl, Guid serviceId, Guid managerId)
        {
            var teqniqueData = new Technique
            {
                Name = name,
                Type = type,
                ImageUrl = imageUrl,
                ServiceId = serviceId,
                ManagerId = managerId
            };

            this.data.Techniques.Add(teqniqueData);

            this.data.SaveChanges();

            return teqniqueData.Id;
        }

        public bool Edit(Guid id, string name, string type, string imageUrl, Guid serviceId)
        {
            var techniqueData = this.data.Techniques.Find(id);

            if (techniqueData == null)
            {
                return false;
            }


            techniqueData.Name = name;
            techniqueData.Type = type;
            techniqueData.ImageUrl = imageUrl;
            techniqueData.ServiceId = serviceId;


            this.data.SaveChanges();

            return true;
        }
        public IEnumerable<TechniqueServiceModel> ByUser(string userId)
           => GetTechniques(this.data
               .Techniques
               .Where(t => t.Manager.UserId == userId));

        public bool IsByManager(Guid techniqueId, Guid managerId)
           => this.data
            .Techniques
            .Any(t => t.Id == techniqueId && t.ManagerId == managerId);

        public IEnumerable<string> AllNames()
              => this.data
                .Techniques
                .Select(c => c.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

        public bool ServiceExist(Guid serviceId)
          => this.data
            .Techniques
            .Any(t => t.Id == serviceId);

        public IEnumerable<TechniqueServiceServiceModel> AllServices()
            => this.data
               .Services
               .Select(s => new TechniqueServiceServiceModel
               {
                   Id = s.Id,
                   Name = s.Name,
               })
               .ToList();


        private static IEnumerable<TechniqueServiceModel> GetTechniques(IQueryable<Technique> techniqueQuery)
            => techniqueQuery
            .Select(c => new TechniqueServiceModel
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type,
                ImageUrl = c.ImageUrl,
                ServiceName = c.Service.Name
            })
                .ToList();

        public IEnumerable<LatestTechniqueServiceModel> Latest()
        => this.data
            .Techniques
            .Where(t => t.IsPublic)
            .OrderByDescending(s => s.Id)
            .Select(s => new LatestTechniqueServiceModel
            {
                Id = s.Id,
                ImageUrl = s.ImageUrl,
                Name = s.Name,
                Type = s.Type,
                ServiceName = s.Service.Name
            })
            .Take(3)
            .ToList();
    }
}
