using Microsoft.EntityFrameworkCore;
using BookingApp.Models;

namespace BookingApp.Data
{
    public class BookingDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }
    }
}