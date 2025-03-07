// PropertyManagement.DataAccess/Repositories/PropertyRepository.cs
using Microsoft.EntityFrameworkCore;
using PropertyManagement.DataAccess.Data;
using PropertyManagement.DataAccess.Entities;

namespace PropertyManagement.DataAccess.Repositories
{
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<IEnumerable<Property>> GetPropertiesWithUnitsAsync();
    }

    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Property>> GetPropertiesWithUnitsAsync()
        {
            return await _context.Properties
                .Include(p => p.Units)
                .ToListAsync();
        }
    }
}