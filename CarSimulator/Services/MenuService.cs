namespace CarSimulator.Services
{
    public static class MenuService
    {
        public static bool IsValidMenuChoice(string choice)
        {
            if (string.IsNullOrWhiteSpace(choice))
                return false;

            if (choice != choice.Trim())
                return false;

            if (choice.Contains('.') || choice.Contains(','))
                return false;

            if (!int.TryParse(choice, out int number))
                return false;

            return number >= 1 && number <= 7;
        }

        public static int ParseMenuChoice(string choice)
        {
            if (!IsValidMenuChoice(choice))
                return -1;

            return int.Parse(choice);
        }

        public static string GetInvalidChoiceMessage()
        {
            return "Ogiltigt val! Välj 1-7.";
        }
    }
}