using EnergyTubo.Models;

namespace EnergyTubo.Interface
{
    public interface ILgaService
    {
        Task<IEnumerable<LGA>> GetLGAs(int stateId);
        Task<LGA> AddLGA(LGA LGA);
        Task<LGA> GetLgaById(int lgaId);
    }
}
