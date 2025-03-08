using Microsoft.AspNetCore.Mvc;
using PropertyManagement.DataAccess.Entities;
using PropertyManagement.DataAccess.Repositories;

namespace PropertyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantsController(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Tenant>> CreateTenant(TenantCreateDto tenantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to entity
            var tenant = new Tenant
            {
                FirstName = tenantDto.FirstName,
                LastName = tenantDto.LastName,
                Email = tenantDto.Email,
                PhoneNumber = tenantDto.PhoneNumber,
                DateOfBirth = tenantDto.DateOfBirth,
                SSN = tenantDto.SSN,
                DriverLicenseNumber = tenantDto.DriverLicenseNumber,
                Income = tenantDto.Income,
                Occupation = tenantDto.Occupation,
                Employer = tenantDto.Employer,
                EmergencyContactName = tenantDto.EmergencyContactName,
                EmergencyContactPhone = tenantDto.EmergencyContactPhone,
                EmergencyContactRelationship = tenantDto.EmergencyContactRelationship,
                IsActive = true,
                EmailNotificationsEnabled = tenantDto.EmailNotificationsEnabled,
                SmsNotificationsEnabled = tenantDto.SmsNotificationsEnabled
            };

            try
            {
                // Repository handles CRUD
                var createdTenant = await _tenantRepository.AddAsync(tenant);

                // Return success response
                return CreatedAtAction(nameof(GetTenant), new { id = createdTenant.Id }, createdTenant);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tenant>> GetTenant(int id)
        {
            var tenant = await _tenantRepository.GetByIdAsync(id);

            if (tenant == null)
            {
                return NotFound($"Tenant with ID: {id} not found");
            }

            return Ok(tenant);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tenant>>> GetAllTenants()
        {
            var tenants = await _tenantRepository.GetAllAsync();
            return Ok(tenants);
        }

        [HttpGet("unit/{unitId}")]
        public async Task<ActionResult<IEnumerable<Tenant>>> GetTenantsByUnit(int unitId)
        {
            var tenants = await _tenantRepository.GetTenantsByUnitIdAsync(unitId);

            if (tenants == null || !tenants.Any())
            {
                return NotFound($"No tenants found for unit with ID: {unitId}");
            }

            return Ok(tenants);
        }

        [HttpGet("property/{propertyId}")]
        public async Task<ActionResult<IEnumerable<Tenant>>> GetTenantsByProperty(int propertyId)
        {
            var tenants = await _tenantRepository.GetTenantsByPropertyIdAsync(propertyId);

            if (tenants == null || !tenants.Any())
            {
                return NotFound($"No tenants found for property with ID: {propertyId}");
            }

            return Ok(tenants);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTenant(int id, TenantUpdateDto tenantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTenant = await _tenantRepository.GetByIdAsync(id);

            if (existingTenant == null)
            {
                return NotFound($"Tenant with ID: {id} not found");
            }

            // Update properties
            existingTenant.FirstName = tenantDto.FirstName;
            existingTenant.LastName = tenantDto.LastName;
            existingTenant.Email = tenantDto.Email;
            existingTenant.PhoneNumber = tenantDto.PhoneNumber;
            existingTenant.Occupation = tenantDto.Occupation;
            existingTenant.Employer = tenantDto.Employer;
            existingTenant.Income = tenantDto.Income;
            existingTenant.EmergencyContactName = tenantDto.EmergencyContactName;
            existingTenant.EmergencyContactPhone = tenantDto.EmergencyContactPhone;
            existingTenant.EmergencyContactRelationship = tenantDto.EmergencyContactRelationship;
            existingTenant.IsActive = tenantDto.IsActive;
            existingTenant.EmailNotificationsEnabled = tenantDto.EmailNotificationsEnabled;
            existingTenant.SmsNotificationsEnabled = tenantDto.SmsNotificationsEnabled;

            try
            {
                // Repository handles CRUD
                await _tenantRepository.UpdateAsync(existingTenant);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            var tenant = await _tenantRepository.GetByIdAsync(id);

            if (tenant == null)
            {
                return NotFound($"Tenant with ID: {id} not found");
            }

            try
            {
                // Check if tenant has active leases
                if (tenant.Leases != null && tenant.Leases.Any(l => l.Status == LeaseStatus.Active))
                {
                    return BadRequest("Cannot delete tenant with active leases");
                }

                // Repository handles CRUD
                await _tenantRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    // DTOs
    public class TenantCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? SSN { get; set; }
        public string? DriverLicenseNumber { get; set; }
        public decimal Income { get; set; }
        public string? Occupation { get; set; }
        public string? Employer { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? EmergencyContactRelationship { get; set; }
        public bool EmailNotificationsEnabled { get; set; } = true;
        public bool SmsNotificationsEnabled { get; set; } = true;
    }

    public class TenantUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Income { get; set; }
        public string? Occupation { get; set; }
        public string? Employer { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? EmergencyContactRelationship { get; set; }
        public bool IsActive { get; set; }
        public bool EmailNotificationsEnabled { get; set; }
        public bool SmsNotificationsEnabled { get; set; }
    }
}