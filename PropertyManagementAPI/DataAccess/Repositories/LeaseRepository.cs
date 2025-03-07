// PropertyManagement.DataAccess/Repositories/LeaseRepository.cs
using Microsoft.EntityFrameworkCore;
using PropertyManagement.DataAccess.Data;
using PropertyManagement.DataAccess.Entities;

namespace PropertyManagement.DataAccess.Repositories
{
    public interface ILeaseRepository : IRepository<Lease>
    {
        Task<IEnumerable<Lease>> GetActiveLeasesByUnitIdAsync(int unitId);
        Task<IEnumerable<Lease>> GetLeasesByTenantIdAsync(int tenantId);
        Task<IEnumerable<Lease>> GetExpiringLeasesAsync(int daysToExpiration);
    }

    public class LeaseRepository : Repository<Lease>, ILeaseRepository
    {
        public LeaseRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Lease>> GetActiveLeasesByUnitIdAsync(int unitId)
        {
            return await _context.Leases
                .Where(l => l.UnitId == unitId && l.EndDate > DateTime.Now)
                .Include(l => l.Tenant)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lease>> GetLeasesByTenantIdAsync(int tenantId)
        {
            return await _context.Leases
                .Where(l => l.TenantId == tenantId)
                .Include(l => l.Unit)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lease>> GetExpiringLeasesAsync(int daysToExpiration)
        {
            var cutoffDate = DateTime.Now.AddDays(daysToExpiration);
            return await _context.Leases
                .Where(l => l.EndDate <= cutoffDate && l.EndDate > DateTime.Now)
                .Include(l => l.Unit)
                .Include(l => l.Tenant)
                .ToListAsync();
        }
    }
}
