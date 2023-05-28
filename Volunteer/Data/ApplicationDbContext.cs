using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volunteer.Models;

namespace Volunteer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }    
        public DbSet<VolunteerUser> VolunteerUsers { get; set; }
        public DbSet<SoldierUser> SoldierUsers { get; set; }    
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<MilitaryUnit> MilitaryUnits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organization>()
                .HasMany(o => o.VolunteerUsers)
                .WithOne(u => u.Organization)
                .HasForeignKey(u => u.OrganizationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<MilitaryUnit>()
                .HasMany(o => o.SoldierUsers)
                .WithOne(u => u.MilitaryUnit)
                .HasForeignKey(u => u.MilitaryUnitId)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }

}