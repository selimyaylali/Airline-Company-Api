using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace midterm_SE4458.Model;

public partial class SelimContext : DbContext
{
    public SelimContext()
    {
    }

    public SelimContext(DbContextOptions<SelimContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendant> Attendants { get; set; }

    public virtual DbSet<AttendantFlight> AttendantFlights { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:syaylalimidterm.database.windows.net,1433;Initial Catalog=selim;Persist Security Info=False;User ID=20070006072@stu.yasar.edu.tr;Password=Rzr53abb;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendant>(entity =>
        {
            entity.HasKey(e => e.AttendantId).HasName("PK__Attendan__7455993743B6CFF5");

            entity.ToTable("Attendant");

            entity.HasIndex(e => e.Username, "UQ__Attendan__536C85E4C0D3D978").IsUnique();

            entity.Property(e => e.AttendantId).HasColumnName("AttendantID");
            entity.Property(e => e.AttendantPassword).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<AttendantFlight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC27E7ABB321");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AttendantId).HasColumnName("AttendantID");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");

            entity.HasOne(d => d.Attendant).WithMany(p => p.AttendantFlights)
                .HasForeignKey(d => d.AttendantId)
                .HasConstraintName("FK__Attendant__Atten__75A278F5");

            entity.HasOne(d => d.Flight).WithMany(p => p.AttendantFlights)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__Attendant__Fligh__76969D2E");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flight__8A9E148E804EA24B");

            entity.ToTable("Flight");

            entity.HasIndex(e => e.FlightNumber, "UQ__Flight__2EAE6F50112044F7").IsUnique();

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.Departure).HasMaxLength(50);
            entity.Property(e => e.Destination).HasMaxLength(50);
            entity.Property(e => e.FlightDate).HasColumnType("date");
            entity.Property(e => e.FlightNumber).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
