namespace Tompet.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Tompet.Core.Contracts;
    using Tompet.Infrastructure.Data;
    using Tompet.Infrastructure.Data.Identity;

    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService service;

        public UserController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager, IUserService _service)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            service = _service;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
