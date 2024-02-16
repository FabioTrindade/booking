using Booking.Domain.Abstractions;

namespace Booking.Domain.Apartments;

public static class ApartmentErrors
{
    public static Error NotFound = new(
        "Apartment.NotFound",
        "The apartment with the specific identifier was not found"
        );
}