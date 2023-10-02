using DocSession.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocSession.Application;

public class ApplicationContext : DbContext
{
  public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
  { }

  public DbSet<User> Users => Set<User>();
  public DbSet<Appointment> Appointments => Set<Appointment>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>()
      .HasKey(u => u.Id)
      .HasName("PrimaryKey_UserId");

    modelBuilder.Entity<User>()
      .HasIndex(u => new { u.Username, u.Email })
      .IsUnique();

    modelBuilder.Entity<Appointment>()
      .HasKey(a => a.Id)
      .HasName("PrimaryKey_AppointmentId");
  }
}
