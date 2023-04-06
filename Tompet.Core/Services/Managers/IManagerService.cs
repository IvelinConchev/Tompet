namespace Tompet.Core.Services.Managers
{
    public interface IManagerService
    {
        public bool isManager(string userId);

        public Guid IdByUser(string userId);
    }
}
