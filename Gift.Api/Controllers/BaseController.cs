using System.Net;
using System.Web.Http;
using Gift.Api.Models;
using Gift.Api.Results;

namespace Gift.Api.Controllers
{
    public class BaseController : ApiController
    {
        protected IHttpActionResult ErrorResponse(ErrorModel model, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest) {
            return new ErrorHttpActionResult(this, model, httpStatusCode);
        }

        protected IHttpActionResult SuccessResponse(SuccessModel model)
        {
            return new SuccessHttpActionResult(this, model);
        }

        protected IHttpActionResult UnauthorizedResponse(string loginProvider, UnauthorizedModel model)
        {
            return new ChallengeResult(loginProvider, this, model);
        }
    }
}
