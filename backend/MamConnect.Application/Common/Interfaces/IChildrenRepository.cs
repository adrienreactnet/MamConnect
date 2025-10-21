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
    Task<Child?> FindByIdAsync(int id);
    Task<Child?> GetChildWithParentsAsync(int id);
    Task AddAsync(Child child);
    Task RemoveAsync(Child child);
    Task SaveChangesAsync();
}
