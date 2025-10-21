using System.Threading.Tasks;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Domain.Entities;
using MamConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MamConnect.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber)
    {
        bool exists = await _dbContext.Users.AnyAsync(user => user.PhoneNumber == phoneNumber);
        return exists;
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        User? user = await _dbContext.Users.SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        return user;
    }

    public async Task<User?> GetWithChildrenAsync(int userId)
    {
        User? user = await _dbContext.Users
            .Include(u => u.Children)
            .Include(u => u.AssignedChildren)
            .SingleOrDefaultAsync(u => u.Id == userId);
        return user;
    }

    public Task RemoveAsync(User user)
    {
        _dbContext.Users.Remove(user);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
