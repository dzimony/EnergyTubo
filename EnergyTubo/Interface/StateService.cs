using EnergyTubo.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EnergyTubo.Interface
{
    public class StateService:IStateService
    {
        private readonly AppDBContext _dbContext;

        public StateService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<State> AddState(State State)
        {
            var result = await _dbContext.States.AddAsync(State);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }


        public async Task<State> GetStateById(int stateId)
        {
            return await _dbContext.States

                
                .FirstOrDefaultAsync(e => e.Id == stateId);
        }
        public async Task<IEnumerable<State>> GetStates()
        {

            return await _dbContext.States.ToListAsync();

        }
    }

}

