using CarSimulator.Interfaces;

namespace CarSimulator.Models
{
    public class Driver : IDriver
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Fatigue { get; set; }

        public Driver(string name, string email)
        {
            Name = name;
            Email = email;
            Fatigue = 0;
        }

        public void IncreaseFatigue()
        {
            Fatigue++;
        }

        public void Rest()
        {
            Fatigue = 0;
            Console.WriteLine($"{Name} tar en rast och känner sig utvilad!");
        }

        public string GetFatigueWarning()
        {
            if (Fatigue >= 10)
                return "KRITISK TRÖTTHET! Föraren måste vila omedelbart!";
            else if (Fatigue >= 7)
                return "Föraren börjar bli trött och behöver ta en rast.";
            return "";
        }

        public void ShowFatigueWarningWithColor()
        {
            if (Fatigue >= 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("KRITISK TRÖTTHET! Föraren måste vila omedelbart!");
                Console.ResetColor();
            }
            else if (Fatigue >= 7)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Föraren börjar bli trött och behöver ta en rast.");
                Console.ResetColor();
            }
        }
    }
}