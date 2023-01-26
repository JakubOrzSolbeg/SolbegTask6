using DataRepository.Entities;
using DTOs.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataRepository.DbContext;

public class MainDbContext1 : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly IConfiguration _configuration;

    public MainDbContext1(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transfer> Transfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        

        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                Login = "admin",
                Passhash = "84875ba07b2435a7910cf5098ee92d9caa7d56f161df52336955e91c98f86172",
                Salt = "0e4cbe8a48eb4414a36fbadf88fe1c90",
                UserType = UserType.Admin
            }
            // password zaq12wsx
        );

        modelBuilder.Entity<Category>().HasData(
            new Category() { Id = 1, CreatorId = 1, IsCustom = false, Name = "Food" },
            new Category() { Id = 2, CreatorId = 1, IsCustom = false, Name = "Transport" },
            new Category() { Id = 3, CreatorId = 1, IsCustom = false, Name = "Mobile" },
            new Category() { Id = 4, CreatorId = 1, IsCustom = false, Name = "Internet" },
            new Category() { Id = 5, CreatorId = 1, IsCustom = false, Name = "Housing" },
            new Category() { Id = 6, CreatorId = 1, IsCustom = false, Name = "Entertainment" },
            new Category() { Id = 7, CreatorId = 1, IsCustom = false, IsIncome = true, Name = "Salary" },
            new Category() { Id = 8, CreatorId = 1, IsCustom = false, IsIncome = true, Name = "Scholarship" },
            new Category() { Id = 9, CreatorId = 1, IsCustom = false, IsIncome = true, Name = "Payment" }
           
        );

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MainDb"),
            b => b.MigrationsAssembly(_configuration["MigrationAssembly"]));
    }
}