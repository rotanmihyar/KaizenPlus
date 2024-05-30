namespace kaizenplus.Models
{
    public enum ErrorCode
    {
        Success = 0,
        InvalidModel = 1,
        UserNotFound = 2,
        InvalidPassword = 3,
        RefreshTokenExpired = 4,
        UsernameTaken = 5,
        EmptyFile = 6,
        InvalidVerificationCode = 7,
        UserInactive = 8,
        UserNotVerified = 9,

        PaymentPending = 41,
        NeedManualCheck = 42,
        RejectionDue3DsecureRiskCheck = 43,
        RejectionByExternalCheck = 44,
        RejectionDueCommunication = 45,
        RejectionDueSystemError = 46,
        RejectionDueWorkflow = 47,
        SoftDecline = 48,
        RejectionDueRisk = 49,
        RejectionDueAddress = 50,
        RejectionDue3Dsecure = 51,
        Blacklist = 52,
        ServerError = 100,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Expired=111,
        ExceedMaxChatsPerMonth = 112,
        noSubscrptionFound=113,
    }
}