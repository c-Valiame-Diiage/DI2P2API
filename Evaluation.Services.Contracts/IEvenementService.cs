using Evaluation.Services.Contracts.DTO.Up;

namespace Evaluation.Services.Contracts
{
    public interface IEvenementService
    {
        /// <summary>
        /// Instantiates a new Event registration.
        /// </summary>
        /// <param name="eventDTO">Event we will save.</param>
        /// <returns>Returns the new created Evenement.</returns>
        Task<EvenementUpDTO> SaveEvenement(EvenementUpDTO eventDTO);
    }
}
