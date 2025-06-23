namespace CarSimulator.Services
{
    public static class MenuService
    {
        public static bool IsValidMenuChoice(string choice)
        {
            if (string.IsNullOrWhiteSpace(choice))
                return false;

            if (!int.TryParse(choice, out int number))
                return false;

            return number >= 1 && number <= 7;
        }

        public static int ParseMenuChoice(string choice)
        {
            if (int.TryParse(choice, out int number))
                return number;

            return -1; // Invalid choice
        }

        public static string GetInvalidChoiceMessage()
        {
            return "Ogiltigt val! Välj 1-7.";
        }
    }
}