using CarSimulator.Interfaces;
using CarSimulator.Models;
using System.Net.Http;
using System.Text.Json;

namespace CarSimulator.Services
{
    public class RandomUserService : IRandomUserService
    {
        private readonly HttpClient _httpClient;
        private static readonly Random _fallbackRandom = new Random();

        // Fallback-förare om API:et ej funkar
        private static readonly List<(string Name, string Email)> _fallbackDrivers = new List<(string, string)>
        {
            ("Anna Andersson", "anna.andersson@email.com"),
            ("Erik Eriksson", "erik.eriksson@email.com"),
            ("Maria Johansson", "maria.johansson@email.com"),
            ("Lars Larsson", "lars.larsson@email.com"),
            ("Karin Nilsson", "karin.nilsson@email.com")
        };

        public RandomUserService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
        }

        public async Task<Driver> GetRandomDriverAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://randomuser.me/api/");
                var jsonDoc = JsonDocument.Parse(response);
                var result = jsonDoc.RootElement.GetProperty("results")[0];

                var firstName = result.GetProperty("name").GetProperty("first").GetString();
                var lastName = result.GetProperty("name").GetProperty("last").GetString();
                var name = $"{firstName} {lastName}";
                var email = result.GetProperty("email").GetString();

                return new Driver(name, email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid API-anrop: {ex.Message}. Använder fallback-förare.");

                // Returnera random fallback-förare istället för alltid en och samma!
                var randomIndex = _fallbackRandom.Next(_fallbackDrivers.Count);
                var fallbackDriver = _fallbackDrivers[randomIndex];

                return new Driver(fallbackDriver.Name, fallbackDriver.Email);
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}