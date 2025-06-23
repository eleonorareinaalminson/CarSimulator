using CarSimulator.Models;
using CarSimulator.Interfaces;

namespace CarSimulator.Tests.Models
{
    [TestClass]
    public class DriverTests
    {
        private IDriver _sut; // System Under Test

        [TestInitialize]
        public void Setup()
        {
            _sut = new Driver("Test Driver", "test@example.com");
        }


        [TestMethod]
        public void NewDriver_ShouldHaveZeroFatigue()
        {
            // Act & Assert
            Assert.AreEqual(0, _sut.Fatigue);
        }

        [TestMethod]
        public void NewDriver_ShouldHaveCorrectName()
        {
            // Act & Assert
            Assert.AreEqual("Test Driver", _sut.Name);
        }

        [TestMethod]
        public void NewDriver_ShouldHaveCorrectEmail()
        {
            // Act & Assert
            Assert.AreEqual("test@example.com", _sut.Email);
        }


        [TestMethod]
        public void IncreaseFatigue_ShouldIncreaseByOne()
        {
            // Act
            _sut.IncreaseFatigue();

            // Assert
            Assert.AreEqual(1, _sut.Fatigue);
        }

        [TestMethod]
        public void IncreaseFatigue_MultipleTimes_ShouldAccumulate()
        {
            // Act
            _sut.IncreaseFatigue();
            _sut.IncreaseFatigue();
            _sut.IncreaseFatigue();

            // Assert
            Assert.AreEqual(3, _sut.Fatigue);
        }

        [TestMethod]
        public void Rest_ShouldResetFatigueToZero()
        {
            // Arrange
            _sut.Fatigue = 8;

            // Act
            _sut.Rest();

            // Assert
            Assert.AreEqual(0, _sut.Fatigue);
        }


        [TestMethod]
        public void GetFatigueWarning_LowFatigue_ShouldReturnEmptyString()
        {
            // Arrange
            _sut.Fatigue = 5;

            // Act
            var result = _sut.GetFatigueWarning();

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void GetFatigueWarning_ModerateFatigue_ShouldReturnWarning()
        {
            // Arrange
            _sut.Fatigue = 7;

            // Act
            var result = _sut.GetFatigueWarning();

            // Assert
            Assert.IsTrue(result.Contains("börjar bli trött"));
        }

        [TestMethod]
        public void GetFatigueWarning_HighFatigue_ShouldReturnCriticalWarning()
        {
            // Arrange
            _sut.Fatigue = 10;

            // Act
            var result = _sut.GetFatigueWarning();

            // Assert
            Assert.IsTrue(result.Contains("KRITISK TRÖTTHET"));
        }

        [TestMethod]
        public void GetFatigueWarning_BoundaryAt6_ShouldReturnEmptyString()
        {
            // Arrange
            _sut.Fatigue = 6;

            // Act
            var result = _sut.GetFatigueWarning();

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void GetFatigueWarning_BoundaryAt9_ShouldReturnWarning()
        {
            // Arrange
            _sut.Fatigue = 9;

            // Act
            var result = _sut.GetFatigueWarning();

            // Assert
            Assert.IsTrue(result.Contains("börjar bli trött"));
        }

        [TestMethod]
        public void Driver_ShouldImplementIDriver()
        {
            // Assert
            Assert.IsTrue(_sut is IDriver);
        }

        [TestMethod]
        public void IncreaseFatigue_ManyTimes_ShouldNotOverflow()
        {
            // Act
            for (int i = 0; i < 1000; i++)
            {
                _sut.IncreaseFatigue();
            }

            // Assert
            Assert.AreEqual(1000, _sut.Fatigue);
            Assert.IsTrue(_sut.Fatigue > 0); // Säkerställ att det inte överflutet
        }

        [TestMethod]
        public void GetFatigueWarning_ExtremelyHighFatigue_ShouldStillReturnCritical()
        {
            // Arrange
            _sut.Fatigue = 50;

            // Act
            var result = _sut.GetFatigueWarning();

            // Assert
            Assert.IsTrue(result.Contains("KRITISK TRÖTTHET"));
        }

        // Test för att verifiera exakta meddelanden
        [TestMethod]
        public void GetFatigueWarning_Fatigue7_ShouldReturnExactMessage()
        {
            // Arrange
            _sut.Fatigue = 7;

            // Act
            var result = _sut.GetFatigueWarning();

            // Assert
            Assert.AreEqual("Föraren börjar bli trött och behöver ta en rast.", result);
        }

        [TestMethod]
        public void GetFatigueWarning_Fatigue10_ShouldReturnExactMessage()
        {
            // Arrange
            _sut.Fatigue = 10;

            // Act
            var result = _sut.GetFatigueWarning();

            // Assert
            Assert.AreEqual("KRITISK TRÖTTHET! Föraren måste vila omedelbart!", result);
        }
    }
}