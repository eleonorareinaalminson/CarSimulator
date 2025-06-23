using CarSimulator.Services;

namespace CarSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var gameService = new GameService();
            await gameService.StartGameAsync();
        }
    }
}