namespace Tompet.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Tompet.Infrastructure.Data;
    using Tompet.Models.Techniques;

    public class TechniquesController : Controller
    {
        private readonly TompetDbContext data;

        public TechniquesController(TompetDbContext data) => this.data = data;

        public IActionResult All([FromQuery]
           AllTechniquesQueryModel query)
        {
            var techniquesQuery = this
                .data.Techniques.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                techniquesQuery = techniquesQuery.Where(c => c.Name == query.Name);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                techniquesQuery = techniquesQuery.Where(c =>
                (c.Name + " " + c.Type).ToLower().Trim().Contains(query.SearchTerm.ToLower())
                || (c.Type + " " + c.Name).ToLower().Trim().Contains(query.SearchTerm.ToLower()));
            }

            techniquesQuery = query.Sorting switch
            {
                TechniqueSorting.NameAndType => techniquesQuery.OrderByDescending(c => c.Name).ThenBy(c => c.Type),
                TechniqueSorting.NameAndType or _ => techniquesQuery.OrderByDescending(c => c.Name)

            };

            var totalTechniques = techniquesQuery.Count();

            var tecniques = techniquesQuery
                .Skip((query.CurrentPage - 1) * AllTechniquesQueryModel.TechniquesPerPage)
                .Take(AllTechniquesQueryModel.TechniquesPerPage)
                .Select(c => new TechniqueListingViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    ImageUrl = c.ImageUrl,
                    Service = c.Service.Name
                })
                .ToList();

            var techniqueNames = this.data
                .Techniques
                .Select(c => c.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            query.TotalTechnique = totalTechniques;
            query.Names = techniqueNames;
            query.Techniques = tecniques;

            return View(query);
        }

        private IEnumerable<TecniqueServiceViewModel> GetTechniqueServices()
        => this.data
            .Services
            .Select(t => new TecniqueServiceViewModel
            {
                Id = t.Id,
                Name = t.Name,
            })
            .ToList();

        public IActionResult Add() => View(new AddTechniquesFormModel
        {
            Services = this.GetTechniqueServices()
        });

        [HttpPost]
        public IActionResult Add(AddTechniquesFormModel teqnique)
        {
            if (!this.data.Services.Any(s => s.Id == teqnique.ServiceId))
            {
                this.ModelState.AddModelError(nameof(teqnique.ServiceId), "Services don't found");
            }

            if (!ModelState.IsValid)
            {
                teqnique.Services = this.GetTechniqueServices();

                return View(teqnique);
            }

            var teqniqueData = new Technique
            {
                Name = teqnique.Name,
                Type = teqnique.Type,
                ImageUrl = teqnique.Images,
                ServiceId = teqnique.ServiceId,
            };

            this.data.Techniques.Add(teqniqueData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

    }
}
