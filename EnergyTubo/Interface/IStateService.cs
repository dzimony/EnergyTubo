using EnergyTubo.Models;

namespace EnergyTubo.Interface
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetStates();
        Task<State> AddState(State State);
        Task<State> GetStateById(int stateId);
    }
}


