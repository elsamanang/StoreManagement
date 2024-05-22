using Microsoft.EntityFrameworkCore;
using StoreManagement.App.Models;
using System.Configuration;

namespace StoreManagement.App.Contexts;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext()
    {

    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            var conn = ConfigurationManager.ConnectionStrings[1].ConnectionString;
            ArgumentException.ThrowIfNullOrWhiteSpace(conn, "ConnectionString Not Found");
            optionsBuilder.UseSqlServer(conn);
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasIndex(x => x.Label).IsUnique();

        modelBuilder.Entity<ProductCategory>().HasIndex(x => x.Name).IsUnique();

        modelBuilder.Entity<Storage>()
            .Property(x => x.Created)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Storage>()
            .Property(x => x.IsActive)
            .HasDefaultValueSql("1")
            .ValueGeneratedOnAdd();

    }


    public virtual DbSet<Product> Products { get; init; }
    public virtual DbSet<ProductCategory> ProductCategories { get; init; }
    public virtual DbSet<Storage> Storages { get; init; }
}
