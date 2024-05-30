using Newtonsoft.Json;
using kaizenplus.Localizations;

namespace kaizenplus.Models
{
    public class BaseResponse
    {
        public ErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success
        {
            get
            {
                return ErrorCode == ErrorCode.Success;
            }
        }

        public BaseResponse(ErrorCode errorCode = ErrorCode.Success, string errorMessage = "")
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                var stringLocalizer = AppHttpContext.GetService<IJsonStringLocalizer>();
                errorMessage = stringLocalizer.GetString(errorCode.ToString(), "GENERAL");
            }

            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public BaseResponse(T data, ErrorCode errorCode = ErrorCode.Success, string errorMessage = "")
            : base(errorCode, errorMessage)
        {
            Data = data;
        }
    }
}