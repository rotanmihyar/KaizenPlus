using System;
using kaizenplus.Localizations;

namespace kaizenplus.Models
{
    public class AppException : Exception
    {
        public ErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public AppException(ErrorCode errorCode, string errorMessage = "")
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                var stringLocalizer = AppHttpContext.GetService<IJsonStringLocalizer>();
                errorMessage = stringLocalizer.GetString(errorCode.ToString(), "GENERAL");
            }

            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}