namespace Tompet.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Tompet.Infrastructure.Data;
    using Tompet.Infrastructure.Data.Models;
    using Tompet.Models.Services;

    public class ServicesController : Controller
    {
        private readonly TompetDbContext data;

        public ServicesController(TompetDbContext data) => this.data = data;

        public IActionResult Add() => View(new AddServicesFormModel());

        [HttpPost]

        public IActionResult Add(AddServicesFormModel service)
        {
            if (!ModelState.IsValid)
            {
                return View(service);
            }

            var serviceData = new Service()
            {
                Name = service.Name,
                //Description = service.Description,
            };

            this.data.Services.Add(serviceData);

            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
