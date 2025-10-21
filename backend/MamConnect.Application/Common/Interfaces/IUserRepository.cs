using System.Threading.Tasks;
using MamConnect.Domain.Entities;

namespace MamConnect.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber);
    Task AddAsync(User user);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task<User?> GetWithChildrenAsync(int userId);
    Task RemoveAsync(User user);
    Task SaveChangesAsync();
}
