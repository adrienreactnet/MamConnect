using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Common.Interfaces;

public interface IChildrenRepository
{
    Task<IReadOnlyCollection<Child>> GetChildrenByAssistantIdAsync(int assistantId);
    Task<IReadOnlyCollection<Child>> GetChildrenByParentIdAsync(int parentId);
    Task<IReadOnlyCollection<Child>> GetAllChildrenAsync();
    Task<IReadOnlyCollection<Child>> GetChildrenWithRelationsAsync();
    Task<IReadOnlyCollection<Child>> GetChildrenByIdsAsync(IReadOnlyCollection<int> childIds);
    /// <summary>
    /// Determines whether a child already exists with the provided first name.
    /// </summary>
    /// <param name="firstName">The first name to check.</param>
    /// <returns><c>true</c> when a child already has the provided first name; otherwise <c>false</c>.</returns>
    Task<bool> ExistsWithFirstNameAsync(string firstName);
    Task<Child?> FindByIdAsync(int id);
    Task<Child?> GetChildWithParentsAsync(int id);
    Task AddAsync(Child child);
    Task RemoveAsync(Child child);
    Task SaveChangesAsync();
}
