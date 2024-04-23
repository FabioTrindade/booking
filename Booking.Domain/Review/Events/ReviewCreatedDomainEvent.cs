using Booking.Domain.Abstractions;

namespace Booking.Domain.Review.Events;

public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;