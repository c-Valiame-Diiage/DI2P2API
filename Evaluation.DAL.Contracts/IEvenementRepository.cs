using Evaluation.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Evaluation.DAL.Contracts
{
    public interface IEvenementRepository
    {
        /// <summary>
        /// Creation of an event.
        /// </summary>
        /// <param name="evenement">Event we will save.</param>
        /// <returns>The new Event created.</returns>
        Task<Evenement> CreateEvenement(Evenement evenement);

        /// <summary>
        /// Gets all events.
        /// </summary>
        /// <returns>Returns a list of events.</returns>
        IQueryable<Evenement> GetAllEvenements();

        /// <summary>
        /// Gets event by id.
        /// </summary>
        /// <param name="idEvenement">Id of the event.</param>
        /// <returns>Returns event based on its id.</returns>
        Task<Evenement> GetEvenementById(int idEvenement);

        /// <summary>
        /// Updates a event.
        /// </summary>
        /// <param name="evenement">Event we will modify and its new values.</param>
        /// <returns>The modified event.</returns>
        Task<Evenement> UpdateEvenement(Evenement evenement);

    }
}
