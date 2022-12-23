namespace Tompet.Infrastructure.Data.Repositories
{
    using Tompet.Infrastructure.Data.Common;

    public class ApplicationDbRepository : Repository, IApplicationDbRepository
    {
        public ApplicationDbRepository(TompetDbContext context)
        {
            this.Context = context;
        }
    }
}
