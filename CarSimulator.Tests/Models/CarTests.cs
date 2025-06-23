using CarSimulator.Models;
using CarSimulator.Interfaces;
using CarSimulator.Enums;

namespace CarSimulator.Tests.Models
{
    [TestClass]
    public class CarTests
    {
        private ICar _sut; // System Under Test

        [TestInitialize]
        public void Setup()
        {
            _sut = new Car();
        }


        [TestMethod]
        public void NewCar_ShouldHaveCorrectFuelAmount()
        {
            // Arrange & Act
            var sut = new Car();

            // Assert
            Assert.AreEqual(20.0, sut.Fuel);
            Assert.AreEqual(20.0, sut.MaxFuel);
        }

        [TestMethod]
        public void NewCar_ShouldStartFacingNorth()
        {
            // Arrange & Act
            var sut = new Car();

            // Assert
            Assert.AreEqual(Direction.North, sut.Direction);
        }


        [TestMethod]
        public void HasFuel_WhenFuelExists_ShouldReturnTrue()
        {
            // Arrange
            _sut.Fuel = 5;

            // Act
            var result = _sut.HasFuel();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasFuel_WhenNoFuel_ShouldReturnFalse()
        {
            // Arrange
            _sut.Fuel = 0;

            // Act
            var result = _sut.HasFuel();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ConsumeFuel_ShouldDecreaseFuelByTwo()
        {
            // Arrange
            _sut.Fuel = 10;

            // Act
            _sut.ConsumeFuel();

            // Assert
            Assert.AreEqual(8, _sut.Fuel); // Ändrat från 9 till 8
        }

        [TestMethod]
        public void Refuel_ShouldFillTankToMax()
        {
            // Arrange
            _sut.Fuel = 5;

            // Act
            _sut.Refuel();

            // Assert
            Assert.AreEqual(_sut.MaxFuel, _sut.Fuel);
        }


        [TestMethod]
        public void TurnLeft_FromNorth_ShouldFaceWest()
        {
            // Arrange
            _sut.Direction = Direction.North;

            // Act
            _sut.TurnLeft();

            // Assert
            Assert.AreEqual(Direction.West, _sut.Direction);
        }

        [TestMethod]
        public void TurnLeft_FromEast_ShouldFaceNorth()
        {
            // Arrange
            _sut.Direction = Direction.East;

            // Act
            _sut.TurnLeft();

            // Assert
            Assert.AreEqual(Direction.North, _sut.Direction);
        }

        [TestMethod]
        public void TurnLeft_FromSouth_ShouldFaceEast()
        {
            // Arrange
            _sut.Direction = Direction.South;

            // Act
            _sut.TurnLeft();

            // Assert
            Assert.AreEqual(Direction.East, _sut.Direction);
        }

        [TestMethod]
        public void TurnLeft_FromWest_ShouldFaceSouth()
        {
            // Arrange
            _sut.Direction = Direction.West;

            // Act
            _sut.TurnLeft();

            // Assert
            Assert.AreEqual(Direction.South, _sut.Direction);
        }


        [TestMethod]
        public void TurnRight_FromNorth_ShouldFaceEast()
        {
            // Arrange
            _sut.Direction = Direction.North;

            // Act
            _sut.TurnRight();

            // Assert
            Assert.AreEqual(Direction.East, _sut.Direction);
        }

        [TestMethod]
        public void TurnRight_FromEast_ShouldFaceSouth()
        {
            // Arrange
            _sut.Direction = Direction.East;

            // Act
            _sut.TurnRight();

            // Assert
            Assert.AreEqual(Direction.South, _sut.Direction);
        }

        [TestMethod]
        public void TurnRight_FromSouth_ShouldFaceWest()
        {
            // Arrange
            _sut.Direction = Direction.South;

            // Act
            _sut.TurnRight();

            // Assert
            Assert.AreEqual(Direction.West, _sut.Direction);
        }

        [TestMethod]
        public void TurnRight_FromWest_ShouldFaceNorth()
        {
            // Arrange
            _sut.Direction = Direction.West;

            // Act
            _sut.TurnRight();

            // Assert
            Assert.AreEqual(Direction.North, _sut.Direction);
        }


        [TestMethod]
        public void GetDirectionInSwedish_North_ShouldReturnNorr()
        {
            // Arrange
            _sut.Direction = Direction.North;

            // Act
            var result = _sut.GetDirectionInSwedish();

            // Assert
            Assert.AreEqual("Norr", result);
        }

        [TestMethod]
        public void GetDirectionInSwedish_South_ShouldReturnSöder()
        {
            // Arrange
            _sut.Direction = Direction.South;

            // Act
            var result = _sut.GetDirectionInSwedish();

            // Assert
            Assert.AreEqual("Söder", result);
        }

        [TestMethod]
        public void GetDirectionInSwedish_East_ShouldReturnÖster()
        {
            // Arrange
            _sut.Direction = Direction.East;

            // Act
            var result = _sut.GetDirectionInSwedish();

            // Assert
            Assert.AreEqual("Öster", result);
        }

        [TestMethod]
        public void GetDirectionInSwedish_West_ShouldReturnVäster()
        {
            // Arrange
            _sut.Direction = Direction.West;

            // Act
            var result = _sut.GetDirectionInSwedish();

            // Assert
            Assert.AreEqual("Väster", result);
        }

        [TestMethod]
        public void ConsumeFuel_WhenNoFuel_ShouldStayAtZero()
        {
            // Arrange
            _sut.Fuel = 0;

            // Act
            _sut.ConsumeFuel();

            // Assert
            Assert.AreEqual(0, _sut.Fuel);
        }

        [TestMethod]
        public void ConsumeFuel_WithMinimalFuel_ShouldGoToZero()
        {
            // Arrange
            _sut.Fuel = 0.1;

            // Act
            _sut.ConsumeFuel();

            // Assert
            Assert.AreEqual(0, _sut.Fuel, 0.001); // Med delta för double-jämförelse
        }

        [TestMethod]
        public void TurnLeft_FourTimes_ShouldReturnToOriginalDirection()
        {
            // Arrange
            var originalDirection = _sut.Direction;

            // Act
            for (int i = 0; i < 4; i++)
            {
                _sut.TurnLeft();
            }

            // Assert
            Assert.AreEqual(originalDirection, _sut.Direction);
        }

        [TestMethod]
        public void TurnRight_FourTimes_ShouldReturnToOriginalDirection()
        {
            // Arrange
            var originalDirection = _sut.Direction;

            // Act
            for (int i = 0; i < 4; i++)
            {
                _sut.TurnRight();
            }

            // Assert
            Assert.AreEqual(originalDirection, _sut.Direction);
        }

        // ===== NYA TESTER FÖR 2L BRÄNSLEFÖRBRUKNING =====

        [TestMethod]
        public void ConsumeFuel_WithExactlyTwoLiters_ShouldEmptyTank()
        {
            // Arrange
            _sut.Fuel = 2.0;

            // Act
            _sut.ConsumeFuel();

            // Assert
            Assert.AreEqual(0, _sut.Fuel, 0.001);
        }

        [TestMethod]
        public void ConsumeFuel_WithOnePointFiveLiters_ShouldGoToZero()
        {
            // Arrange
            _sut.Fuel = 1.5;

            // Act
            _sut.ConsumeFuel();

            // Assert
            Assert.AreEqual(0, _sut.Fuel, 0.001);
        }

        [TestMethod]
        public void ConsumeFuel_MultipleTimes_ShouldDecreaseProperly()
        {
            // Arrange
            _sut.Fuel = 10;

            // Act
            _sut.ConsumeFuel(); // 10 -> 8
            _sut.ConsumeFuel(); // 8 -> 6

            // Assert
            Assert.AreEqual(6, _sut.Fuel, 0.001);
        }

        [TestMethod]
        public void ConsumeFuel_UntilEmpty_ShouldHandleCorrectly()
        {
            // Arrange
            _sut.Fuel = 5; // 5 liter

            // Act & Assert
            _sut.ConsumeFuel(); // 5 -> 3
            Assert.AreEqual(3, _sut.Fuel, 0.001);

            _sut.ConsumeFuel(); // 3 -> 1
            Assert.AreEqual(1, _sut.Fuel, 0.001);

            _sut.ConsumeFuel(); // 1 -> 0 (inte -1)
            Assert.AreEqual(0, _sut.Fuel, 0.001);

            _sut.ConsumeFuel(); // 0 -> 0 (förblir 0)
            Assert.AreEqual(0, _sut.Fuel, 0.001);
        }

        [TestMethod]
        public void Car_ShouldImplementICar()
        {
            // Assert
            Assert.IsTrue(_sut is ICar);
        }

        [TestMethod]
        public void Fuel_ShouldNeverBeNegative()
        {
            // Arrange
            _sut.Fuel = 0.5;

            // Act
            _sut.ConsumeFuel();
            _sut.ConsumeFuel(); // Försök konsumera mer än vad som finns

            // Assert
            Assert.IsTrue(_sut.Fuel >= 0, "Fuel should never be negative");
        }
    }
}