using System;

namespace Gift.Api.Models
{
    public class ErrorModel
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        //Response Content If Needed
        public object ErrorContent { get; set; }

        public ErrorModel(object errorContent)
        {
            ErrorContent = errorContent;
            ErrorMessage = string.Empty;
            ErrorCode = 0;
        }

        public ErrorModel(object errorContent, string errorMessage)
        {
            ErrorContent = errorContent;
            ErrorMessage = errorMessage;
            ErrorCode = 0;
        }

        public ErrorModel(object errorContent, string errorMessage, int errorCode)
        {
            ErrorContent = errorContent;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }
    }
}