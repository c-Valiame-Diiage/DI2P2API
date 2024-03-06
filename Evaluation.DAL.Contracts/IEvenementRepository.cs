using Evaluation.Entities;

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

    }
}
