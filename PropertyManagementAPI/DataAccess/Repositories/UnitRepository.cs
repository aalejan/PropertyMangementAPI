using Microsoft.EntityFrameworkCore;
using PropertyManagement.DataAccess.Data;
using PropertyManagement.DataAccess.Entities;

namespace PropertyManagement.DataAccess.Repositories
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<IEnumerable<Unit>> GetByPropertyIdAsync(int propertyId);
        Task<IEnumerable<Unit>> GetAvailableUnitsAsync();
    }

    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        public UnitRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<Unit>> GetAllAsync()
        {
            return await _context.Units
                .Include(u => u.Property)
                .ToListAsync();
        }

        public override async Task<Unit> GetByIdAsync(int id)
        {
            return await _context.Units
                .Include(u => u.Property)
                .Include(u => u.Leases)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Unit>> GetByPropertyIdAsync(int propertyId)
        {
            return await _context.Units
                .Where(u => u.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Unit>> GetAvailableUnitsAsync()
        {
            return await _context.Units
                .Where(u => !u.IsOccupied)
                .Include(u => u.Property)
                .ToListAsync();
        }

        public override async Task<Unit> AddAsync(Unit entity)
        {
            await _context.Units.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public override async Task UpdateAsync(Unit entity)
        {
            _context.Units.Update(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task DeleteAsync(int id)
        {
            var unit = await GetByIdAsync(id);
            if (unit != null)
            {
                // Check if unit has active leases before allowing deletion
                if (unit.Leases != null && unit.Leases.Any(l => l.Status == LeaseStatus.Active))
                {
                    throw new InvalidOperationException("Cannot delete unit with active leases");
                }

                _context.Units.Remove(unit);
                await _context.SaveChangesAsync();
            }
        }
    }
}