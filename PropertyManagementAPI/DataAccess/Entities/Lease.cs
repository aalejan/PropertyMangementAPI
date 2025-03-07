using System.Reflection.Metadata;

namespace PropertyManagement.DataAccess.Entities
{
    public class Lease
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal SecurityDeposit { get; set; }
        public bool IsPetAllowed { get; set; }
        public decimal PetDeposit { get; set; }
        public bool IsRenewable { get; set; }
        public int RentDueDay { get; set; } = 1; // Default to 1st of month
        public int LateFeeGracePeriod { get; set; } = 5; // Default to 5 days
        public decimal LateFeeAmount { get; set; }
        public LeaseStatus Status { get; set; }
        public string TerminationReason { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime SignedDate { get; set; }
        public string Notes { get; set; }

        // Foreign Keys
        public int UnitId { get; set; }
        public int TenantId { get; set; }

        // Navigation properties
        public Unit Unit { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }

    public enum LeaseStatus
    {
        Active = 1,
        Expired = 2,
        Terminated = 3,
        Pending = 4,
        Renewed = 5
    }
}
