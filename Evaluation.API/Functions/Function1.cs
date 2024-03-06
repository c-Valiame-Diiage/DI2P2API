using System.Net;
using Evaluation.Services.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Evaluation.API.Functions
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IService _service;

        public Function1(ILoggerFactory loggerFactory, IService service)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _service = service;
        }

        [Function("Function")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
