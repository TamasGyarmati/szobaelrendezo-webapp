using BackendASP.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendASP.Data;

public class AppDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public AppDbContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connString = "Server=localhost,1433;Database=szobaelrendezoDb;User Id=sa;Password=SqlServerPassw0rd!;TrustServerCertificate=True";
        optionsBuilder.UseSqlServer(connString);
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // PUT request miatt kell importolni data-t
        modelBuilder.Entity<Room>().HasData(
            new Room()
            {
                Id = 1,
                Height = 0,
                Width = 0,
            });
        base.OnModelCreating(modelBuilder);
    }
}