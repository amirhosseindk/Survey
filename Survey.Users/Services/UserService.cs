using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Survey.Common.Exception;
using Survey.Common.Maps;
using Survey.Common.Types;
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
                var result = await _userManager.AddClaimAsync(user, claim);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to add claim {ClaimType} to user {UserId}. Errors: {Errors}", claim.Type, user.Id, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding claim {ClaimType} to user {UserId}: {ErrorMessage}", claim.Type, user.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims)
        {
            try
            {
                _logger.LogInformation("Adding {ClaimCount} claims to user {UserId}", claims.Count(), user.Id);
                var result = await _userManager.AddClaimsAsync(user, claims);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to add claims to user {UserId}. Errors: {Errors}", user.Id, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding claims to user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            try
            {
                _logger.LogInformation("Adding user {UserId} to role {Role}", user.Id, role);
                var result = await _userManager.AddToRoleAsync(user, role);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to add user {UserId} to role {Role}. Errors: {Errors}", user.Id, role, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding user {UserId} to role {Role}: {ErrorMessage}", user.Id, role, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        {
            try
            {
                _logger.LogInformation("Adding user {UserId} to roles {Roles}", user.Id, string.Join(",", roles));
                var result = await _userManager.AddToRolesAsync(user, roles);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to add user {UserId} to roles {Roles}. Errors: {Errors}", user.Id, string.Join(",", roles), errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding user {UserId} to roles: {ErrorMessage}", user.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token)
        {
            try
            {
                _logger.LogInformation("Changing email for user {UserId} to {NewEmail}", user.Id, newEmail);
                var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to change email for user {UserId}. Errors: {Errors}", user.Id, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while changing email for user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            try
            {
                _logger.LogInformation("Changing password for user {UserId}", user.Id);
                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to change password for user {UserId}. Errors: {Errors}", user.Id, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while changing password for user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
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
                    _logger.LogInformation("User {UserId} default claims Added successfully. Adding default roles...", user.Id);
                    await AddDefaultRolesAsync(user);
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("User creation failed for {Username}. Errors: {Errors}", user.UserName, errors);

                    if (result.Errors.Any(e => e.Code == "DuplicateUserName"))
                    {
                        throw new BusinessException(ErrorMap.GetMessage(ErrorType.ExistingEmailAddress), (int)ErrorType.ExistingEmailAddress);
                    }
                    else
                    {
                        throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserCreationFailed), (int)ErrorType.UserCreationFailed);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating user {Username}: {ErrorMessage}", user.UserName, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            try
            {
                _logger.LogInformation("Deleting user {UserId}", user.Id);
                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("User deletion failed for {UserId}. Errors: {Errors}", user.Id, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserDeleteFailed), (int)ErrorType.UserDeleteFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            try
            {
                _logger.LogInformation("Finding user by email {Email}", email);
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    _logger.LogWarning("User with email {Email} not found", email);
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserNotFound), (int)ErrorType.UserNotFound);
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while finding user by email {Email}: {ErrorMessage}", email, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<User?> FindByNameAsync(string userName)
        {
            try
            {
                _logger.LogInformation("Finding user by username {Username}", userName);
                var user = await _userManager.FindByNameAsync(userName);

                if (user == null)
                {
                    _logger.LogWarning("User {Username} not found", userName);
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserNotFound), (int)ErrorType.UserNotFound);
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while finding user by username {Username}: {ErrorMessage}", userName, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<User?> FindByIdAsync(string userId)
        {
            try
            {
                _logger.LogInformation("Finding user by ID {UserId}", userId);
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserNotFound), (int)ErrorType.UserNotFound);
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while finding user by ID {UserId}: {ErrorMessage}", userId, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogWarning("User cannot be null in GetClaimsAsync");
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.NullArgument), (int)ErrorType.NullArgument);
                }

                _logger.LogInformation("Getting claims for user {UserId}", user.Id);
                return await _userManager.GetClaimsAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting claims for user {UserId}: {ErrorMessage}", user?.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<string?> GetEmailAsync(User user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogWarning("User cannot be null in GetEmailAsync");
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.NullArgument), (int)ErrorType.NullArgument);
                }

                _logger.LogInformation("Getting email for user {UserId}", user.Id);
                return await _userManager.GetEmailAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting email for user {UserId}: {ErrorMessage}", user?.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogWarning("User cannot be null in GetRolesAsync");
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.NullArgument), (int)ErrorType.NullArgument);
                }

                _logger.LogInformation("Getting roles for user {UserId}", user.Id);
                return await _userManager.GetRolesAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting roles for user {UserId}: {ErrorMessage}", user?.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<User?> GetUserAsync(ClaimsPrincipal principal)
        {
            try
            {
                if (principal == null)
                {
                    _logger.LogWarning("Principal cannot be null in GetUserAsync");
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.NullArgument), (int)ErrorType.NullArgument);
                }

                _logger.LogInformation("Getting user by principal");
                var user = await _userManager.GetUserAsync(principal);

                if (user == null)
                {
                    _logger.LogWarning("User not found for the given principal");
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserNotFound), (int)ErrorType.UserNotFound);
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting user by principal: {ErrorMessage}", ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<string?> GetUserIdAsync(User user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogWarning("User cannot be null in GetUserIdAsync");
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.NullArgument), (int)ErrorType.NullArgument);
                }

                _logger.LogInformation("Getting user id for user {UserId}", user.Id);
                return await _userManager.GetUserIdAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting user ID: {ErrorMessage}", ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<string?> GetUserNameAsync(User user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogWarning("User cannot be null in GetUserNameAsync");
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.NullArgument), (int)ErrorType.NullArgument);
                }

                _logger.LogInformation("Getting user name for {UserId}", user.Id);
                return await _userManager.GetUserNameAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting user name for {UserId}: {ErrorMessage}", user?.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> RemoveClaimAsync(User user, Claim claim)
        {
            try
            {
                _logger.LogInformation("Removing claim {ClaimType} from user {UserId}", claim.Type, user.Id);
                var result = await _userManager.RemoveClaimAsync(user, claim);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to remove claim {ClaimType} from user {UserId}. Errors: {Errors}", claim.Type, user.Id, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing claim {ClaimType} from user {UserId}: {ErrorMessage}", claim.Type, user.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> RemoveClaimsAsync(User user, IEnumerable<Claim> claims)
        {
            try
            {
                _logger.LogInformation("Removing {ClaimCount} claims from user {UserId}", claims.Count(), user.Id);
                var result = await _userManager.RemoveClaimsAsync(user, claims);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to remove claims from user {UserId}. Errors: {Errors}", user.Id, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing claims from user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(User user, string role)
        {
            try
            {
                _logger.LogInformation("Removing user {UserId} from role {Role}", user.Id, role);
                var result = await _userManager.RemoveFromRoleAsync(user, role);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to remove user {UserId} from role {Role}. Errors: {Errors}", user.Id, role, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing user {UserId} from role {Role}: {ErrorMessage}", user.Id, role, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
        {
            try
            {
                _logger.LogInformation("Removing user {UserId} from roles {Roles}", user.Id, string.Join(",", roles));
                var result = await _userManager.RemoveFromRolesAsync(user, roles);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to remove user {UserId} from roles. Errors: {Errors}", user.Id, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing user {UserId} from roles: {ErrorMessage}", user.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            try
            {
                _logger.LogInformation("Resetting password for user {UserId}", user.Id);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to reset password for user {UserId}. Errors: {Errors}", user.Id, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while resetting password for user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            try
            {
                _logger.LogInformation("Updating user {UserId}", user.Id);
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("User update failed for {UserId}. Errors: {Errors}", user.Id, errors);

                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.UserUpdateFailed), (int)ErrorType.UserUpdateFailed);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user {UserId}: {ErrorMessage}", user.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<bool> SignInAsync(User user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogWarning("User cannot be null in SignInAsync");
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.NullArgument), (int)ErrorType.NullArgument);
                }

                _logger.LogInformation("Logging in user {UserId}", user.Id);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("Login successful for user {Username}. checking missing claims...", user.UserName);
                await EnsureClaimsForUserAsync(user);
                _logger.LogInformation("Login successful for user {Username}. checking missing roles...", user.UserName);
                await EnsureRolesForUserAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while logging in user {UserId}: {ErrorMessage}", user?.Id, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<bool> SignInAsync(string userName, string password)
        {
            try
            {
                var user = await FindByNameAsync(userName);
                if (user == null)
                {
                    _logger.LogWarning("Login failed. User {Username} not found", userName);
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.InvalidUsernameOrPassword), (int)ErrorType.InvalidUsernameOrPassword);
                }

                _logger.LogInformation("Attempting login for user {Username}", userName);
                var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: true, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Invalid login attempt for user {Username}", userName);
                    throw new BusinessException(ErrorMap.GetMessage(ErrorType.InvalidUsernameOrPassword), (int)ErrorType.InvalidUsernameOrPassword);
                }

                _logger.LogInformation("Login successful for user {Username}. checking missing claims...", userName);
                await EnsureClaimsForUserAsync(user);
                _logger.LogInformation("Login successful for user {Username}. checking missing roles...", userName);
                await EnsureRolesForUserAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while signing in user {Username}: {ErrorMessage}", userName, ex.Message);
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
            }
        }

        public async Task<bool> SignOutAsync()
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
                throw new BusinessException(ErrorMap.GetMessage(ErrorType.InternalServerError), (int)ErrorType.InternalServerError);
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

        private async Task EnsureRolesForUserAsync(User user)
        {
            var existingRoles = await GetRolesAsync(user);
            var rolesToAdd = new List<string>();

            if (user.IsProfessor)
            {
                if (!existingRoles.Any(c => c == "Professor"))
                    rolesToAdd.Add("Professor");
            }
            else
            {
                if (!existingRoles.Any(c => c == "Student"))
                    rolesToAdd.Add("Student");
            }

            if (rolesToAdd.Any())
            {
                _logger.LogInformation("Adding missing roles to user {UserId}", user.Id);
                await AddToRolesAsync(user, rolesToAdd);
            }
        }

        private async Task AddDefaultRolesAsync(User user)
        {
            string role;

            if (user.IsProfessor)
                role = "Professor";
            else
                role = "Student";

            await AddToRoleAsync(user, role);

            _logger.LogInformation("Default roles added for user {UserId}", user.Id);
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