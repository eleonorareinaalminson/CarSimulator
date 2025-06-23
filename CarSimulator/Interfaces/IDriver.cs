namespace CarSimulator.Interfaces
{
    public interface IDriver
    {
        string Name { get; set; }
        string Email { get; set; }
        int Fatigue { get; set; }
        void IncreaseFatigue();
        void Rest();
        string GetFatigueWarning();
        void ShowFatigueWarningWithColor();
    }
}
