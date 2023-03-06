namespace Tompet.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Tompet.Infrastructure.Data;
    using Tompet.Models.Techniques;

    public class TechniquesController : Controller
    {
        private readonly TompetDbContext data;

        public TechniquesController(TompetDbContext data) => this.data = data;

        public IActionResult Add() => View(new AddTechniquesFormModel
        {
            Services = this.GetTechniqueServices()
        });

        public IActionResult All()
        {
            var tecniques = this.data
                .Techniques
                .OrderByDescending(t => t.Id)
                .Select(c => new TechniqueListingViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    ImageUrl = c.ImageUrl,
                    Service = c.Service.Name
                });

            return View(tecniques);
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

            return RedirectToAction("Index", "Home");
        }

    }
}
