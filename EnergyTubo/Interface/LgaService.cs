using EnergyTubo.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyTubo.Interface
{
    public class LgaService:ILgaService
    {
        private readonly AppDBContext _dbContext;

        public LgaService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LGA> AddLGA(LGA LGA)
        {
            var result = await _dbContext.LGAs.AddAsync(LGA);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<LGA>> GetLGAs(int stateId)
        {

            return await _dbContext.LGAs.Where(x=>x.StateId == stateId).ToListAsync();

        }

        public async Task<LGA> GetLgaById(int lgaId)
        {
            return await _dbContext.LGAs


                .FirstOrDefaultAsync(e => e.Id == lgaId);
        }

    }

}


