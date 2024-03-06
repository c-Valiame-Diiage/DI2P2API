using System.Net;
using System.Text.Json;
using System.Text;
using Evaluation.Services.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Evaluation.Services.Contracts.DTO.Up;
using Evaluation.Entities;

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


        /// <summary>
        /// Gets all events.
        /// </summary>
        /// <param name="req">Incoming request.</param>
        /// <returns>Returns the response of the function.</returns>
        [Function("GetAllEvenements")]
        public async Task<HttpResponseData> GetAllEvenements(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Evenements")] HttpRequestData req)
        {
            logger.LogInformation("C# HTTP trigger function processed a request to get all events.");
            string errorMessage = "Error getting all events:";

            var response = req.CreateResponse();

            try
            {
                var events = await this.evenementService.GetAllEvenements();
                await response.WriteAsJsonAsync(events);
            }

            catch (Exception ex)
            {
                logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.WriteString($"{errorMessage} {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Gets a evenement by its id.
        /// </summary>
        /// <param name="req">Incoming request.</param>
        /// <param name="id">Unique identifier of the evenement.</param>
        /// <returns>Returns the response of the function.</returns>
        [Function("GetEvenementById")]
        public async Task<HttpResponseData> GetEvenementById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Evenements/{id}")] HttpRequestData req, int id)
        {
            logger.LogInformation("C# HTTP trigger function processed a request for evenement with id: {id}.", id);
            string errorMessage = "Error retriving evenement by id:";

            var response = req.CreateResponse();
            try
            {
                var evenement = await this.evenementService.GetEvenementById(id);

                if (evenement == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    await response.WriteAsJsonAsync(evenement);
                }
            }

            catch (Exception ex)
            {
                this.logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.WriteString($"{errorMessage} {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Update a evenement.
        /// </summary>
        /// <param name="req">Incoming request.</param>
        /// <returns>Returns the evenement updated.</returns>
        [Function("UpdateEvenement")]
        public async Task<HttpResponseData> UpdateProfile(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Evenements/")] HttpRequestData req,
        FunctionContext executionContext)
        {
            string errorMessage = "Error updating evenement:";

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var evenement = JsonSerializer.Deserialize<Evenement>(requestBody);

            var response = req.CreateResponse();

            try
            {
                var updatedProfile = await this.evenementService.UpdateEvenement(evenement!);

                await response.WriteAsJsonAsync(updatedProfile);
            }

            catch (Exception ex)
            {
                logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.WriteString($"{errorMessage} {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Deletes a evenement.
        /// </summary>
        /// <param name="req">Incomming request.</param>
        /// <param name="id">Identifier of the evenement we want to delete.</param>
        /// <returns>Returns a <see cref="Task"/> of <seealso cref="HttpResponseData"/> type.</returns>
        [Function("DeleteEvenement")]
        public async Task<HttpResponseData> DeleteEvenement([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Evenements/{id}")] HttpRequestData req, int id)
        {
            this.logger.LogInformation("Starting of DeleteEvenement method");
            string errorMessage = "Error deleting evenement:";

            var response = req.CreateResponse(HttpStatusCode.NoContent);

            try
            {
                await this.evenementService.DeleteEvenement(id);
            }

            catch (Exception ex)
            {
                this.logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.WriteString($"{errorMessage} {ex.Message}");
            }

            return response;
        }
    }
}
