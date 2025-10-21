using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MamConnect.Api.Dtos;
using MamConnect.Domain.Entities;
using MamConnect.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MamConnect.Api.Controllers;

[Authorize(Roles = nameof(UserRole.Admin))]
[ApiController]
[Route("api/vaccinations")]
public class VaccinationsController : ControllerBase
{
    private readonly IVaccinationService _vaccinationService;

    public VaccinationsController(IVaccinationService vaccinationService)
    {
        this._vaccinationService = vaccinationService;
    }

    [HttpGet("children/{childId:int}")]
    public async Task<IActionResult> GetChildSchedule(int childId, CancellationToken cancellationToken)
    {
        Child? child = await _vaccinationService.GetChildScheduleAsync(childId, cancellationToken);

        if (child == null)
        {
            ProblemDetails problem = CreateProblemDetails(
                StatusCodes.Status404NotFound,
                "ChildNotFound",
                $"No child was found with the identifier {childId}.");
            return NotFound(problem);
        }

        ChildVaccinationScheduleDto response = MapToScheduleDto(child);
        return Ok(response);
    }

    [HttpPut("children/{childId:int}/vaccines/{vaccineId:int}")]
    public async Task<IActionResult> UpdateChildVaccine(
        int childId,
        int vaccineId,
        UpdateChildVaccineRequest request,
        CancellationToken cancellationToken)
    {
        ProblemDetails? validationProblem = ValidateUpdateRequest(request);
        if (validationProblem != null)
        {
            return BadRequest(validationProblem);
        }

        ChildVaccine? updated = await _vaccinationService.UpdateChildVaccineAsync(
            childId,
            vaccineId,
            request.ScheduledDate,
            request.AdministrationDate,
            request.Status,
            request.Comments,
            cancellationToken);

        if (updated == null)
        {
            ProblemDetails problem = CreateProblemDetails(
                StatusCodes.Status404NotFound,
                "VaccinationNotFound",
                "The vaccination entry for the provided child and vaccine could not be located.");
            return NotFound(problem);
        }

        Child? child = await _vaccinationService.GetChildScheduleAsync(childId, cancellationToken);
        if (child == null)
        {
            ProblemDetails problem = CreateProblemDetails(
                StatusCodes.Status404NotFound,
                "ChildNotFound",
                $"No child was found with the identifier {childId}.");
            return NotFound(problem);
        }

        ChildVaccinationScheduleDto response = MapToScheduleDto(child);
        return Ok(response);
    }

    [HttpGet("overview")]
    public async Task<IActionResult> GetOverview(CancellationToken cancellationToken)
    {
        VaccinationOverview overview = await _vaccinationService.GetOverviewAsync(cancellationToken);
        VaccinationOverviewDto response = new VaccinationOverviewDto(
            overview.TotalChildren,
            overview.TotalVaccinations,
            overview.CompletedVaccinations,
            overview.PendingVaccinations,
            overview.ToScheduleVaccinations,
            overview.OverdueVaccinations,
            overview.ChildrenWithOverdueVaccinations);
        return Ok(response);
    }

    private static ChildVaccinationScheduleDto MapToScheduleDto(Child child)
    {
        List<ChildVaccineEntryDto> vaccines = child.ChildVaccines
            .OrderBy(cv => cv.ScheduledDate ?? DateOnly.MinValue)
            .ThenBy(cv => cv.VaccineId)
            .Select(cv => new ChildVaccineEntryDto(
                cv.VaccineId,
                cv.Vaccine.Name,
                cv.Vaccine.AgeInMonths,
                cv.Status,
                cv.ScheduledDate,
                cv.AdministrationDate,
                cv.Comments))
            .ToList();

        return new ChildVaccinationScheduleDto(
            child.Id,
            child.FirstName,
            child.BirthDate,
            vaccines);
    }

    private static ProblemDetails? ValidateUpdateRequest(UpdateChildVaccineRequest request)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (request.Status.HasValue && request.Status.Value != VaccineStatus.Completed)
        {
            return CreateProblemDetails(
                StatusCodes.Status400BadRequest,
                "StatusChangeNotAllowed",
                "Only the administered status can be selected manually.");
        }

        if (request.AdministrationDate.HasValue && (!request.Status.HasValue || request.Status.Value != VaccineStatus.Completed))
        {
            return CreateProblemDetails(
                StatusCodes.Status400BadRequest,
                "StatusRequiredForAdministration",
                "An administration date requires selecting the administered status.");
        }

        if (request.Status == VaccineStatus.Completed)
        {
            if (!request.AdministrationDate.HasValue)
            {
                return CreateProblemDetails(
                    StatusCodes.Status400BadRequest,
                    "AdministrationDateRequired",
                    "An administration date is required when marking a vaccine as administered.");
            }

            if (request.AdministrationDate.Value > today)
            {
                return CreateProblemDetails(
                    StatusCodes.Status400BadRequest,
                    "AdministrationDateInFuture",
                    "The administration date cannot be set in the future.");
            }
        }

        return null;
    }

    private static ProblemDetails CreateProblemDetails(int statusCode, string title, string detail)
    {
        ProblemDetails problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail
        };
        return problem;
    }
}
