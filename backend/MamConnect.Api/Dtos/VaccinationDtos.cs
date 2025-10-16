using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MamConnect.Domain.Entities;

namespace MamConnect.Api.Dtos;

public record ChildVaccinationScheduleDto(
    int ChildId,
    string FirstName,
    DateOnly BirthDate,
    IReadOnlyList<ChildVaccineEntryDto> Vaccines);

public record ChildVaccineEntryDto(
    int VaccineId,
    string VaccineName,
    int AgeInMonths,
    VaccineStatus Status,
    DateOnly? ScheduledDate,
    DateOnly? AdministrationDate,
    string? Comments);

public record VaccinationOverviewDto(
    int TotalChildren,
    int TotalVaccinations,
    int CompletedVaccinations,
    int ScheduledVaccinations,
    int OverdueVaccinations,
    int ChildrenWithOverdueVaccinations);

public class UpdateChildVaccineRequest
{
    [StringLength(512)]
    public string? Comments { get; set; }

    public DateOnly? ScheduledDate { get; set; }

    public DateOnly? AdministrationDate { get; set; }

    public VaccineStatus Status { get; set; }
}