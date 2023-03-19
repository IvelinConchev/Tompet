namespace Tompet.Core.Services.Statistics
{
    using Tompet.Infrastructure.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly TompetDbContext data;

        public StatisticsService(TompetDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalTechniques = this.data.Techniques.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalTechniques = totalTechniques,
                TotalUsers = totalUsers,
            };
        }
    }
}
