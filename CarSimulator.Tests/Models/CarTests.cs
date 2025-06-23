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
        public void NewCar_ShouldHaveFullTankAndNorthDirection()
        {
            // Arrange & Act
            var sut = new Car();

            // Assert
            Assert.AreEqual(20.0, sut.Fuel);
            Assert.AreEqual(20.0, sut.MaxFuel);
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
        public void ConsumeFuel_ShouldDecreaseFuelByOne()
        {
            // Arrange
            _sut.Fuel = 10;

            // Act
            _sut.ConsumeFuel();

            // Assert
            Assert.AreEqual(9, _sut.Fuel);
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
        public void TurnLeft_FromNorth_ShouldGiveWest()
        {
            // Arrange
            _sut.Direction = Direction.North;

            // Act
            _sut.TurnLeft();

            // Assert
            Assert.AreEqual(Direction.West, _sut.Direction);
        }

        [TestMethod]
        public void TurnRight_FromNorth_ShouldGiveEast()
        {
            // Arrange
            _sut.Direction = Direction.North;

            // Act
            _sut.TurnRight();

            // Assert
            Assert.AreEqual(Direction.East, _sut.Direction);
        }

        [TestMethod]
        public void AllDirections_TestNorthSouthEastWest()
        {
            // Arrange
            _sut.Direction = Direction.North;

            // Act & Assert
            _sut.TurnRight();
            Assert.AreEqual(Direction.East, _sut.Direction);

            _sut.TurnRight();
            Assert.AreEqual(Direction.South, _sut.Direction);

            _sut.TurnRight();
            Assert.AreEqual(Direction.West, _sut.Direction);

            _sut.TurnRight();
            Assert.AreEqual(Direction.North, _sut.Direction);
        }

        [TestMethod]
        public void GetDirectionInSwedish_ShouldReturnSwedishNames()
        {
            // Test all directions using SUT
            _sut.Direction = Direction.North;
            Assert.AreEqual("Norr", _sut.GetDirectionInSwedish());

            _sut.Direction = Direction.South;
            Assert.AreEqual("Söder", _sut.GetDirectionInSwedish());

            _sut.Direction = Direction.East;
            Assert.AreEqual("Öster", _sut.GetDirectionInSwedish());

            _sut.Direction = Direction.West;
            Assert.AreEqual("Väster", _sut.GetDirectionInSwedish());
        }
    }
}