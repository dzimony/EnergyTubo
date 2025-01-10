using EnergyTubo.Models;

namespace EnergyTubo.Interface
{
    public interface IBankService
    {
        Task<Bank> GetBanks(string Key);
    }
}

