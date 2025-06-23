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
        private string _lastActionMessage = ""; 

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
            Console.WriteLine($"Email: {_driver.Email}");
            Console.WriteLine("\nTryck på valfri tangent för att börja...");
            Console.ReadKey();

            bool continueGame = true;
            while (continueGame)
            {
                Console.Clear(); 

                ShowGameScreen();

                Console.Write("\nVälj ett alternativ: ");
                var choice = Console.ReadLine();

                continueGame = HandleMenuChoice(choice);

                if (continueGame && !string.IsNullOrEmpty(_lastActionMessage))
                {
                    Thread.Sleep(1000);
                }
            }

            Console.Clear();
            Console.WriteLine("Tack för att du spelade Bil-Simulatorn!");
        }

        private void ShowGameScreen()
        {
            Console.WriteLine("=== BIL-SIMULATOR ===\n");

            if (!string.IsNullOrEmpty(_lastActionMessage))
            {
                Console.WriteLine($"Senaste handling: {_lastActionMessage}\n");
            }

            ShowStatus();
            ShowWarnings();
            ShowMenu();
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
            Console.WriteLine($"Förare: {_driver.Name}");
            Console.WriteLine($"Riktning: {_car.GetDirectionInSwedish()}");

            ShowFuelStatus();

            ShowFatigueStatus();
        }

        private void ShowWarnings()
        {
            var warning = _driver.GetFatigueWarning();
            if (!string.IsNullOrEmpty(warning))
            {
                Console.WriteLine();
                _driver.ShowFatigueWarningWithColor();
            }
        }

        private void ShowFuelStatus()
        {
            var fuelPercentage = (_car.Fuel / _car.MaxFuel) * 100;

            Console.Write("Bensin: ");

            if (fuelPercentage > 50)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (fuelPercentage > 20)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine($"{_car.Fuel:F0}/{_car.MaxFuel:F0} liter");
            Console.ResetColor();
        }

        private void ShowFatigueStatus()
        {
            Console.Write("Trötthet: ");

            if (_driver.Fatigue <= 5)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (_driver.Fatigue <= 8)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
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
                    _lastActionMessage = $"{_driver.Name} tar en rast och känner sig utvilad!";
                    return true;
                case "6":
                    RefuelCar();
                    return true;
                case "7":
                    return false;
                default:
                    _lastActionMessage = "Ogiltigt val! Välj 1-7.";
                    return true;
            }
        }

        private void TurnLeft()
        {
            if (!_car.HasFuel())
            {
                _lastActionMessage = "Bensinen är slut! Du måste tanka innan bilen kan röra sig.";
                return;
            }

            _car.TurnLeft();
            _car.ConsumeFuel();
            _driver.IncreaseFatigue();

            _lastActionMessage = $"Bilen svänger vänster och är nu riktad åt {_car.GetDirectionInSwedish()}.";
        }

        private void TurnRight()
        {
            if (!_car.HasFuel())
            {
                _lastActionMessage = "Bensinen är slut! Du måste tanka innan bilen kan röra sig.";
                return;
            }

            _car.TurnRight();
            _car.ConsumeFuel();
            _driver.IncreaseFatigue();

            _lastActionMessage = $"Bilen svänger höger och är nu riktad åt {_car.GetDirectionInSwedish()}.";
        }

        private void DriveForward()
        {
            if (!_car.HasFuel())
            {
                _lastActionMessage = "Bensinen är slut! Du måste tanka innan bilen kan röra sig.";
                return;
            }

            _car.ConsumeFuel();
            _driver.IncreaseFatigue();

            _lastActionMessage = $"Bilen kör framåt åt {_car.GetDirectionInSwedish()}.";
        }

        private void DriveBackward()
        {
            if (!_car.HasFuel())
            {
                _lastActionMessage = "Bensinen är slut! Du måste tanka innan bilen kan röra sig.";
                return;
            }

            _car.ConsumeFuel();
            _driver.IncreaseFatigue();

            _lastActionMessage = "Bilen backar.";
        }

        private void RefuelCar()
        {
            _car.Refuel();
            _driver.IncreaseFatigue();

            _lastActionMessage = $"Bilen är nu tankad till sin fulla kapacitet ({_car.MaxFuel:F0} liter).";
        }
    }
}