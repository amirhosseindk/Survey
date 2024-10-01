using Survey.Users.Models;
using System.Security.Claims;

namespace Suvery.WebAPI.Extensions
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

        public static User ToUserModel(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return new User
            {
                Id = user.GetUserId(),
                Email = user.GetUserEmail(),
                UserName = user.GetUserName(),
                IsProfessor = user.IsProfessor(),
                StudentNumber = user.GetUserStudentNumber()
            };
        }
    }
}