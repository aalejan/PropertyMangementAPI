using System.Reflection.Metadata;

namespace PropertyManagement.DataAccess.Entities
{
    public class MaintenanceRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
        public DateTime? DateScheduled { get; set; }
        public DateTime? DateCompleted { get; set; }
        public MaintenancePriority Priority { get; set; }
        public MaintenanceStatus Status { get; set; }
        public string CompletionNotes { get; set; }
        public decimal? Cost { get; set; }
        public bool IsTenantResponsible { get; set; }
        public bool IsRecurring { get; set; }
        public string PhotoUrl { get; set; }

        // Optional contractor information instead of full vendor entity
        public string ContractorName { get; set; }
        public string ContractorPhone { get; set; }
        public string ContractorEmail { get; set; }

        // Foreign keys
        public int UnitId { get; set; }
        public int TenantId { get; set; }

        // Navigation properties
        public Unit Unit { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<Document> Documents { get; set; } = new List<Document>();

        // For tracking notifications
        public bool InitialNotificationSent { get; set; }
        public bool CompletionNotificationSent { get; set; }
    }

    public enum MaintenancePriority
    {
        Emergency = 1,
        Urgent = 2,
        Normal = 3,
        Low = 4
    }

    public enum MaintenanceStatus
    {
        Submitted = 1,
        Scheduled = 2,
        InProgress = 3,
        Completed = 4,
        Cancelled = 5,
        Deferred = 6
    }
}
