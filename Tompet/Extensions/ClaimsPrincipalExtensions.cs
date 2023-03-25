namespace Tompet.Extensions
{
    using System.Security.Claims;

    using static Tompet.Infrastructure.Data.DataConstants.UserConstant.Roles;
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }
}
