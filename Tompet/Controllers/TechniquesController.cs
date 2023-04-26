namespace Tompet.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Tompet.Core.Services.Managers;
    using Tompet.Core.Services.Techniques;
    using Tompet.Extensions;
    using Tompet.Models.Techniques;

    public class TechniquesController : Controller
    {
        private readonly ITechniqueService techniques;
        private readonly IManagerService managers;
        private readonly IMapper mapper;

        public TechniquesController(
            ITechniqueService techniques,
            IManagerService managers, 
            IMapper mapper)
        {
            this.techniques = techniques;
            this.managers = managers;
            this.mapper = mapper;
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

            var techniqueNames = this.techniques.AllNames();

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
            if (!this.managers.isManager(this.User.Id()))
            {
                return RedirectToAction(nameof(ManagersContoller.Become), "Managers");
            }

            return View(new TechniqueFormModel
            {
                Services = this.techniques.AllServices()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(TechniqueFormModel technique)
        {
            var managerId = this.managers.IdByUser(this.User.Id());

            if (managerId.Equals(Guid.Empty))
            {
                return RedirectToAction(nameof(ManagersContoller.Become), "Managers");
            }

            if (!this.techniques.ServiceExist(technique.ServiceId))
            {
                this.ModelState.AddModelError(nameof(technique.ServiceId), "Services don't found");
            }

            if (ModelState.IsValid)
            {
                technique.Services = this.techniques.AllServices();

                return View(technique);
            }

            this.techniques.Create(
                technique.Name,
                technique.Type,
                technique.ImageUrl,
                technique.ServiceId,
                managerId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public ActionResult Edit(Guid id)
        {
            var userId = this.User.Id();

            if (!this.managers.isManager(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersContoller.Become), "Managers");
            }

            var technique = this.techniques.Details(id);

            if (technique.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
                //return BadRequest();
            }

            var techniqueForm = this.mapper.Map<TechniqueFormModel>(technique);

            techniqueForm.Services = this.techniques.AllServices();

            return View(techniqueForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Guid id, TechniqueFormModel technique)
        {
            var managerId = this.managers.IdByUser(this.User.Id());

            if (managerId.Equals(Guid.Empty) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersContoller.Become), "Managers");
            }

            if (!this.techniques.ServiceExist(technique.ServiceId))
            {
                this.ModelState.AddModelError(nameof(technique.ServiceId), "Services don't found");
            }

            if (ModelState.IsValid)
            {
                technique.Services = this.techniques.AllServices();

                return View(technique);
            }

            if (!this.techniques.IsByManager(id, managerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.techniques.Edit(
                id,
                technique.Name,
                technique.Type,
                technique.ImageUrl,
                technique.ServiceId);


            return RedirectToAction(nameof(All));
        }
    }
}
