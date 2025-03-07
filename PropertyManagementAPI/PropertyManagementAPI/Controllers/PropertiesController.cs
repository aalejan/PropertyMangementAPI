using Microsoft.AspNetCore.Mvc;
using PropertyManagement.DataAccess.Entities;
using PropertyManagement.DataAccess.Repositories;

namespace PropertyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertiesController(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Property>> CreateProperty(PropertyCreateDto propertyDto)
        {
            // Map DTO to entity
            var property = new Property
            {
                Name = propertyDto.Name,
                Address = propertyDto.Address,
                City = propertyDto.City,
                State = propertyDto.State,
                ZipCode = propertyDto.ZipCode,
                AcquisitionDate = (DateTime)propertyDto.AcquisitionDate,
                PurchasePrice = (decimal)propertyDto.PurchasePrice,
                // Don't include Units here - we'll handle those separately
            };

            try
            {
                // Add to database
                var createdProperty = await _propertyRepository.AddAsync(property);

                // Return success response
                return CreatedAtAction(nameof(GetProperty), new { id = createdProperty.Id }, createdProperty);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(int id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            return Ok(property);
        }
    }

    // Simple DTO for property creation
    public class PropertyCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? AcquisitionDate { get; set; }
    }
}