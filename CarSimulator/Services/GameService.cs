using CarSimulator.Interfaces;
using CarSimulator.Models;
using CarSimulator.Enums;

namespace CarSimulator.Services
{
    public class GameService : IGameService
    {
        private readonly IRandomUserService _randomUserService;
        private IDriver _driver;
        private ICar _car;

        public GameService() : this(new RandomUserService())
        {
        }

        public GameService(IRandomUserService randomUserService)
        {
            _randomUserService = randomUserService;
            _car = new Car();
        }

        public async Task StartGameAsync()
        {
            Console.WriteLine("Välkommen till Bil-Simulatorn!");
            Console.WriteLine("Hämtar en slumpad bilförare från API...\n");

            _driver = await _randomUserService.GetRandomDriverAsync();

            Console.WriteLine($"Din bilförare: {_driver.Name}");
            Console.WriteLine($"Email: {_driver.Email}\n");

            bool continueGame = true;
            while (continueGame)
            {
                ShowMenu();
                ShowStatus();

                var warning = _driver.GetFatigueWarning();
                if (!string.IsNullOrEmpty(warning))
                {
                    _driver.ShowFatigueWarningWithColor();
                }

                Console.Write("\nVälj ett alternativ: ");
                var choice = Console.ReadLine();

                continueGame = HandleMenuChoice(choice);
            }

            Console.WriteLine("Tack för att du spelade Bil-Simulatorn!");
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n=== TILLGÄNGLIGA KOMMANDON ===");
            Console.WriteLine("1. Sväng vänster");
            Console.WriteLine("2. Sväng höger");
            Console.WriteLine("3. Kör framåt");
            Console.WriteLine("4. Backa");
            Console.WriteLine("5. Rasta");
            Console.WriteLine("6. Tanka bilen");
            Console.WriteLine("7. Avsluta");
            Console.WriteLine("===============================");
        }

        private void ShowStatus()
        {
            Console.WriteLine($"\n--- FÖRARENS OCH BILENS STATUS ---");
            Console.WriteLine($"Bilföraren {_driver.Name} kör åt {_car.GetDirectionInSwedish()}");
            Console.WriteLine($"Bilens riktning: {_car.GetDirectionInSwedish()}");

            // Färgkodad bensin baserat på nivå
            ShowFuelStatus();

            // Färgkodad trötthet baserat på nivå
            ShowFatigueStatus();
        }

        private void ShowFuelStatus()
        {
            var fuelPercentage = (_car.Fuel / _car.MaxFuel) * 100;

            Console.Write("Bensin: ");

            if (fuelPercentage > 50)
            {
                // Grönt när OK (över 50%)
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (fuelPercentage > 20)
            {
                // Gult när lågt (20-50%)
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                // Rött när kritiskt (under 20%)
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine($"{_car.Fuel:F0}/{_car.MaxFuel:F0} liter");
            Console.ResetColor();
        }

        private void ShowFatigueStatus()
        {
            Console.Write("Förarens trötthet: ");

            if (_driver.Fatigue <= 5)
            {
                // Grönt när OK (0-5)
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (_driver.Fatigue <= 8)
            {
                // Gult när trött (6-8)
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                // Rött när kritiskt (9-10)
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine($"{_driver.Fatigue}/10");
            Console.ResetColor();
        }

        private bool HandleMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    TurnLeft();
                    return true;
                case "2":
                    TurnRight();
                    return true;
                case "3":
                    DriveForward();
                    return true;
                case "4":
                    DriveBackward();
                    return true;
                case "5":
                    _driver.Rest();
                    return true;
                case "6":
                    RefuelCar();
                    return true;
                case "7":
                    return false;
                default:
                    Console.WriteLine("Ogiltigt val! Välj 1-7.");
                    return true;
            }
        }

        private void TurnLeft()
        {
            if (!_car.HasFuel())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bensinen är slut! Du måste tanka innan bilen kan röra sig.");
                Console.ResetColor();
                return;
            }

            _car.TurnLeft();
            _car.ConsumeFuel();
            _driver.IncreaseFatigue();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Bilen svänger vänster och är nu riktad åt {_car.GetDirectionInSwedish()}.");
            Console.ResetColor();
        }

        private void TurnRight()
        {
            if (!_car.HasFuel())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bensinen är slut! Du måste tanka innan bilen kan röra sig.");
                Console.ResetColor();
                return;
            }

            _car.TurnRight();
            _car.ConsumeFuel();
            _driver.IncreaseFatigue();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Bilen svänger höger och är nu riktad åt {_car.GetDirectionInSwedish()}.");
            Console.ResetColor();
        }

        private void DriveForward()
        {
            if (!_car.HasFuel())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bensinen är slut! Du måste tanka innan bilen kan röra sig.");
                Console.ResetColor();
                return;
            }

            _car.ConsumeFuel();
            _driver.IncreaseFatigue();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Bilen kör framåt åt {_car.GetDirectionInSwedish()}.");
            Console.ResetColor();
        }

        private void DriveBackward()
        {
            if (!_car.HasFuel())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bensinen är slut! Du måste tanka innan bilen kan röra sig.");
                Console.ResetColor();
                return;
            }

            _car.ConsumeFuel();
            _driver.IncreaseFatigue();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bilen backar.");
            Console.ResetColor();
        }

        private void RefuelCar()
        {
            _car.Refuel();
            _driver.IncreaseFatigue();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Bilen är nu tankad till sin fulla kapacitet ({_car.MaxFuel:F0} liter).");
            Console.ResetColor();
        }
    }
}