namespace Booking.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(Domain.Users.Email, string subject, string body);
}