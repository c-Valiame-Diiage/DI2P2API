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

        public async Task<List<Evenement>> GetAllEvenements()
        {
            return await Task.Run(() =>
            {
                return evenementRepository.GetAllEvenements().ToList();
            });
        }

        public async Task<Evenement> GetEvenementById(int idEvenement)
        {
            return await this.evenementRepository.GetEvenementById(idEvenement);
        }

        public async Task<Evenement> UpdateEvenement(Evenement evenement)
        {
            var existingEvenement = await evenementRepository.GetEvenementById(evenement.Id);

            existingEvenement.Titre = evenement.Titre;
            existingEvenement.Description = evenement.Description;
            existingEvenement.DateEvent = evenement.DateEvent;
            existingEvenement.TimeEvent = evenement.TimeEvent!;
            existingEvenement.Lieu = evenement.Lieu;

            return await evenementRepository.UpdateEvenement(existingEvenement);
        }
    }
}
