namespace Tompet.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Tompet.Infrastructure.Data.Identity;

    public class TompetDbContext : IdentityDbContext<ApplicationUser>
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

        public DbSet<ApplicationFile> Files { get; set; }

    }
}