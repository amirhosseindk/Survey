using Microsoft.AspNetCore.Identity;
using Survey.Users.Models;
using System.Security.Claims;

namespace Survey.Users.Contracts
{
    public interface IUserService
    {
        Task<IdentityResult> AddClaimAsync(User user, Claim claim);
        Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles);
        Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token);
        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> DeleteAsync(User user);
        Task<User?> FindByEmailAsync(string email);
        Task<User?> FindByNameAsync(string userName);
        Task<User?> FindByIdAsync(string userId);
        Task<IList<Claim>> GetClaimsAsync(User user);
        Task<string?> GetEmailAsync(User user);
        Task<IList<string>> GetRolesAsync(User user);
        Task<User?> GetUserAsync(ClaimsPrincipal principal);
        Task<string?> GetUserIdAsync(User user);
        Task<string?> GetUserNameAsync(User user);
        Task<IdentityResult> RemoveClaimAsync(User user, Claim claim);
        Task<IdentityResult> RemoveClaimsAsync(User user, IEnumerable<Claim> claims);
        Task<IdentityResult> RemoveFromRoleAsync(User user, string role);
        Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<bool> SignInAsync(User user);
        Task<bool> SignInAsync(string userName, string password);
        Task<bool> SignOutAsync();
    }
}