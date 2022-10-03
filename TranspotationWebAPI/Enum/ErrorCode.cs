namespace TranspotationAPI.Enum
{
    //For error code to show in the console
    public class ErrorCode
    {
        // Error for not found Account in database or error with code
        public const string ACCOUNT_NOT_FOUND = "THIS_ACCOUNT_NOT_FOUND";
        public const string USER_NOT_FOUND = "THIS_USER_NOT_FOUND";
        public const string PASSWORD_INCORRECT = "PASSWORD_INCORRECT";
        public const string ACCOUNT_BLOCKED = "ACCOUNT_BLOCKED";
        public const string EMAIL_EXIST = "THIS_EMAIL_HAS_BEEN_REGISTERED";
        public const string REGISTRATION_FAILED = "REGISTRATION_FAILED";
        public const string GET_ACCOUNT_INFO_FAIL = "GET_ACCOUNT_INFO_FAIL";
        public const string GET_ROLE_ACCOUNT_FAIL = "GET_ROLE_ACCOUNT_FAIL";
        public const string THIS_ACCOUNT_IS_NOT_AUTH = "THIS_ACCOUNT_IS_NOT_AUTH";
        public const string REPOSITORY_ERROR = "REPOSITORY_ERROR";
        public const string CREATE_COMPANY_TRIP_FAIL = "CREATE_COMPANY_TRIP_FAIL";
    }
}
