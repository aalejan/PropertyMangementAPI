// PropertyManagement.DataAccess/Repositories/UnitRepository.cs
using Microsoft.EntityFrameworkCore;
using PropertyManagement.DataAccess.Data;
using PropertyManagement.DataAccess.Entities;

namespace PropertyManagement.DataAccess.Repositories
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<IEnumerable<Unit>> GetUnitsByPropertyIdAsync(int propertyId);
    }

    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        public UnitRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Unit>> GetUnitsByPropertyIdAsync(int propertyId)
        {
            return await _context.Units
                .Where(u => u.PropertyId == propertyId)
                .ToListAsync();
        }
    }
}