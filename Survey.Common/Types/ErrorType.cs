namespace Survey.Common.Types
{
    public enum ErrorType
    {
        // General errors
        NullArgument = 1000,
        InternalServerError = 1001,

        // User errors
        UserNotFound = 2000,
        UserCreationFailed = 2001,
        UserUpdateFailed = 2002,
        UserDeleteFailed = 2003,
        InvalidUsernameOrPassword = 2004,
        InvalidPhoneNumber = 2005,
        ExistingPhoneNumber = 2006,
        ExistingEmailAddress = 2007
    }
}