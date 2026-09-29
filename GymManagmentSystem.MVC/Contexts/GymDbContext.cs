using GymManagmentSystem.MVC.Configurations;
using GymManagmentSystem.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem.MVC.Contexts;
public class GymDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=NASR\\SQLEXPRESS;Database=GymManagmentDb;Trusted_Connection=True;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlanConfigurations());
    }

    public DbSet<Plan> Plans { get; set; }
}
