// PropertyManagement.DataAccess/Repositories/TenantRepository.cs
using Microsoft.EntityFrameworkCore;
using PropertyManagement.DataAccess.Data;
using PropertyManagement.DataAccess.Entities;

namespace PropertyManagement.DataAccess.Repositories
{
    public interface ITenantRepository : IRepository<Tenant>
    {
        Task<IEnumerable<Tenant>> GetTenantsByUnitIdAsync(int unitId);
        Task<IEnumerable<Tenant>> GetTenantsByPropertyIdAsync(int propertyId);
    }

    public class TenantRepository : Repository<Tenant>, ITenantRepository
    {
        public TenantRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Tenant>> GetTenantsByUnitIdAsync(int unitId)
        {
            return await _context.Tenants
                .Where(t => t.Leases.Any(l => l.UnitId == unitId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Tenant>> GetTenantsByPropertyIdAsync(int propertyId)
        {
            return await _context.Tenants
                .Where(t => t.Leases.Any(l => l.Unit.PropertyId == propertyId))
                .ToListAsync();
        }
    }
}
