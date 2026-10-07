namespace Talaqah.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId, CancellationToken cancellationToken = default);
    Task<bool> IsInRoleAsync(string userId, string role, CancellationToken cancellationToken = default);
    Task<bool> AuthorizeAsync(string userId, string policyName, CancellationToken cancellationToken = default);
    Task<(bool Result, string UserId)> CreateUserAsync(string userName, string password, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserAsync(string userId, CancellationToken cancellationToken = default);
}
