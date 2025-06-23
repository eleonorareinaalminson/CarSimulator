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
        public void IncreaseFatigue_ShouldIncreaseByOne()
        {
            // Act
            _sut.IncreaseFatigue();

            // Assert
            Assert.AreEqual(1, _sut.Fatigue);
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
        public void GetFatigueWarning_ShouldReturnCriticalWarning_WhenFatigueIs10()
        {
            // Arrange
            _sut.Fatigue = 10;

            // Act
            var result = _sut.GetFatigueWarning();

            // Assert
            Assert.IsTrue(result.Contains("KRITISK TRÖTTHET"));
        }

        [TestMethod]
        public void GetFatigueWarning_ShouldReturnWarning_WhenFatigueIs7()
        {
            // Arrange
            _sut.Fatigue = 7;

            // Act
            var result = _sut.GetFatigueWarning();

            // Assert
            Assert.IsTrue(result.Contains("börjar bli trött"));
        }

        [TestMethod]
        public void GetFatigueWarning_ShouldReturnEmptyString_WhenFatigueIsLow()
        {
            // Arrange
            _sut.Fatigue = 5;

            // Act
            var result = _sut.GetFatigueWarning();

            // Assert
            Assert.AreEqual("", result);
        }
    }
}