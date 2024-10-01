using Survey.Common.Types;

namespace Survey.Common.Maps
{
    public static class ErrorMap
    {
        private static readonly Dictionary<ErrorType, string> _errorMessages = new Dictionary<ErrorType, string>
    {
        // General
        { ErrorType.NullArgument, "Argument should not be null or empty" },
        { ErrorType.InternalServerError, "An internal server error occurred" },

        // User errors
        { ErrorType.UserNotFound, "User not found" },
        { ErrorType.UserCreationFailed, "User creation failed" },
        { ErrorType.UserUpdateFailed, "User update failed" },
        { ErrorType.UserDeleteFailed, "User deletion failed" },
        { ErrorType.InvalidUsernameOrPassword, "Invalid username or password" },
        { ErrorType.InvalidPhoneNumber, "Invalid phone number" },
        { ErrorType.ExistingPhoneNumber, "Phone number already exists" },
        { ErrorType.ExistingEmailAddress, "Email address already exists" }
    };

        public static string GetMessage(ErrorType errorType) => _errorMessages.ContainsKey(errorType) ? _errorMessages[errorType] : "Unknown error";
    }
}