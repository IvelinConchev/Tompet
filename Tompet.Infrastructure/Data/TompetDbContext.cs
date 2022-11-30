namespace Tompet.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class TompetDbContext : IdentityDbContext
    {
        public TompetDbContext(DbContextOptions<TompetDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}

        public DbSet<Company> Companies { get; set; }

        public DbSet<Service> Services { get; set; }   

        public DbSet<Technique> Techniques { get; set; }
    }
}