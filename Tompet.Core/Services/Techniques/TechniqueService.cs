﻿namespace Tompet.Core.Services.Techniques
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
            .Select(t => new TechniqueDetailsServiceModel
            {
                Id = t.Id,
                Name = t.Name,
                //Description = t.Description,
                Type = t.Type,
                ImageUrl = t.ImageUrl,
                ServiceName = t.Service.Name,
                ManagerId = t.ManagerId,
                ManagerName = t.Manager.Name,
                UserId = t.Manager.UserId
            })
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

        public IEnumerable<TechniqueServiceServiceModel> AllCategories()
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

        
    }
}
