namespace Tompet.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Tompet.Infrastructure.Data;
    using Tompet.Models.Techniques;

    public class TechniquesController : Controller
    {
        private readonly TompetDbContext data;

        public TechniquesController(TompetDbContext data) => this.data = data;

        public IActionResult Add() => View(new AddTechniquesFormModel());

        [HttpPost]
        public IActionResult Add(AddTechniquesFormModel teqnique)
        {
            if (!ModelState.IsValid)
            {
                return View(teqnique);
            }

            var teqniqueData = new Technique
            {
                Name = teqnique.Name,
                Type = teqnique.Type,
                ImageUrl = teqnique.Image
            };

            this.data.Techniques.Add(teqniqueData);

            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}
