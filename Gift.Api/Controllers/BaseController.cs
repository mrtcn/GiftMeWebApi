using System.Web.Http;
using Gift.Api.Models;
using Gift.Api.Results;

namespace Gift.Api.Controllers
{
    public class BaseController : ApiController
    {
        protected IHttpActionResult ErrorResponse(ErrorModel model) {
            return new ErrorHttpActionResult(this, model);
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
