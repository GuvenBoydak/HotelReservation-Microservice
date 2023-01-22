using HotelReservationService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationService.Infrastracture.EntityConfigurations;

public class ReservationConfiguration:IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Pax).IsRequired();
        builder.Property(x => x.ArrivalDate).IsRequired();
        builder.Property(x => x.DepartureDate).IsRequired();
        builder.Property(x => x.ReservationName).IsRequired();
        builder.Property(x => x.ReservationEmail).IsRequired();
        builder.Property(x => x.ReservationPhoneNumber).IsRequired();
    }
}