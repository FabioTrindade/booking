using Booking.Domain.Apartments;
using Booking.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

internal sealed class BookingRepository : Repository<Domain.Bookings.Booking>, IBookingRepository
{
    private static readonly BookingStatus[] ActiveBookingStatuses =
        {
            Domain.Bookings.BookingStatus.Reserved,
            Domain.Bookings.BookingStatus.Confirmed,
            Domain.Bookings.BookingStatus.Completed
        };

    public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(
        Apartment apartment,
        DateRange duration,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Domain.Bookings.Booking>()
            .AnyAsync(booking =>
                    booking.ApartmentId == apartment.Id &&
                    booking.Duration.Start <= duration.End &&
                    booking.Duration.End >= duration.Start &&
                    ActiveBookingStatuses.Contains(booking.Status),
                cancellationToken);
    }
}