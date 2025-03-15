namespace BookingApp.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}