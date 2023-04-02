namespace Tompet.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Tompet.Core.Services.Managers;
    using Tompet.Core.Services.Techniques;
    using Tompet.Extensions;
    using Tompet.Infrastructure.Data;
    using Tompet.Infrastructure.Data.Models;
    using Tompet.Models.Techniques;

    public class TechniquesController : Controller
    {
        private readonly ITechniqueService techniques;
        private readonly TompetDbContext data;
        private readonly IManagerService managers;

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

        [Authorize]
        public IActionResult Mine()
        {
            var myTechniques = this.techniques.ByUser(this.User.Id());

            return View(myTechniques);
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsManager())
            {
                return RedirectToAction(nameof(ManagersContoller.Become), "Managers");
            }

            return View(new AddTechniquesFormModel
            {
                Services = this.GetTechniqueServices()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddTechniquesFormModel teqnique)
        {
            var managerId = this.data
                .Managers
                .Where(m => m.UserId == this.User.Id())
                .Select(m => m.Id)
                .FirstOrDefault();

            if (managerId.Equals(Guid.Empty))
            {
                return RedirectToAction(nameof(ManagersContoller.Become), "Managers");
            }

            if (!this.data.Services.Any(s => s.Id == teqnique.ServiceId))
            {
                this.ModelState.AddModelError(nameof(teqnique.ServiceId), "Services don't found");
            }

            if (ModelState.IsValid)
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
                ManagerId = managerId
            };

            this.data.Techniques.Add(teqniqueData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsManager()
            => this.data
            .Managers
            .Any(m => m.UserId == this.User.Id());

        private IEnumerable<TecniqueServiceViewModel> GetTechniqueServices()
            => this.data
            .Services
            .Select(s => new TecniqueServiceViewModel
            {
                Id = s.Id,
                Name = s.Name,
            })
            .ToList();
    }
}
