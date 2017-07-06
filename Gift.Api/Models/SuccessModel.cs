namespace Gift.Api.Models
{
    public class SuccessModel {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Content { get; set; }

        public SuccessModel(object content)
        {
            Content = content;
            Message = string.Empty;
            Code = 0;
        }

        public SuccessModel(object content, string message)
        {
            Content = content;
            Message = message;
            Code = 0;
        }

        public SuccessModel(object content, string message, int code)
        {
            Content = content;
            Message = message;
            Code = code;
        }
    }
}