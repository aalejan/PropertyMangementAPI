namespace PropertyManagement.DataAccess.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public PaymentType Type { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public string TransactionId { get; set; }
        public string Notes { get; set; }
        public bool IsLateFeeApplied { get; set; }
        public decimal LateFeeAmount { get; set; }
        public decimal TotalAmount => Amount + (IsLateFeeApplied ? LateFeeAmount : 0);

        // Foreign Keys
        public int LeaseId { get; set; }
        public int TenantId { get; set; }

        // Navigation properties
        public Lease Lease { get; set; }
        public Tenant Tenant { get; set; }
    }

    public enum PaymentType
    {
        Rent = 1,
        SecurityDeposit = 2,
        PetDeposit = 3,
        LateFee = 4,
        Utility = 5,
        Maintenance = 6,
        Other = 7
    }

    public enum PaymentMethod
    {
        Cash = 1,
        Check = 2,
        CreditCard = 3,
        BankTransfer = 4,
        PayPal = 5,
        Venmo = 6,
        Other = 7
    }

    public enum PaymentStatus
    {
        Pending = 1,
        Paid = 2,
        Late = 3,
        Overdue = 4,
        Refunded = 5,
        Cancelled = 6
    }
}
