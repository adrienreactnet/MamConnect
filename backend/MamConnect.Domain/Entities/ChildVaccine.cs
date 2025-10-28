using System;
using System.Text.Json.Serialization;

namespace MamConnect.Domain.Entities;

public class ChildVaccine
{
    public int Id { get; set; }
    public int ChildId { get; set; }

    [JsonIgnore]
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
    Pending = 1,
    ToSchedule = 2,
    Overdue = 3
}
