namespace Booking.Application.Bookings.GetBooking;

public sealed class BookingResponse
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public Guid ApartmentId { get; init; }
}