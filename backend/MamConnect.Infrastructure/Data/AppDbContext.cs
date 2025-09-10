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
    }
}
