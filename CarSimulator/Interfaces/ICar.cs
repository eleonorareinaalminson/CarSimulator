using CarSimulator.Enums;

namespace CarSimulator.Interfaces
{
    public interface ICar
    {
        Direction Direction { get; set; }
        double Fuel { get; set; }
        double MaxFuel { get; }
        bool HasFuel();
        void ConsumeFuel();
        void Refuel();
        void TurnLeft();
        void TurnRight();
        string GetDirectionInSwedish();
    }
}