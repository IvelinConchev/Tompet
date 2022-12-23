namespace Tompet.Core.Contracts
{
    using Tompet.Core.Models;
    using Tompet.Infrastructure.Data.Identity;

    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<UserEditViewModel> GetUserForEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);

        Task<ApplicationUser> GetUserById(string id);
    }
}
