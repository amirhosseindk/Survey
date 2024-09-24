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
                return await _userManager.AddClaimAsync(user, claim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding claim - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims)
        {
            try
            {
                return await _userManager.AddClaimsAsync(user, claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding claims - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            try
            {
                return await _userManager.AddToRoleAsync(user, role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding user to role - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        {
            try
            {
                return await _userManager.AddToRolesAsync(user, roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding user to roles - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token)
        {
            try
            {
                return await _userManager.ChangeEmailAsync(user, newEmail, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while changing email - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            try
            {
                return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while changing password - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    return result;
                }
                else
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
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating user - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            try
            {
                return await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting user - {ex.Message}");
                throw;
            }
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while finding user by email - {ex.Message}");
                throw;
            }
        }

        public async Task<User?> FindByNameAsync(string userName)
        {
            try
            {
                return await _userManager.FindByNameAsync(userName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while finding user by name - {ex.Message}");
                throw;
            }
        }

        public async Task<User?> FindByIdAsync(string userId)
        {
            try
            {
                return await _userManager.FindByIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while finding user by ID - {ex.Message}");
                throw;
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user)
        {
            try
            {
                return await _userManager.GetClaimsAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting claims - {ex.Message}");
                throw;
            }
        }

        public async Task<string?> GetEmailAsync(User user)
        {
            try
            {
                return await _userManager.GetEmailAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting email - {ex.Message}");
                throw;
            }
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            try
            {
                return await _userManager.GetRolesAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting roles - {ex.Message}");
                throw;
            }
        }

        public async Task<User?> GetUserAsync(ClaimsPrincipal principal)
        {
            try
            {
                return await _userManager.GetUserAsync(principal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting user - {ex.Message}");
                throw;
            }
        }

        public async Task<string?> GetUserIdAsync(User user)
        {
            try
            {
                return await _userManager.GetUserIdAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting user ID - {ex.Message}");
                throw;
            }
        }

        public async Task<string?> GetUserNameAsync(User user)
        {
            try
            {
                return await _userManager.GetUserNameAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting user name - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> RemoveClaimAsync(User user, Claim claim)
        {
            try
            {
                return await _userManager.RemoveClaimAsync(user, claim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while removing claim - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> RemoveClaimsAsync(User user, IEnumerable<Claim> claims)
        {
            try
            {
                return await _userManager.RemoveClaimsAsync(user, claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while removing claims - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(User user, string role)
        {
            try
            {
                return await _userManager.RemoveFromRoleAsync(user, role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while removing from role - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
        {
            try
            {
                return await _userManager.RemoveFromRolesAsync(user, roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while removing from roles - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            try
            {
                return await _userManager.ResetPasswordAsync(user, token, newPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while resetting password - {ex.Message}");
                throw;
            }
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            try
            {
                return await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating user - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> LoginAsync(User user)
        {
            try
            {
                await _signInManager.SignInAsync(user, false);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while signing in - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            try
            {
                var user = await FindByNameAsync(userName);
                if (user == null)
                {
                    _logger.LogWarning("User not found.");
                    return false;
                }

                var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
                if (result.Succeeded)
                {
                    var existingClaims = await GetClaimsAsync(user);
                    var claimsToAdd = new List<Claim>();

                    if (!existingClaims.Any(c => c.Type == ClaimTypes.NameIdentifier))
                    {
                        claimsToAdd.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    }

                    if (!existingClaims.Any(c => c.Type == ClaimTypes.Name))
                    {
                        claimsToAdd.Add(new Claim(ClaimTypes.Name, user.UserName));
                    }

                    if (!existingClaims.Any(c => c.Type == ClaimTypes.Email))
                    {
                        claimsToAdd.Add(new Claim(ClaimTypes.Email, user.Email));
                    }

                    if (!existingClaims.Any(c => c.Type == "StudentNumber"))
                    {
                        claimsToAdd.Add(new Claim("StudentNumber", user.StudentNumber));
                    }

                    if (!existingClaims.Any(c => c.Type == "IsProfessor"))
                    {
                        claimsToAdd.Add(new Claim("IsProfessor", user.IsProfessor.ToString()));
                    }

                    if (claimsToAdd.Any())
                    {
                        await _userManager.AddClaimsAsync(user, claimsToAdd);
                    }

                    _logger.LogInformation("User signed in successfully.");
                    return true;
                }

                _logger.LogWarning("Invalid login attempt.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while signing in.");
                throw;
            }
        }

        public async Task<bool> LogOutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while signing out - {ex.Message}");
                throw;
            }
        }
    }
}