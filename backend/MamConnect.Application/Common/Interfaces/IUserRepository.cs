using System.Collections.Generic;
using System.Threading.Tasks;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber);
    Task AddAsync(User user);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task<User?> GetWithChildrenAsync(int userId);
    Task<IReadOnlyCollection<User>> GetUsersByRoleAsync(UserRole role);
    Task<IReadOnlyCollection<User>> GetUsersWithChildrenByRoleAsync(UserRole role);
    Task<User?> GetByIdAndRoleAsync(int id, UserRole role);
    Task<User?> GetByIdWithChildrenAsync(int id, UserRole role);
    Task RemoveAsync(User user);
    Task SaveChangesAsync();
}
