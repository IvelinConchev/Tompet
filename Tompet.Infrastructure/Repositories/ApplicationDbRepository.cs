namespace Tompet.Infrastructure.Repositories
{
    using Tompet.Infrastructure.Data;
    using Tompet.Infrastructure.Data.Common;

    public class ApplicationDbRepository : Repository, IApplicationDbRepository
    {
        public ApplicationDbRepository(TompetDbContext context)
        {
            this.Context = context;
        }
    }
}
