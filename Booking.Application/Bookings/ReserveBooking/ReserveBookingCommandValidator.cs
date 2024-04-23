using FluentValidation;

namespace Booking.Application.Bookings.ReserveBooking;

public class ReserveBookingCommandValidator : AbstractValidator<ReserveBookingCommand>
{
    public ReserveBookingCommandValidator()
    {
        RuleFor(r => r.UserId).NotEmpty();
        RuleFor(r => r.ApartmentId).NotEmpty();
        RuleFor(r => r.StarDate).LessThan(rf => rf.EndDate);
    }
}