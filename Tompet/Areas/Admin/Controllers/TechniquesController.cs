namespace Tompet.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    
    public class TechniquesController : AdminController
    {
        public IActionResult Index() => View();
    }
}
