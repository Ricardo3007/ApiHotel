using System;
using System.Collections.Generic;
using ApiHotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiHotel.Infrastructure.Context;

public partial class HotelContext : DbContext
{
    public HotelContext()
    {
    }

    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agent { get; set; }

    public virtual DbSet<AgentHotel> AgentHotel { get; set; }

    public virtual DbSet<Guest> Guest { get; set; }

    public virtual DbSet<Hotel> Hotel { get; set; }

    public virtual DbSet<Reservation> Reservation { get; set; }

    public virtual DbSet<Room> Room { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.AgentId).HasName("PK__Agent__9AC3BFF1ABCBBD5E");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<AgentHotel>(entity =>
        {
            entity.HasKey(e => e.AgentHotelId).HasName("PK__AgentHot__5F066E633D3BC604");

            entity.HasOne(d => d.Agent).WithMany(p => p.AgentHotel)
                .HasForeignKey(d => d.AgentId)
                .HasConstraintName("FK__AgentHote__Agent__5165187F");

            entity.HasOne(d => d.Hotel).WithMany(p => p.AgentHotel)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__AgentHote__Hotel__52593CB8");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.GuestId).HasName("PK__Guest__0C423C12F93A3089");

            entity.Property(e => e.DocumentNumber).HasMaxLength(50);
            entity.Property(e => e.DocumentType).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasOne(d => d.Reservation).WithMany(p => p.Guest)
                .HasForeignKey(d => d.ReservationId)
                .HasConstraintName("FK__Guest__Reservati__59063A47");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PK__Hotel__46023BDFC1BC2F8F");

            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B7EE5F2464A858BA");

            entity.Property(e => e.CheckInDate).HasColumnType("datetime");
            entity.Property(e => e.CheckOutDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmergencyContactName).HasMaxLength(255);
            entity.Property(e => e.EmergencyContactPhone).HasMaxLength(20);

            entity.HasOne(d => d.Room).WithMany(p => p.Reservation)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Reservati__RoomI__5535A963");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__3286393989923E2E");

            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.RoomType).HasMaxLength(100);
            entity.Property(e => e.Taxes).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Room)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__Room__HotelId__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
