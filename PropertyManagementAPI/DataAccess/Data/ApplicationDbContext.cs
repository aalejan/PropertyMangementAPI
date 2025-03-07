using Microsoft.EntityFrameworkCore;
using PropertyManagement.DataAccess.Entities;

namespace PropertyManagement.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Lease> Leases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Property to Unit relationship
            modelBuilder.Entity<Property>()
                .HasMany(p => p.Units)
                .WithOne(u => u.Property)
                .HasForeignKey(u => u.PropertyId);

            // Unit to Lease relationship
            modelBuilder.Entity<Unit>()
                .HasMany(u => u.Leases)
                .WithOne(l => l.Unit)
                .HasForeignKey(l => l.UnitId);

            // Tenant to Lease relationship
            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.Leases)
                .WithOne(l => l.Tenant)
                .HasForeignKey(l => l.TenantId);

            // Basic constraints for required fields
            modelBuilder.Entity<Property>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Tenant>()
                .Property(t => t.Email)
                .IsRequired();
        }
    }
}
