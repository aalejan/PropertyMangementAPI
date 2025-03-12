using Microsoft.AspNetCore.Mvc;
using PropertyManagement.DataAccess.Entities;
using PropertyManagement.DataAccess.Repositories;

namespace PropertyManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaseController : ControllerBase
    {

        private readonly ILeaseRepository _leaseRepository;

        public LeaseController(ILeaseRepository leaseRepository)
        {
            _leaseRepository = leaseRepository;
        }

        [HttpGet("lease/{unitId}")]
        public async Task<ActionResult<Lease>> GetLeaseByUnitID(int unitId)
        {

            var lease = await _leaseRepository.GetActiveLeasesByUnitIdAsync(unitId);

            if (lease == null)
            {
                return NotFound($"No active leases found with this unit id: {unitId}");
            }

            return Ok(lease);
        }




    }
}
