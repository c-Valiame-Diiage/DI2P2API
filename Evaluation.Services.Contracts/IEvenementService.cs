using Evaluation.Entities;
using Evaluation.Services.Contracts.DTO.Up;

namespace Evaluation.Services.Contracts
{
    public interface IEvenementService
    {
        /// <summary>
        /// Instantiates a new Event registration.
        /// </summary>
        /// <param name="eventDTO">Event we will save.</param>
        /// <returns>Returns the new created event.</returns>
        Task<EvenementUpDTO> SaveEvenement(EvenementUpDTO eventDTO);

        /// <summary>
        /// Gets all events.
        /// </summary>
        /// <returns>Returns a list of events.</returns>
        Task<List<Evenement>> GetAllEvenements();
    }
}
