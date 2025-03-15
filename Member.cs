namespace BookingApp.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}