using Survey.Application.Dtos.Users.Base;
using System.Security.Claims;

namespace WebApp.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claim = user.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claim = user.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claim = user.FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }

        public static bool IsProfessor(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.IsInRole("Professor");
        }

        public static string GetUserStudentNumber(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claim = user.FindFirst("StudentNumber");
            return claim?.Value;
        }

        public static UserDto ToUserModel(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return new UserDto
            {
                UserId = user.GetUserId(),
                Email = user.GetUserEmail(),
                UserName = user.GetUserName(),
                IsProfessor = user.IsProfessor(),
                StudentNumber = user.GetUserStudentNumber()
            };
        }

        public static bool IsLoggedIn(this ClaimsPrincipal user)
        {
            return user?.Identity != null && user.Identity.IsAuthenticated;
        }
    }
}