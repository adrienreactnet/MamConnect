using MamConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace MamConnect.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Child> Children => Set<Child>();
    public DbSet<ChildVaccine> ChildVaccines => Set<ChildVaccine>();
    public DbSet<DailyReport> DailyReports => Set<DailyReport>();
    public DbSet<Vaccine> Vaccines => Set<Vaccine>();
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

        modelBuilder.Entity<ChildVaccine>()
            .HasOne(cv => cv.Child)
            .WithMany(c => c.ChildVaccines)
            .HasForeignKey(cv => cv.ChildId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChildVaccine>()
            .HasOne(cv => cv.Vaccine)
            .WithMany(v => v.ChildVaccines)
            .HasForeignKey(cv => cv.VaccineId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChildVaccine>()
            .HasIndex(cv => new { cv.ChildId, cv.VaccineId, cv.ScheduledDate })
            .IsUnique();

        modelBuilder.Entity<ChildVaccine>()
            .Property(cv => cv.Comments)
            .HasMaxLength(512);

        // Seed data for testing
        const string passwordHash = "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea";
        const string adminPasswordHash = "AQAAAAEAACcQAAAAEBpXW/zlBa5UwW0tpsWsFmvZSK4bsEq+aPNbw+GQYlS2C/Zx6ujWmAoBrtWEDegbPA==";
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Role = UserRole.Admin,
                Email = "admin@example.com",
                PasswordHash = adminPasswordHash,
                FirstName = "Admin",
                LastName = "User",
                PhoneNumber = "0123456789"
            },
            new User { Id = 2, Role = UserRole.Assistant, Email = "assistant1@example.com", PasswordHash = passwordHash, FirstName = "Alice", LastName = "Martin", PhoneNumber = "0100000001" },
            new User { Id = 3, Role = UserRole.Assistant, Email = "assistant2@example.com", PasswordHash = passwordHash, FirstName = "Bruno", LastName = "Petit", PhoneNumber = "0100000002" },
            new User { Id = 4, Role = UserRole.Assistant, Email = "assistant3@example.com", PasswordHash = passwordHash, FirstName = "Claire", LastName = "Leroy", PhoneNumber = "0100000003" },
            new User { Id = 5, Role = UserRole.Assistant, Email = "assistant4@example.com", PasswordHash = passwordHash, FirstName = "David", LastName = "Morel", PhoneNumber = "0100000004" },
            new User { Id = 6, Role = UserRole.Assistant, Email = "assistant5@example.com", PasswordHash = passwordHash, FirstName = "Emma", LastName = "Roussel", PhoneNumber = "0100000005" },
            new User { Id = 7, Role = UserRole.Parent, Email = "parent1@example.com", PasswordHash = passwordHash, FirstName = "Paul", LastName = "Durand", PhoneNumber = "0200000001" },
            new User { Id = 8, Role = UserRole.Parent, Email = "parent2@example.com", PasswordHash = passwordHash, FirstName = "Claire", LastName = "Durand", PhoneNumber = "0200000002" },
            new User { Id = 9, Role = UserRole.Parent, Email = "parent3@example.com", PasswordHash = passwordHash, FirstName = "Sophie", LastName = "Martin", PhoneNumber = "0200000003" },
            new User { Id = 10, Role = UserRole.Parent, Email = "parent4@example.com", PasswordHash = passwordHash, FirstName = "Julien", LastName = "Lambert", PhoneNumber = "0200000004" },
            new User { Id = 11, Role = UserRole.Parent, Email = "parent5@example.com", PasswordHash = passwordHash, FirstName = "Camille", LastName = "Lambert", PhoneNumber = "0200000005" },
            new User { Id = 12, Role = UserRole.Parent, Email = "parent6@example.com", PasswordHash = passwordHash, FirstName = "Nadia", LastName = "Bernard", PhoneNumber = "0200000006" },
            new User { Id = 13, Role = UserRole.Parent, Email = "parent7@example.com", PasswordHash = passwordHash, FirstName = "Hugo", LastName = "Lefevre", PhoneNumber = "0200000007" },
            new User { Id = 14, Role = UserRole.Parent, Email = "parent8@example.com", PasswordHash = passwordHash, FirstName = "Elise", LastName = "Lefevre", PhoneNumber = "0200000008" },
            new User { Id = 15, Role = UserRole.Parent, Email = "parent9@example.com", PasswordHash = passwordHash, FirstName = "Thomas", LastName = "Petit", PhoneNumber = "0200000009" },
            new User { Id = 16, Role = UserRole.Parent, Email = "parent10@example.com", PasswordHash = passwordHash, FirstName = "Laura", LastName = "Petit", PhoneNumber = "0200000010" },
            new User { Id = 17, Role = UserRole.Parent, Email = "parent11@example.com", PasswordHash = passwordHash, FirstName = "Antoine", LastName = "Moreau", PhoneNumber = "0200000011" },
            new User { Id = 18, Role = UserRole.Parent, Email = "parent12@example.com", PasswordHash = passwordHash, FirstName = "Marie", LastName = "Moreau", PhoneNumber = "0200000012" },
            new User { Id = 19, Role = UserRole.Parent, Email = "parent13@example.com", PasswordHash = passwordHash, FirstName = "Isabelle", LastName = "Garnier", PhoneNumber = "0200000013" },
            new User { Id = 20, Role = UserRole.Parent, Email = "parent14@example.com", PasswordHash = passwordHash, FirstName = "Victor", LastName = "Robert", PhoneNumber = "0200000014" },
            new User { Id = 21, Role = UserRole.Parent, Email = "parent15@example.com", PasswordHash = passwordHash, FirstName = "Amelie", LastName = "Robert", PhoneNumber = "0200000015" },
            new User { Id = 22, Role = UserRole.Parent, Email = "parent16@example.com", PasswordHash = passwordHash, FirstName = "Patrick", LastName = "Caron", PhoneNumber = "0200000016" },
            new User { Id = 23, Role = UserRole.Parent, Email = "parent17@example.com", PasswordHash = passwordHash, FirstName = "Helene", LastName = "Marchand", PhoneNumber = "0200000017" },
            new User { Id = 24, Role = UserRole.Parent, Email = "parent18@example.com", PasswordHash = passwordHash, FirstName = "Xavier", LastName = "Marchand", PhoneNumber = "0200000018" },
            new User { Id = 25, Role = UserRole.Parent, Email = "parent19@example.com", PasswordHash = passwordHash, FirstName = "Chantal", LastName = "Dupont", PhoneNumber = "0200000019" },
            new User { Id = 26, Role = UserRole.Parent, Email = "parent20@example.com", PasswordHash = passwordHash, FirstName = "Olivier", LastName = "Renard", PhoneNumber = "0200000020" },
            new User { Id = 27, Role = UserRole.Parent, Email = "parent21@example.com", PasswordHash = passwordHash, FirstName = "Lucie", LastName = "Renard", PhoneNumber = "0200000021" }


        );

        Child[] childSeedData = new Child[]
        {
            new Child { Id = 1, FirstName = "Liam", BirthDate = new DateOnly(2019, 5, 21), AssistantId = 2 },
            new Child { Id = 2, FirstName = "Emma", BirthDate = new DateOnly(2020, 3, 14), AssistantId = 2 },
            new Child { Id = 3, FirstName = "Noah", BirthDate = new DateOnly(2020, 11, 2), AssistantId = 2 },
            new Child { Id = 4, FirstName = "Olivia", BirthDate = new DateOnly(2018, 7, 9), AssistantId = 3 },
            new Child { Id = 5, FirstName = "Ava", BirthDate = new DateOnly(2019, 1, 30), AssistantId = 3 },
            new Child { Id = 6, FirstName = "Ethan", BirthDate = new DateOnly(2021, 4, 18), AssistantId = 3 },
            new Child { Id = 7, FirstName = "Mia", BirthDate = new DateOnly(2020, 9, 5), AssistantId = 3 },
            new Child { Id = 8, FirstName = "Lucas", BirthDate = new DateOnly(2018, 12, 12), AssistantId = 4 },
            new Child { Id = 9, FirstName = "Sophia", BirthDate = new DateOnly(2019, 6, 22), AssistantId = 4 },
            new Child { Id = 10, FirstName = "Mason", BirthDate = new DateOnly(2020, 2, 11), AssistantId = 4 },
            new Child { Id = 11, FirstName = "Isabella", BirthDate = new DateOnly(2018, 10, 3), AssistantId = 4 },
            new Child { Id = 12, FirstName = "Harper", BirthDate = new DateOnly(2021, 1, 27), AssistantId = 4 },
            new Child { Id = 13, FirstName = "James", BirthDate = new DateOnly(2019, 8, 15), AssistantId = 5 },
            new Child { Id = 14, FirstName = "Amelia", BirthDate = new DateOnly(2020, 5, 16), AssistantId = 5 },
            new Child { Id = 15, FirstName = "Benjamin", BirthDate = new DateOnly(2018, 4, 7), AssistantId = 5 },
            new Child { Id = 16, FirstName = "Charlotte", BirthDate = new DateOnly(2019, 12, 1), AssistantId = 6 },
            new Child { Id = 17, FirstName = "Elijah", BirthDate = new DateOnly(2020, 7, 19), AssistantId = 6 },
            new Child { Id = 18, FirstName = "Evelyn", BirthDate = new DateOnly(2021, 9, 23), AssistantId = 6 },
            new Child { Id = 19, FirstName = "Henry", BirthDate = new DateOnly(2018, 3, 13), AssistantId = 6 }
        };

        modelBuilder.Entity<Child>().HasData(childSeedData);

        modelBuilder.Entity<DailyReport>().HasData(
            new DailyReport
            {
                Id = 1,
                Content = "First report Liam",
                CreatedAt = new DateTime(2024, 1, 1),
                ChildId = 1
            },
            new DailyReport
            {
                Id = 2,
                Content = "First report Emma",
                CreatedAt = new DateTime(2024, 1, 1),
                ChildId = 2
            }
        );

        modelBuilder.Entity<Vaccine>().HasData(
            new Vaccine
            {
                Id = 1,
                Name = "DTP et Coqueluche",
                AgesInMonths = "2,4,11"
            },
            new Vaccine
            {
                Id = 2,
                Name = "Hib",
                AgesInMonths = "2,4,11"
            },
            new Vaccine
            {
                Id = 3,
                Name = "Hépatite B",
                AgesInMonths = "2,4,11"
            },
            new Vaccine
            {
                Id = 4,
                Name = "Pneumocoque",
                AgesInMonths = "2,4,11"
            },
            new Vaccine
            {
                Id = 5,
                Name = "ROR",
                AgesInMonths = "12,16"
            },
            new Vaccine
            {
                Id = 6,
                Name = "Méningocoques ACWY",
                AgesInMonths = "6,12"
            },
            new Vaccine
            {
                Id = 7,
                Name = "Méningocoque B",
                AgesInMonths = "3,5,12"
            }
        );

        List<ChildVaccine> childVaccineSeedData = new List<ChildVaccine>();
        int childVaccineId = 1;
        DateTime defaultCreatedAt = new DateTime(2024, 1, 1);
        foreach (Child child in childSeedData)
        {
            DateOnly firstDoseDate = child.BirthDate.AddMonths(2);
            childVaccineSeedData.Add(new ChildVaccine
            {
                Id = childVaccineId,
                ChildId = child.Id,
                VaccineId = 1,
                Status = VaccineStatus.Completed,
                ScheduledDate = firstDoseDate,
                AdministrationDate = firstDoseDate,
                Comments = "Dose initiale administrée conformément au calendrier.",
                CreatedAt = defaultCreatedAt,
                UpdatedAt = defaultCreatedAt
            });
            childVaccineId++;

            DateOnly secondDoseDate = child.BirthDate.AddMonths(4);
            bool isSecondDoseScheduled = child.Id % 2 == 0;
            childVaccineSeedData.Add(new ChildVaccine
            {
                Id = childVaccineId,
                ChildId = child.Id,
                VaccineId = 2,
                Status = isSecondDoseScheduled ? VaccineStatus.Scheduled : VaccineStatus.Completed,
                ScheduledDate = secondDoseDate,
                AdministrationDate = isSecondDoseScheduled ? null : secondDoseDate,
                Comments = isSecondDoseScheduled ? "Dose programmée avec la famille." : "Dose réalisée lors de la visite mensuelle.",
                CreatedAt = defaultCreatedAt,
                UpdatedAt = isSecondDoseScheduled ? null : defaultCreatedAt
            });
            childVaccineId++;

            DateOnly boosterDoseDate = child.BirthDate.AddMonths(12);
            bool isBoosterOverdue = child.Id % 3 == 0;
            childVaccineSeedData.Add(new ChildVaccine
            {
                Id = childVaccineId,
                ChildId = child.Id,
                VaccineId = 5,
                Status = isBoosterOverdue ? VaccineStatus.Overdue : VaccineStatus.Scheduled,
                ScheduledDate = boosterDoseDate,
                AdministrationDate = null,
                Comments = isBoosterOverdue ? "Relance nécessaire auprès des parents." : "Rappel prévu lors de la prochaine visite médicale.",
                CreatedAt = defaultCreatedAt,
                UpdatedAt = isBoosterOverdue ? new DateTime(2024, 3, 15) : null
            });
            childVaccineId++;
        }

        modelBuilder.Entity<ChildVaccine>().HasData(childVaccineSeedData);


        modelBuilder.Entity("ChildUser").HasData(
            new { ChildrenId = 1, ParentsId = 7 },
            new { ChildrenId = 1, ParentsId = 8 },
            new { ChildrenId = 2, ParentsId = 7 },
            new { ChildrenId = 2, ParentsId = 8 },
            new { ChildrenId = 3, ParentsId = 9 },
            new { ChildrenId = 4, ParentsId = 10 },
            new { ChildrenId = 4, ParentsId = 11 },
            new { ChildrenId = 5, ParentsId = 10 },
            new { ChildrenId = 5, ParentsId = 11 },
            new { ChildrenId = 6, ParentsId = 12 },
            new { ChildrenId = 7, ParentsId = 13 },
            new { ChildrenId = 7, ParentsId = 14 },
            new { ChildrenId = 8, ParentsId = 15 },
            new { ChildrenId = 8, ParentsId = 16 },
            new { ChildrenId = 9, ParentsId = 15 },
            new { ChildrenId = 9, ParentsId = 16 },
            new { ChildrenId = 10, ParentsId = 17 },
            new { ChildrenId = 10, ParentsId = 18 },
            new { ChildrenId = 11, ParentsId = 17 },
            new { ChildrenId = 11, ParentsId = 18 },
            new { ChildrenId = 12, ParentsId = 19 },
            new { ChildrenId = 13, ParentsId = 20 },
            new { ChildrenId = 13, ParentsId = 21 },
            new { ChildrenId = 14, ParentsId = 20 },
            new { ChildrenId = 14, ParentsId = 21 },
            new { ChildrenId = 15, ParentsId = 22 },
            new { ChildrenId = 16, ParentsId = 23 },
            new { ChildrenId = 16, ParentsId = 24 },
            new { ChildrenId = 17, ParentsId = 23 },
            new { ChildrenId = 17, ParentsId = 24 },
            new { ChildrenId = 18, ParentsId = 25 },
            new { ChildrenId = 19, ParentsId = 26 },
            new { ChildrenId = 19, ParentsId = 27 }
        );
    }
}