using DocSession.Application.Models.Persons;
using DocSession.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocSession.Application;

public sealed class ApplicationContext : DbContext
{
  public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
  {
    Database.EnsureCreated();
  }

  public DbSet<User> Users => Set<User>();
  public DbSet<Appointment> Appointments => Set<Appointment>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Person>()
      .HasIndex(p => new { p.Email, p.PhoneNumber})
      .IsUnique();

    modelBuilder.Entity<Appointment>()
      .HasOne(a => a.Person)
      .WithMany(p => p.Appointments)
      .HasForeignKey(a => a.PersonId)
      .OnDelete(DeleteBehavior.Restrict)
      .IsRequired();

    modelBuilder.Entity<Appointment>()
      .HasOne(a => a.Doctor)
      .WithMany(d => d.Appointments)
      .HasForeignKey(a => a.DoctorId)
      .OnDelete(DeleteBehavior.Restrict)
      .IsRequired();

    modelBuilder.Entity<Appointment>()
      .HasOne(a => a.Status)
      .WithMany(s => s.Appointments)
      .HasForeignKey(a => a.StatusId)
      .OnDelete(DeleteBehavior.Restrict)
      .IsRequired();
  }
}
