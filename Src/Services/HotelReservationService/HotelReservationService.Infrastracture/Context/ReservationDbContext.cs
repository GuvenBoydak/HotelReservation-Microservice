using HotelReservationService.Domain.Models;
using HotelReservationService.Infrastracture.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationService.Infrastracture.Context;

public class ReservationDbContext:DbContext
{
    public ReservationDbContext(DbContextOptions options):base(options)
    { }


    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        modelBuilder.ApplyConfiguration(new PackageConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());
    }
}