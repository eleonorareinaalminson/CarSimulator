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
        public void GetDirectionInSwedish_South_ShouldReturnS�der()
        {
            // Arrange
            _sut.Direction = Direction.South;

            // Act
            var result = _sut.GetDirectionInSwedish();

            // Assert
            Assert.AreEqual("S�der", result);
        }

        [TestMethod]
        public void GetDirectionInSwedish_East_ShouldReturn�ster()
        {
            // Arrange
            _sut.Direction = Direction.East;

            // Act
            var result = _sut.GetDirectionInSwedish();

            // Assert
            Assert.AreEqual("�ster", result);
        }

        [TestMethod]
        public void GetDirectionInSwedish_West_ShouldReturnV�ster()
        {
            // Arrange
            _sut.Direction = Direction.West;

            // Act
            var result = _sut.GetDirectionInSwedish();

            // Assert
            Assert.AreEqual("V�ster", result);
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
        public void ConsumeFuel_WithMinimalFuel_ShouldHandleCorrectly()
        {
            // Arrange
            _sut.Fuel = 0.1;

            // Act
            _sut.ConsumeFuel();

            // Assert
            Assert.AreEqual(0, _sut.Fuel, 0.001); // Med delta f�r double-j�mf�relse
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
    }
}