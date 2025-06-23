using CarSimulator.Models;

namespace CarSimulator.Interfaces
{
    public interface IRandomUserService
    {
        Task<Driver> GetRandomDriverAsync();
    }
}