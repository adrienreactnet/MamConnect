using System;

namespace MamConnect.Domain.Entities;

public class ChildVaccine
{
    public int Id { get; set; }
    public int ChildId { get; set; }
    public Child Child { get; set; } = null!;
    public int VaccineId { get; set; }
    public Vaccine Vaccine { get; set; } = null!;
    public VaccineStatus Status { get; set; }
    public DateOnly? ScheduledDate { get; set; }
    public DateOnly? AdministrationDate { get; set; }
    public string? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public enum VaccineStatus
{
    Completed = 0,
    Scheduled = 1,
    Overdue = 2
}