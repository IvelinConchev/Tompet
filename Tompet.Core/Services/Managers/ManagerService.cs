namespace Tompet.Core.Services.Managers
{
    using Tompet.Infrastructure.Data;

    public class ManagerService : IManagerService
    {
        private readonly TompetDbContext data;

        public ManagerService(TompetDbContext data)
            => this.data = data;

        public bool isManager(string userId)
          => this.data
            .Managers
            .Any(m => m.UserId == userId);
    }
}
