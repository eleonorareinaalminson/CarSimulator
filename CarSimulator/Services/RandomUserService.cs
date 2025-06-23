using CarSimulator.Interfaces;
using CarSimulator.Models;
using System.Net.Http;
using System.Text.Json;

namespace CarSimulator.Services
{
    public class RandomUserService : IRandomUserService
    {
        private readonly HttpClient _httpClient;

        public RandomUserService()
        {
            _httpClient = new HttpClient();
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
                Console.WriteLine($"Fel vid API-anrop: {ex.Message}");
                return new Driver("Test Förare", "test@example.com");
            }
        }
    }
}
