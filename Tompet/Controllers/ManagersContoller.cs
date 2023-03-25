namespace Tompet.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Tompet.Extensions;
    using Tompet.Infrastructure.Data;
    using Tompet.Infrastructure.Data.Models;
    using Tompet.Models.Managers;

    using static Tompet.Infrastructure.Data.DataConstants.Messages;

    public class ManagersContoller : Controller
    {
        private readonly TompetDbContext data;

        public ManagersContoller(TompetDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]

        public IActionResult Become(BecomeManagerFormModel manager)
        {
            var userId = this.User.Id();

            var userIdAlreadyManager = this.data
                .Managers
                .Any(m => m.UserId == userId);

            if (userIdAlreadyManager)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(manager);
            }

            var managerData = new Manager
            {
                Name = manager.Name,
                PhoneNumber = manager.PhoneNumber,
                UserId = userId
            };

            this.data.Managers.Add(managerData);
            this.data.SaveChanges();

            this.TempData[GlobalMessageKey] = "Thank you for becomming a manager!";

            return RedirectToAction("All", "Techniques");
        }
    }
}
