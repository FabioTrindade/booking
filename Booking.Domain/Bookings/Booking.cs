using Booking.Domain.Abstractions;
using Booking.Domain.Apartments;
using Booking.Domain.Bookings.Events;
using Booking.Domain.Shared;
using System.Runtime.CompilerServices;

namespace Booking.Domain.Bookings;

public sealed class Booking : Entity
{
    private Booking(
        Guid id,
        Guid userId,
        Guid apartmentId,
        DateRange duration,
        Money priceForPeriod,
        Money cleaningFee,
        Money amenitiesUpCharge,
        Money totalPrice,
        BookingStatus status,
        DateTime createdOnUtc)
        : base(id)
    {
        UserId = userId;
        ApartmentId = apartmentId;
        Duration = duration;
        PriceForPeriod = priceForPeriod;
        CleaningFee = cleaningFee;
        AmenitiesUpCharge = amenitiesUpCharge;
        TotalPrice = totalPrice;
        Status = status;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid ApartmentId { get; private set; }

    public Guid UserId { get; private set; }

    public DateRange Duration { get; private set; }

    public Money PriceForPeriod { get; private set; }

    public Money CleaningFee { get; private set; }

    public Money AmenitiesUpCharge { get; private set; }

    public Money TotalPrice { get; private set; }

    public BookingStatus Status { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ConfirmedOnUtc { get; private set; }

    public DateTime? RejectedOnUtc { get; private set; }

    public DateTime? CompletedOnUtc { get; private set; }

    public DateTime? CancelledOnUtc { get; private set; }


    public static Booking Reserve(
        Guid userId,
        Apartment apartment,
        DateRange duration,
        DateTime utcnow,
        PricingService pricingService)
    {
        var pricingDetails = pricingService.CalcutatePrice(apartment, duration);

        var booking = new Booking(
            Guid.NewGuid(),
            userId,
            apartment.Id,
            duration,
            pricingDetails.PriceForPeriod,
            pricingDetails.CleaningFee,
            pricingDetails.AmenitiesUpCharge,
            pricingDetails.TotalPrice,
            BookingStatus.Reserved,
            utcnow
            );

        booking.RaiseDomainEvents(new BookingReservedDomainEvent(booking.Id));

        apartment.LastBookedOnUtc = utcnow;

        return booking;
    }

    public Result Confirm(DateTime utcNow)
    {
        if (Status != BookingStatus.Reserved)
            return Result.Failure(BookingErrors.NotPending);

        Status = BookingStatus.Confirmed;
        ConfirmedOnUtc = utcNow;

        RaiseDomainEvents(new BookingConfirmedDomainEvent(Id));

        return Result.Success();
    }

    public Result Reject(DateTime utcNow)
    {
        if (Status != BookingStatus.Reserved)
            return Result.Failure(BookingErrors.NotReserved);

        Status = BookingStatus.Rejected;
        ConfirmedOnUtc = utcNow;

        RaiseDomainEvents(new BookingRejectedDomainEvent(Id));

        return Result.Success();
    }

    public Result Complete(DateTime utcNow)
    {
        if (Status != BookingStatus.Confirmed)
            return Result.Failure(BookingErrors.NotConfirmed);

        Status = BookingStatus.Completed;
        ConfirmedOnUtc = utcNow;

        RaiseDomainEvents(new BookingCompletedDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel(DateTime utcNow)
    {
        if(Status != BookingStatus.Confirmed)
            return Result.Failure(BookingErrors.NotConfirmed);

        var currentDate = DateOnly.FromDateTime(utcNow);

        if(currentDate > Duration.Start)
            return Result.Failure(BookingErrors.AlreadyStarted);

        Status = BookingStatus.Cancelled;
        ConfirmedOnUtc = utcNow;

        RaiseDomainEvents(new BookingCancelledDomainEvent(Id));

        return Result.Success();
    }
}
