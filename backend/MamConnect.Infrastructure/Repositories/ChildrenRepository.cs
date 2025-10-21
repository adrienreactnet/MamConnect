using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;
using MamConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MamConnect.Infrastructure.Repositories;

public class ChildrenRepository : IChildrenRepository
{
    private readonly AppDbContext _dbContext;

    public ChildrenRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Child>> GetChildrenByAssistantIdAsync(int assistantId)
    {
        List<Child> children = await _dbContext.Children
            .Where(child => child.AssistantId == assistantId)
            .OrderBy(child => child.FirstName)
            .ToListAsync();
        return children;
    }

    public async Task<IReadOnlyCollection<Child>> GetChildrenByParentIdAsync(int parentId)
    {
        List<Child> children = await _dbContext.Children
            .Where(child => child.Parents.Any(parent => parent.Id == parentId))
            .OrderBy(child => child.FirstName)
            .ToListAsync();
        return children;
    }

    public async Task<IReadOnlyCollection<Child>> GetAllChildrenAsync()
    {
        List<Child> children = await _dbContext.Children
            .OrderBy(child => child.FirstName)
            .ToListAsync();
        return children;
    }

    public async Task<IReadOnlyCollection<Child>> GetChildrenWithRelationsAsync()
    {
        List<Child> children = await _dbContext.Children
            .Include(child => child.Assistant)
            .Include(child => child.Parents)
            .OrderBy(child => child.FirstName)
            .ToListAsync();
        return children;
    }

    public async Task<Child?> FindByIdAsync(int id)
    {
        Child? child = await _dbContext.Children.FindAsync(id);
        return child;
    }

    public async Task<Child?> GetChildWithParentsAsync(int id)
    {
        Child? child = await _dbContext.Children
            .Include(c => c.Parents)
            .ThenInclude(parent => parent.Children)
            .FirstOrDefaultAsync(c => c.Id == id);
        return child;
    }

    public async Task AddAsync(Child child)
    {
        await _dbContext.Children.AddAsync(child);
    }

    public Task RemoveAsync(Child child)
    {
        _dbContext.Children.Remove(child);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
