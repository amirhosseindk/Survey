using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Survey.Users.Contracts;
using Survey.Users.Models;
using System.Security.Claims;

namespace Survey.Users.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IdentityResult> AddClaimAsync(User user, Claim claim)
        {
            try
            {
                _logger.LogInformation("Adding claim {ClaimType} to user {UserId}", claim.Type, user.Id);
                return await _userManager.AddClaimAsync(user, claim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding claim {ClaimType} to user {UserId}: {ErrorMessage}", claim.Type, user.Id, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims)
        {
            try
            {
                _logger.LogInformation("Adding {ClaimCount} claims to user {UserId}", claims.Count(), user.Id);
                return await _userManager.AddClaimsAsync(user, claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding claims to user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            try
            {
                _logger.LogInformation("Adding user {UserId} to role {Role}", user.Id, role);
                return await _userManager.AddToRoleAsync(user, role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding user {UserId} to role {Role}: {ErrorMessage}", user.Id, role, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        {
            try
            {
                _logger.LogInformation("Adding user {UserId} to roles {Roles}", user.Id, string.Join(",", roles));
                return await _userManager.AddToRolesAsync(user, roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding user {UserId} to roles: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token)
        {
            try
            {
                _logger.LogInformation("Changing email for user {UserId} to {NewEmail}", user.Id, newEmail);
                return await _userManager.ChangeEmailAsync(user, newEmail, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while changing email for user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            try
            {
                _logger.LogInformation("Changing password for user {UserId}", user.Id);
                return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while changing password for user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            try
            {
                _logger.LogInformation("Creating a new user with username {Username}", user.UserName);
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User {UserId} created successfully. Adding default claims...", user.Id);
                    await AddDefaultClaimsAsync(user);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating user {Username}: {ErrorMessage}", user.UserName, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            try
            {
                _logger.LogInformation("Deleting user {UserId}", user.Id);
                return await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            try
            {
                _logger.LogInformation("Finding user by email {Email}", email);
                return await _userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while finding user by email {Email}: {ErrorMessage}", email, ex.Message);
                throw;
            }
        }

        public async Task<User?> FindByNameAsync(string userName)
        {
            try
            {
                _logger.LogInformation("Finding user by username {Username}", userName);
                return await _userManager.FindByNameAsync(userName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while finding user by username {Username}: {ErrorMessage}", userName, ex.Message);
                throw;
            }
        }

        public async Task<User?> FindByIdAsync(string userId)
        {
            try
            {
                _logger.LogInformation("Finding user by ID {UserId}", userId);
                return await _userManager.FindByIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while finding user by ID {UserId}: {ErrorMessage}", userId, ex.Message);
                throw;
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user)
        {
            try
            {
                _logger.LogInformation("Getting claims for user {UserId}", user.Id);
                return await _userManager.GetClaimsAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting claims for user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<string?> GetEmailAsync(User user)
        {
            try
            {
                _logger.LogInformation("Getting email for user {UserId}", user.Id);
                return await _userManager.GetEmailAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting email for user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            try
            {
                _logger.LogInformation("Getting roles for user {UserId}", user.Id);
                return await _userManager.GetRolesAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting roles for user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<User?> GetUserAsync(ClaimsPrincipal principal)
        {
            try
            {
                _logger.LogInformation("Getting user by principal");
                return await _userManager.GetUserAsync(principal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting user by principal: {ErrorMessage}", ex.Message);
                throw;
            }
        }

        public async Task<string?> GetUserIdAsync(User user)
        {
            try
            {
                _logger.LogInformation("Getting user id");
                return await _userManager.GetUserIdAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting user ID: {ErrorMessage}", ex.Message);
                throw;
            }
        }

        public async Task<string?> GetUserNameAsync(User user)
        {
            try
            {
                _logger.LogInformation("Getting user name for {UserId}", user.Id);
                return await _userManager.GetUserNameAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting user name for {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> RemoveClaimAsync(User user, Claim claim)
        {
            try
            {
                _logger.LogInformation("Removing claim {ClaimType} from user {UserId}", claim.Type, user.Id);
                return await _userManager.RemoveClaimAsync(user, claim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing claim {ClaimType} from user {UserId}: {ErrorMessage}", claim.Type, user.Id, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> RemoveClaimsAsync(User user, IEnumerable<Claim> claims)
        {
            try
            {
                _logger.LogInformation("Removing {ClaimCount} claims from user {UserId}", claims.Count(), user.Id);
                return await _userManager.RemoveClaimsAsync(user, claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing claims from user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(User user, string role)
        {
            try
            {
                _logger.LogInformation("Removing user {UserId} from role {Role}", user.Id, role);
                return await _userManager.RemoveFromRoleAsync(user, role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing user {UserId} from role {Role}: {ErrorMessage}", user.Id, role, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
        {
            try
            {
                _logger.LogInformation("Removing user {UserId} from roles {Roles}", user.Id, string.Join(",", roles));
                return await _userManager.RemoveFromRolesAsync(user, roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing user {UserId} from roles: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            try
            {
                _logger.LogInformation("Resetting password for user {UserId}", user.Id);
                return await _userManager.ResetPasswordAsync(user, token, newPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while resetting password for user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            try
            {
                _logger.LogInformation("Updating user {UserId}", user.Id);
                return await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<bool> SignInAsync(User user)
        {
            try
            {
                _logger.LogInformation("Logging in user {UserId}", user.Id);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while logging in user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw;
            }
        }

        public async Task<bool> SignInAsync(string userName, string password)
        {
            var user = await FindByNameAsync(userName);
            if (user == null)
            {
                _logger.LogWarning("Login failed. User {Username} not found", userName);
                return false;
            }

            _logger.LogInformation("Attempting login for user {Username}", userName);
            var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                _logger.LogWarning("Invalid login attempt for user {Username}", userName);
                return false;
            }

            _logger.LogInformation("Login successful for user {Username}. Adding missing claims...", userName);
            await EnsureClaimsForUserAsync(user);
            return true;
        }

        public async Task<bool> LogOutAsync()
        {
            try
            {
                _logger.LogInformation("Logging out current user");
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while logging out: {ErrorMessage}", ex.Message);
                throw;
            }
        }

        private async Task EnsureClaimsForUserAsync(User user)
        {
            var existingClaims = await GetClaimsAsync(user);
            var claimsToAdd = new List<Claim>();

            if (!existingClaims.Any(c => c.Type == ClaimTypes.NameIdentifier))
                claimsToAdd.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            if (!existingClaims.Any(c => c.Type == ClaimTypes.Name))
                claimsToAdd.Add(new Claim(ClaimTypes.Name, user.UserName));

            if (!existingClaims.Any(c => c.Type == ClaimTypes.Email))
                claimsToAdd.Add(new Claim(ClaimTypes.Email, user.Email));

            if (!existingClaims.Any(c => c.Type == "StudentNumber"))
                claimsToAdd.Add(new Claim("StudentNumber", user.StudentNumber));

            if (!existingClaims.Any(c => c.Type == "IsProfessor"))
                claimsToAdd.Add(new Claim("IsProfessor", user.IsProfessor.ToString()));

            if (claimsToAdd.Any())
            {
                _logger.LogInformation("Adding missing claims to user {UserId}", user.Id);
                await AddClaimsAsync(user, claimsToAdd);
            }
        }

        private async Task AddDefaultClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("StudentNumber", user.StudentNumber),
                new Claim("IsProfessor", user.IsProfessor.ToString())
            };

            await AddClaimsAsync(user, claims);
            _logger.LogInformation("Default claims added for user {UserId}", user.Id);
        }
    }
}
