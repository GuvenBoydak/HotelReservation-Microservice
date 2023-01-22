using HotelReservationService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationService.Infrastracture.EntityConfigurations;

public class PackageConfiguration:IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(40).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Price).IsRequired();
    }
}