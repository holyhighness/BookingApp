using BookingApp.Data;
using BookingApp.Models;
using Grpc.Core;

namespace BookingApp.Grpc
{
    public class BookingServiceImpl : BookingService.BookingServiceBase
    {
        private readonly BookingDbContext _dbContext;

        public BookingServiceImpl(BookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<BookResponse> Book(BookRequest request, ServerCallContext context)
        {
            var member = await _dbContext.Members.Include(m => m.Bookings).FirstOrDefaultAsync(m => m.Id == request.MemberId);
            if (member == null || member.Bookings.Count >= 2)
                return new BookResponse { Message = "Booking not allowed" };

            var item = await _dbContext.InventoryItems.Include(i => i.Bookings).FirstOrDefaultAsync(i => i.Id == request.InventoryItemId);
            if (item == null || item.Quantity <= item.Bookings.Count)
                return new BookResponse { Message = "Item out of stock" };

            var booking = new Booking
            {
                MemberId = request.MemberId,
                InventoryItemId = request.InventoryItemId,
                BookingDate = DateTime.UtcNow,
                Reference = Guid.NewGuid().ToString()
            };
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
            return new BookResponse { Reference = booking.Reference, Message = "Booking successful" };
        }

        public override async Task<CancelResponse> Cancel(CancelRequest request, ServerCallContext context)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Reference == request.Reference);
            if (booking == null) return new CancelResponse { Message = "Booking not found" };

            _dbContext.Bookings.Remove(booking);
            await _dbContext.SaveChangesAsync();
            return new CancelResponse { Message = "Booking cancelled" };
        }
    }
}