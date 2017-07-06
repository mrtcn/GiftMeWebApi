using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Gift.Api.Models;

namespace Gift.Api.Results
{
    public class ChallengeResult : IHttpActionResult
    {
        public string LoginProvider { get; set; }
        public HttpRequestMessage Request { get; set; }
        public UnauthorizedModel UnauthorizedModel { get; set; }

        public ChallengeResult(string loginProvider, ApiController controller, UnauthorizedModel unauthorizedModel = null)
        {
            LoginProvider = loginProvider;
            Request = controller.Request;
            UnauthorizedModel = unauthorizedModel;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            Request.GetOwinContext().Authentication.Challenge(LoginProvider);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            response.Content = new ObjectContent<UnauthorizedModel>(UnauthorizedModel, new JsonMediaTypeFormatter());
            return Task.FromResult(response);
        }
    }
}