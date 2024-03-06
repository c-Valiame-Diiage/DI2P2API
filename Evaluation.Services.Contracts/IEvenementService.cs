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

        /// <summary>
        /// Gets event by its id.
        /// </summary>
        /// <param name="idEvenement">Unique identifier of the event.</param>
        /// <returns>Returns the event.</returns>
        Task<Evenement> GetEvenementById(int idEvenement);

        /// <summary>
        /// Update a event.
        /// </summary>
        /// <param name="evenementUpDTO">Event information to modify.</param>
        /// <returns>Returns the event updated.</returns>
        Task<Evenement> UpdateEvenement(Evenement evenement);

        /// <summary>
        /// Deletes a event.
        /// </summary>
        /// <param name="idEvenement">Identifier of the event we want to delete.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        Task DeleteEvenement(int idEvenement);
    }
}
