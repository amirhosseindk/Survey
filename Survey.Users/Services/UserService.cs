using Microsoft.AspNetCore.Identity;
using Survey.Users.Contracts;
using Survey.Users.Models;
using System.Security.Claims;

namespace Survey.Users.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> AddClaimAsync(User user, Claim claim)
        {
            return _userManager.AddClaimAsync(user, claim);
        }

        public Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims)
        {
            return _userManager.AddClaimsAsync(user, claims);
        }

        public Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return _userManager.AddToRoleAsync(user, role);
        }

        public Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        {
            return _userManager.AddToRolesAsync(user, roles);
        }

        public Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token)
        {
            return _userManager.ChangeEmailAsync(user, newEmail, token);
        }

        public Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<IdentityResult> DeleteAsync(User user)
        {
            return _userManager.DeleteAsync(user);
        }

        public Task<User?> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<User?> FindByNameAsync(string userName)
        {
            return _userManager.FindByNameAsync(userName);
        }

        public Task<User?> FindByIdAsync(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<IList<Claim>> GetClaimsAsync(User user)
        {
            return _userManager.GetClaimsAsync(user);
        }

        public Task<string?> GetEmailAsync(User user)
        {
            return _userManager.GetEmailAsync(user);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public Task<User?> GetUserAsync(ClaimsPrincipal principal)
        {
            return _userManager.GetUserAsync(principal);
        }

        public Task<string?> GetUserIdAsync(User user)
        {
            return _userManager.GetUserIdAsync(user);
        }

        public Task<string?> GetUserNameAsync(User user)
        {
            return _userManager.GetUserNameAsync(user);
        }

        public Task<IdentityResult> RemoveClaimAsync(User user, Claim claim)
        {
            return _userManager.RemoveClaimAsync(user, claim);
        }

        public Task<IdentityResult> RemoveClaimsAsync(User user, IEnumerable<Claim> claims)
        {
            return _userManager.RemoveClaimsAsync(user, claims);
        }

        public Task<IdentityResult> RemoveFromRoleAsync(User user, string role)
        {
            return _userManager.RemoveFromRoleAsync(user, role);
        }

        public Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
        {
            return _userManager.RemoveFromRolesAsync(user, roles);
        }

        public Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public Task<IdentityResult> UpdateUserAsync(User user)
        {
            return _userManager.UpdateAsync(user);
        }
    }

}