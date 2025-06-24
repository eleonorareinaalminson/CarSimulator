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
            // Returnera -1 för: null, whitespace, spaces och decimaler
            if (string.IsNullOrWhiteSpace(choice))
                return -1;

            // se att strängen inte har leading/trailing spaces
            if (choice != choice.Trim())
                return -1;

            // kolla efter decimaler
            if (choice.Contains('.') || choice.Contains(','))
                return -1;

            // Försök parsa - returnera faktiska numret även utanför intervallet
            if (int.TryParse(choice, out int number))
                return number;

            return -1;
        }

        public static string GetInvalidChoiceMessage()
        {
            return "Ogiltigt val! Välj 1-7.";
        }
    }
}