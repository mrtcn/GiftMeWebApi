﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Gift.Api.Models;
using Newtonsoft.Json.Serialization;

namespace Gift.Api.Results
{
    public class ErrorHttpActionResult: IHttpActionResult {
        public ErrorModel ErrorModel { get; set; }
        public HttpRequestMessage RequestMessage { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public ErrorHttpActionResult(ApiController controller, ErrorModel model, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            ErrorModel = model;
            RequestMessage = controller.Request;
            HttpStatusCode = httpStatusCode;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken) {
            var jsonformatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonformatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode);
            
            response.Content = new ObjectContent<ErrorModel>(ErrorModel, jsonformatter);
            response.RequestMessage = RequestMessage;
            return Task.FromResult(response);
        }
    }
}