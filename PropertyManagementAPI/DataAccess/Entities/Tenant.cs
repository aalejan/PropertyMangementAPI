namespace PropertyManagement.DataAccess.Entities
{
    public class Tenant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? SSN { get; set; } // Consider encryption for sensitive data
        public string? DriverLicenseNumber { get; set; }
        public decimal Income { get; set; }
        public string? Occupation { get; set; }
        public string? Employer { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? EmergencyContactRelationship { get; set; }
        public bool IsActive { get; set; } = true;

        // For SendGrid notifications
        public bool EmailNotificationsEnabled { get; set; } = true;
        public bool SmsNotificationsEnabled { get; set; } = true;

        // Navigation properties
        public ICollection<Lease> Leases { get; set; } = new List<Lease>();
        public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();

        // Helper property
        public string FullName => $"{FirstName} {LastName}";
    }
}
