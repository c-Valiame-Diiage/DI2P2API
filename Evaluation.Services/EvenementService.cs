using Evaluation.DAL.Contracts;
using Evaluation.Entities;
using Evaluation.Services.Contracts;
using Evaluation.Services.Contracts.DTO.Up;

namespace Evaluation.Services
{
    public class EvenementService : IEvenementService
    {
        private IEvenementRepository evenementRepository;

        public EvenementService(IEvenementRepository _evenementRepository)
        {
            evenementRepository = _evenementRepository;
        }
        public async Task<EvenementUpDTO> SaveEvenement(EvenementUpDTO eventDTO)
        {
            Evenement evenement = new Evenement()
            {
                Titre = eventDTO.Titre,
                Description = eventDTO.Description,
                DateEvent = eventDTO.DateEvent,
                TimeEvent = eventDTO.TimeEvent,
                Lieu = eventDTO.Lieu
            };

            var eventCreated = await this.evenementRepository.CreateEvenement(evenement);

            return new EvenementUpDTO()
            {
                Titre = eventCreated.Titre,
                Description = eventCreated.Description,
                DateEvent = eventCreated.DateEvent,
                TimeEvent = eventCreated.TimeEvent,
                Lieu = eventCreated.Lieu
            };
        }
    }
}
