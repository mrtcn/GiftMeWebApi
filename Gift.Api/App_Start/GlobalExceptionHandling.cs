using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Gift.Api.Models;
using Gift.Framework.Extensions;

namespace Gift.Api
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string message;
            if (actionExecutedContext.Request.Headers.AcceptLanguage != null
                &&
                actionExecutedContext.Request.Headers.AcceptLanguage.ToString().Contains(SupportedLanguageType.Turkish.GetEnumDescription<DisplayAttribute>().Name))
            {
                message = "Bilinmeyen bir hata oluştu";
            }
            else
            {
                message = "An error occured";
            }
            var response = new ErrorModel(null, message, 1);
            var host = actionExecutedContext.Request.RequestUri.DnsSafeHost;
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            actionExecutedContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));
        }
    }
}