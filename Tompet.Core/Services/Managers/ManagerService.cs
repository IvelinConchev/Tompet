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

        public Guid IdByUser(string userId)
          => this.data
                .Managers
                .Where(m => m.UserId == userId)
                .Select(m => m.Id)
                .FirstOrDefault();

        //string IManagerService.GetIdByUser(string userId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
