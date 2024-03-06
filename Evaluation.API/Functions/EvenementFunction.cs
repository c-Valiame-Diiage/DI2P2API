using System.Net;
using System.Text.Json;
using System.Text;
using Evaluation.Services.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Evaluation.Services.Contracts.DTO.Up;

namespace Evaluation.API.Functions
{
    public class EvenementFunction
    {
        private readonly ILogger logger;
        private readonly IEvenementService evenementService;

        public EvenementFunction(ILoggerFactory _loggerFactory, IEvenementService _evenementService)
        {
            logger = _loggerFactory.CreateLogger<EvenementFunction>();
            evenementService = _evenementService;
        }

        /// <summary>
        /// Creates a event.
        /// </summary>
        /// <param name="req">Incomming request.</param>
        /// <returns>Returns a <see cref="Task"/> of <seealso cref="HttpResponseData"/> type.</returns>
        [Function("CreateEvenement")]
        public async Task<HttpResponseData> CreateEvenement([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Evenements")] HttpRequestData req)
        {
            var response = req.CreateResponse();
            string errorMessage = "Error saving event:";

            string requestBody;
            using (var reader = new StreamReader(req.Body, Encoding.UTF8))
            {
                requestBody = await reader.ReadToEndAsync();
            }
            try
            {
                var evenement = JsonSerializer.Deserialize<EvenementUpDTO>(requestBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var savedevenement = await this.evenementService.SaveEvenement(evenement!);
                await response.WriteAsJsonAsync(savedevenement);
                response.StatusCode = HttpStatusCode.Created;
            }

            catch (JsonException ex)
            {
                this.logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.Body = new MemoryStream(Encoding.UTF8.GetBytes($"Bad input in argument {errorMessage} {ex.Message}"));
            }

            catch (Exception ex)
            {
                this.logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.Body = new MemoryStream(Encoding.UTF8.GetBytes($"{errorMessage} {ex.Message}"));
            }

            return response;
        }
    }
}
