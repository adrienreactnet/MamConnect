using MamConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace MamConnect.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Child> Children => Set<Child>();
    public DbSet<DailyReport> DailyReports => Set<DailyReport>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);        

        modelBuilder.Entity<User>()
            .HasMany(u => u.AssignedChildren)
            .WithOne(c => c.Assistant)
            .HasForeignKey(c => c.AssistantId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Children)
            .WithMany(c => c.Parents)
            .UsingEntity(j => j.ToTable("ParentChildren"));

        // Seed data for testing
        const string passwordHash = "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea";
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Role = UserRole.Assistant, Email = "assistant1@example.com", PasswordHash = passwordHash },
            new User { Id = 2, Role = UserRole.Assistant, Email = "assistant2@example.com", PasswordHash = passwordHash },
            new User { Id = 3, Role = UserRole.Parent, Email = "parent1@example.com", PasswordHash = passwordHash },
            new User { Id = 4, Role = UserRole.Parent, Email = "parent2@example.com", PasswordHash = passwordHash }
        );        

        modelBuilder.Entity<Child>().HasData(
            new Child
            {
                Id = 1,
                FirstName = "Emeline",
                BirthDate = new DateOnly(2020, 1, 1),
                AssistantId = 1
            },
            new Child
            {
                Id = 2,
                FirstName = "Alice",
                BirthDate = new DateOnly(2020, 1, 1),
                AssistantId = 2
            }
        );

        modelBuilder.Entity<DailyReport>().HasData(
            new DailyReport
            {
                Id = 1,
                Content = "First report Emeline",
                CreatedAt = new DateTime(2024, 1, 1),
                ChildId = 1
            },
            new DailyReport
            {
                Id = 2,
                Content = "First report Alice",
                CreatedAt = new DateTime(2024, 1, 1),
                ChildId = 2
            }
        );

        modelBuilder.Entity("ChildUser").HasData(
            new { ChildrenId = 1, ParentsId = 3 },
            new { ChildrenId = 2, ParentsId = 4 }
        );
    }
}
