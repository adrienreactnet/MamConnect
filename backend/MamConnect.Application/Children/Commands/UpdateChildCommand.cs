using System;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Children.Commands;

public class UpdateChildCommand
{
    private readonly IChildrenRepository _childrenRepository;

    public UpdateChildCommand(IChildrenRepository childrenRepository)
    {
        _childrenRepository = childrenRepository;
    }

    /// <summary>
    /// Updates an existing child with the provided information.
    /// </summary>
    /// <param name="id">The identifier of the child to update.</param>
    /// <param name="input">The new values for the child.</param>
    /// <returns><c>true</c> if the child exists and has been updated; otherwise <c>false</c>.</returns>
    public async Task<bool> ExecuteAsync(int id, Input input)
    {
        Child? child = await _childrenRepository.FindByIdAsync(id);
        if (child == null)
        {
            return false;
        }

        string trimmedFirstName = input.FirstName?.Trim() ?? string.Empty;
        string trimmedLastName = input.LastName?.Trim() ?? string.Empty;
        child.FirstName = trimmedFirstName;
        child.LastName = trimmedLastName;
        if (input.BirthDate.HasValue)
        {
            child.BirthDate = input.BirthDate.Value;
        }

        child.AssistantId = input.AssistantId;
        string? trimmedAllergies = input.Allergies?.Trim();
        child.Allergies = string.IsNullOrWhiteSpace(trimmedAllergies) ? null : trimmedAllergies;
        await _childrenRepository.SaveChangesAsync();
        return true;
    }

    public sealed class Input
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> class.
        /// </summary>
        /// <param name="firstName">The updated first name.</param>
        /// <param name="lastName">The updated last name.</param>
        /// <param name="birthDate">The updated birth date.</param>
        /// <param name="assistantId">The updated assistant identifier.</param>
        /// <param name="allergies">The updated allergy description.</param>
        public Input(string firstName, string lastName, DateOnly? birthDate, int? assistantId, string? allergies)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            AssistantId = assistantId;
            Allergies = allergies;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public DateOnly? BirthDate { get; }

        public int? AssistantId { get; }

        public string? Allergies { get; }
    }
}
