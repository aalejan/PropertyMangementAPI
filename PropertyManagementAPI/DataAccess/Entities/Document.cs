namespace PropertyManagement.DataAccess.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public long FileSizeBytes { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public DocumentType Type { get; set; }
        public DateTime? ExpirationDate { get; set; }

        // Foreign Keys - nullable to allow association with different entities
        public int? PropertyId { get; set; }
        public int? TenantId { get; set; }
        public int? LeaseId { get; set; }
        public int? MaintenanceRequestId { get; set; }

        // Navigation properties
        public Property Property { get; set; }
        public Tenant Tenant { get; set; }
        public Lease Lease { get; set; }
        public MaintenanceRequest MaintenanceRequest { get; set; }
    }

    public enum DocumentType
    {
        Lease = 1,
        Insurance = 2,
        PropertyTax = 3,
        Deed = 4,
        TenantApplication = 5,
        TenantId = 6,
        TenantIncomeVerification = 7,
        MaintenanceReceipt = 8,
        InvoiceReceipt = 9,
        PropertyPhoto = 10,
        Inspection = 11,
        LegalDocument = 12,
        ContractorInvoice = 13,
        Other = 14
    }
}
