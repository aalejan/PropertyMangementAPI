namespace PropertyManagement.DataAccess.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? SentDate { get; set; }
        public NotificationType Type { get; set; }
        public NotificationStatus Status { get; set; }
        public NotificationPriority Priority { get; set; }
        public string RecipientEmail { get; set; }
        public string RecipientPhone { get; set; }
        public string ErrorMessage { get; set; }
        public int? RetryCount { get; set; }
        public DateTime? ScheduledDate { get; set; }

        // Foreign Keys - nullable to allow association with different entities
        public int? TenantId { get; set; }
        public int? PropertyId { get; set; }
        public int? UnitId { get; set; }
        public int? LeaseId { get; set; }
        public int? MaintenanceRequestId { get; set; }
        public int? PaymentId { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; }
        public Property Property { get; set; }
        public Unit Unit { get; set; }
        public Lease Lease { get; set; }
        public MaintenanceRequest MaintenanceRequest { get; set; }
        public Payment Payment { get; set; }
    }

    public enum NotificationType
    {
        Email = 1,
        SMS = 2,
        PushNotification = 3
    }

    public enum NotificationStatus
    {
        Pending = 1,
        Sent = 2,
        Failed = 3,
        Scheduled = 4,
        Cancelled = 5
    }

    public enum NotificationPriority
    {
        Low = 1,
        Normal = 2,
        High = 3,
        Urgent = 4
    }
}
