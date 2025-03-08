namespace PropertyManagement.DataAccess.Entities
{
    public class Unit
    {
        public int Id { get; set; }
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

        // Navigation properties
        public Property Property { get; set; }
        public ICollection<Lease> Leases { get; set; } = new List<Lease>();
        public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();
    }

    public enum UnitType
    {
        Apartment = 1,
        House = 2,
        Studio = 3,
        Office = 4,
        Retail = 5
    }
}
