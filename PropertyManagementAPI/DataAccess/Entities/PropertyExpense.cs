namespace PropertyManagement.DataAccess.Entities
{
    public class PropertyExpense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; }
        public string Notes { get; set; }
        public string ReceiptUrl { get; set; }
        public bool IsTaxDeductible { get; set; }
        public string VendorName { get; set; }
        public bool IsRecurring { get; set; }
        public RecurringFrequency? RecurringFrequency { get; set; }

        // Foreign Keys
        public int PropertyId { get; set; }
        public int? UnitId { get; set; } // Optional - if expense is unit-specific

        // Navigation properties
        public Property Property { get; set; }
        public Unit Unit { get; set; }
    }

    public enum ExpenseCategory
    {
        Mortgage = 1,
        Insurance = 2,
        PropertyTax = 3,
        Utilities = 4,
        Maintenance = 5,
        Repairs = 6,
        Management = 7,
        Legal = 8,
        Accounting = 9,
        Advertising = 10,
        CapitalImprovement = 11,
        Other = 12
    }

    public enum RecurringFrequency
    {
        Daily = 1,
        Weekly = 2,
        BiWeekly = 3,
        Monthly = 4,
        Quarterly = 5,
        Annually = 6
    }
}
