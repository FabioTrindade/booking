using Booking.Domain.Abstractions;

namespace Booking.Domain.Bookings;

public static class BookingErrors
{
    public static Error NotFound = new(
        "Bookink.Found",
        "The booking with the specific identifier was not found");

    public static Error OverLap = new(
        "Booking.Overlap",
        "The current booking is overlapping with as existing one");

    public static Error NotReserved = new(
        "Booking.NotReserved",
        "The booking is not pending");

    public static Error NotConfirmed = new(
        "Booking.NotConfirmed",
        "The booking is not confirmed");

    public static Error AlreadyStarted = new(
        "Booking.AlreadyStarted",
        "The booking has already started");
}
