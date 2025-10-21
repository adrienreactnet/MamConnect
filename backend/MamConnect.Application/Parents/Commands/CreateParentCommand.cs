using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Parents.Commands;

public class CreateParentCommand
{
    private readonly IUserRepository _userRepository;
    private readonly IChildrenRepository _childrenRepository;

    public CreateParentCommand(IUserRepository userRepository, IChildrenRepository childrenRepository)
    {
        _userRepository = userRepository;
        _childrenRepository = childrenRepository;
    }

    /// <summary>
    /// Creates a new parent and assigns children if provided.
    /// </summary>
    /// <param name="email">The parent email.</param>
    /// <param name="firstName">The parent first name.</param>
    /// <param name="lastName">The parent last name.</param>
    /// <param name="phoneNumber">The parent phone number.</param>
    /// <param name="childrenIds">The identifiers of children to link.</param>
    /// <returns>The created parent.</returns>
    public async Task<User> ExecuteAsync(
        string email,
        string firstName,
        string lastName,
        string phoneNumber,
        IReadOnlyCollection<int> childrenIds)
    {
        User parent = new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Role = UserRole.Parent,
            PasswordHash = string.Empty
        };

        if (childrenIds.Count > 0)
        {
            IReadOnlyCollection<Child> children = await _childrenRepository.GetChildrenByIdsAsync(childrenIds);
            foreach (Child child in children)
            {
                parent.Children.Add(child);
            }
        }

        await _userRepository.AddAsync(parent);
        await _userRepository.SaveChangesAsync();
        return parent;
    }
}
