using Booking.Domain.Abstractions;

namespace Booking.Domain.Users;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.Found",
        "The user with the specifier identifier was not found"
    );

    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "The provider credentials were invalid"
    );
}