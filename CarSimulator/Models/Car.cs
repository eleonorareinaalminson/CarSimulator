using CarSimulator.Enums;
using CarSimulator.Interfaces;

namespace CarSimulator.Models
{
    public class Car : ICar
    {
        public Direction Direction { get; set; }
        public double Fuel { get; set; }
        public double MaxFuel { get; private set; } = 20.0; 

        public Car()
        {
            Direction = Direction.North;
            Fuel = MaxFuel; 
        }

        public bool HasFuel()
        {
            return Fuel > 0;
        }

        public void ConsumeFuel()
        {
            const double fuelConsumptionPerAction = 1.0;

            if (Fuel > 0)
            {
                Fuel = Math.Max(0, Fuel - fuelConsumptionPerAction); // Aldrig negativt
            }
        }

        public void Refuel()
        {
            Fuel = MaxFuel; // Fyller till 20 liter
        }

        public void TurnLeft()
        {
            Direction = Direction switch
            {
                Direction.North => Direction.West,
                Direction.West => Direction.South,
                Direction.South => Direction.East,
                Direction.East => Direction.North,
                _ => Direction.North
            };
        }

        public void TurnRight()
        {
            Direction = Direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => Direction.North
            };
        }

        public string GetDirectionInSwedish()
        {
            return Direction switch
            {
                Direction.North => "Norr",
                Direction.South => "Söder",
                Direction.East => "Öster",
                Direction.West => "Väster",
                _ => "Okänd"
            };
        }
    }
}