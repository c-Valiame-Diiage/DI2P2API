using Evaluation.DAL.Contracts;
using Evaluation.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Evaluation.DAL.Repositories
{
    public class EvenementRepository : IEvenementRepository
    {
        private readonly DbContextEntity dbContext;

        public EvenementRepository(DbContextEntity dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Evenement> CreateEvenement(Evenement evenement)
        {
            try
            {
                var evenementSaved = this.dbContext.Evenement.Add(evenement);
                await this.dbContext.SaveChangesAsync();

                return evenementSaved.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
