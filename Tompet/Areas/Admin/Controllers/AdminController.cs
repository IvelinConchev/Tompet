namespace Tompet.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdminConstants.Roles.AdministratorRoleName)]
    public abstract class AdminController : Controller    
    {
    }
}
