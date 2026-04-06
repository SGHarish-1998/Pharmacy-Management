namespace PharmacyApi.Models
{
    public class Sale
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MedicineId { get; set; }
        public string MedicineName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    }
}
