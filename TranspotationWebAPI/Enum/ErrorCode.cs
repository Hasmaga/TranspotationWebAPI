namespace TranspotationAPI.Enum
{
    //For error code to show in the console
    public class ErrorCode
    {
        // Error for not found Account in database or error with code
        public const string ACCOUNT_NOT_FOUND = "THIS_ACCOUNT_NOT_FOUND";
        public const string PASSWORD_INCORRECT = "PASSWORD_INCORRECT";
        public const string ACCOUNT_BLOCKED = "ACCOUNT_BLOCKED";
        public const string EMAIL_EXIST = "THIS_EMAIL_HAS_BEEN_REGISTERED";
        public const string REGISTRATION_FAILED = "REGISTRATION_FAILED";
    }
}
