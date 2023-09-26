using DocSession.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocSession.Application;

public class ApplicationContext : DbContext
{
  public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
  { }

  public DbSet<User> Users => Set<User>();
  public DbSet<Appointment> Appointments => Set<Appointment>();
}
