namespace Tompet.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Tompet.Core.Services.Techniques;
    using Tompet.Infrastructure.Data;
    using Tompet.Infrastructure.Data.Models;
    using Tompet.Models.Techniques;

    public class TechniquesController : Controller
    {
        private readonly ITechniqueService techniques;
        private readonly TompetDbContext data;

        public TechniquesController(ITechniqueService techniques, TompetDbContext data)
        {
            this.techniques = techniques;
            this.data = data;
        }

        public IActionResult All([FromQuery]
           AllTechniquesQueryModel query)
        {
            var queryResult = this.techniques.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllTechniquesQueryModel.TechniquesPerPage);

            var techniqueNames = this.techniques.AllTechniqueNames();

            query.Names = techniqueNames;
            query.TotalTechnique = queryResult.TotalTechniques;

            query.Techniques = queryResult.Techniques;

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
