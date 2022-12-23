namespace Tompet.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Tompet.Infrastructure.Data;

    [Authorize(Roles = DataConstants.UserConstant.Roles.Administrator)]
    [Area("Admin")]
    public class BaseController : Controller
    {
    }
}
