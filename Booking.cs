namespace BookingApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int InventoryItemId { get; set; }
        public InventoryItem InventoryItem { get; set; }
        public DateTime BookingDate { get; set; }
        public string Reference { get; set; }
    }
}