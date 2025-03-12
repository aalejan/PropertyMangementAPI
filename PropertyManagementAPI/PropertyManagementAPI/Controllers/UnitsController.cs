using Microsoft.AspNetCore.Mvc;
using PropertyManagement.DataAccess.Entities;
using PropertyManagement.DataAccess.Repositories;

namespace PropertyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitRepository _unitRepository;

        public UnitsController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateUnit(UnitCreateDto unitDto)
        {
            // Map DTO to entity
            var unit = new Unit
            {
                UnitNumber = unitDto.UnitNumber,
                PropertyId = unitDto.PropertyId,
                Bedrooms = unitDto.Bedrooms,
                Bathrooms = (int)unitDto.Bathrooms,
                SquareFootage = unitDto.SquareFootage,
                MonthlyRent = unitDto.MonthlyRent,
                IsOccupied = false // Default to not occupied for new units
            };

            try
            {
                // Let repository handle CRUD
                var createdUnit = await _unitRepository.AddAsync(unit);

                // Return success response
                return CreatedAtAction(nameof(GetUnit), new { id = createdUnit.Id }, createdUnit);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return Ok(unit);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetAllUnits()
        {
            var units = await _unitRepository.GetAllAsync();
            return Ok(units);
        }

        [HttpGet("property/{propertyId}")]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnitsByProperty(int propertyId)
        {
            var units = await _unitRepository.GetByPropertyIdAsync(propertyId);

            if (units == null || !units.Any())
            {
                return NotFound($"No units found for property with ID: {propertyId}");
            }

            return Ok(units);
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Unit>>> GetAvailableUnits()
        {
            var units = await _unitRepository.GetAvailableUnitsAsync();
            return Ok(units);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, UnitUpdateDto unitDto)
        {
            var existingUnit = await _unitRepository.GetByIdAsync(id);

            if (existingUnit == null)
            {
                return NotFound($"Unit with ID: {id} not found");
            }

            // Update properties
            existingUnit.UnitNumber = unitDto.UnitNumber;
            existingUnit.Bedrooms = unitDto.Bedrooms;
            existingUnit.Bathrooms = (int)unitDto.Bathrooms;
            existingUnit.SquareFootage = unitDto.SquareFootage;
            existingUnit.MonthlyRent = unitDto.MonthlyRent;
            existingUnit.IsOccupied = unitDto.IsOccupied;

            try
            {
                // Let repository handle CRUD
                await _unitRepository.UpdateAsync(existingUnit);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);

            if (unit == null)
            {
                return NotFound($"Unit with ID: {id} not found");
            }

            try
            {
                // Let repository handle CRUD and validation
                await _unitRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    // DTOs
    public class UnitCreateDto
    {
        public string UnitNumber { get; set; }
        public decimal SquareFootage { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal SecurityDeposit { get; set; }
        public UnitType Type { get; set; }
        public string? Description { get; set; }
        public string? Amenities { get; set; }
        public DateTime? LastRenovationDate { get; set; }

        // Foreign Keys
        public int PropertyId { get; set; }
    }

    public class UnitUpdateDto
    {
        public string UnitNumber { get; set; }
        public decimal SquareFootage { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal SecurityDeposit { get; set; }
        public bool IsOccupied { get; set; }
        public UnitType Type { get; set; }
        public string? Description { get; set; }
        public string? Amenities { get; set; }
        public DateTime? LastRenovationDate { get; set; }

        // Foreign Keys
        public int PropertyId { get; set; }
    }
}