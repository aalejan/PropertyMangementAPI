namespace PropertyManagement.DataAccess.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime AcquisitionDate { get; set; }

        // Navigation properties
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
