using Evaluation.DAL.Contracts;
using Evaluation.Entities;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Evenement> GetAllEvenements()
        {
            try
            {
                return this.dbContext.Evenement.AsQueryable();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Evenement> GetEvenementById(int idEvenement)
        {
            try
            {
                return await this.dbContext.Evenement.SingleAsync(p => p.Id == idEvenement);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Evenement> UpdateEvenement(Evenement evenement)
        {
            try
            {
                this.dbContext.Evenement.Update(evenement);
                await this.dbContext.SaveChangesAsync();
                return evenement;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
