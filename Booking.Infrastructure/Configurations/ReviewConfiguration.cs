using Booking.Domain.Apartments;
using Booking.Domain.Review;
using Booking.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Configurations;

public sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");

        builder.HasKey(review => review.Id);

        builder.Property(review => review.Rating)
            .HasConversion(rating => rating.Value, value => Rating.Create(value).Value);

        builder.Property(review => review.Comment)
            .HasMaxLength(200)
            .HasConversion(commnet => commnet.Value, value => new Comment(value));

        builder.HasOne<Apartment>()
            .WithMany()
            .HasForeignKey(review => review.ApartmentId);
        
        builder.HasOne<Apartment>()
            .WithMany()
            .HasForeignKey(review => review.BookingId);
        
        builder.HasOne<Apartment>()
            .WithMany()
            .HasForeignKey(review => review.UserId);
    }
}