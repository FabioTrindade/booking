using Booking.Domain.Abstractions;

namespace Booking.Domain.Users.Events;

public sealed record UserCreateDomainEvent(Guid userId) : IDomainEvent;
