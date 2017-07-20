using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Gift.Api.Models;
using Newtonsoft.Json.Serialization;

namespace Gift.Api.Results
{
    public class SuccessHttpActionResult: IHttpActionResult {
        public SuccessModel SuccessModel { get; set; }
        public HttpRequestMessage RequestMessage { get; set; }        
        public SuccessHttpActionResult(ApiController controller, SuccessModel successModel)
        {
            SuccessModel = successModel;
            RequestMessage = controller.Request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)        
        {
            var jsonformatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonformatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ObjectContent<SuccessModel>(SuccessModel, jsonformatter);
            response.RequestMessage = RequestMessage;

            return Task.FromResult(response);
        }
    }
}