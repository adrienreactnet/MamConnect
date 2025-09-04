using MamConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace MamConnect.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Child> Children => Set<Child>();
    public DbSet<DailyReport> DailyReports => Set<DailyReport>();
}
