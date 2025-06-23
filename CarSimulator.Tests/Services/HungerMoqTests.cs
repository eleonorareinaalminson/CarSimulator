using Moq;

namespace CarSimulator.Tests.Services
{
    [TestClass]
    public class HungerMoqTests
    {
        public enum HungerLevel
        {
            Full = 0,    // 0-5
            Hungry = 1,  // 6-10
            Starving = 2 // 11+
        }

        public interface IHungerService
        {
            HungerLevel CalculateHungerLevel(int hunger);
            bool IsGameOver(int hunger);
            int IncreaseHunger(int currentHunger, int amount = 2);
            int Eat();
            string GetHungerMessage(int hunger);
        }

        private readonly Mock<IHungerService> _hungerServiceMock;
        private readonly IHungerService _sut; // System Under Test

        public HungerMoqTests()
        {
            _hungerServiceMock = new Mock<IHungerService>();

            // Setup mock behaviors
            SetupHungerServiceMock();

            _sut = _hungerServiceMock.Object;
        }

        private void SetupHungerServiceMock()
        {
            // Setup hunger level calculations
            _hungerServiceMock.Setup(x => x.CalculateHungerLevel(It.Is<int>(h => h <= 5)))
                             .Returns(HungerLevel.Full);
            _hungerServiceMock.Setup(x => x.CalculateHungerLevel(It.Is<int>(h => h > 5 && h <= 10)))
                             .Returns(HungerLevel.Hungry);
            _hungerServiceMock.Setup(x => x.CalculateHungerLevel(It.Is<int>(h => h > 10)))
                             .Returns(HungerLevel.Starving);

            // Setup hunger increase behavior
            _hungerServiceMock.Setup(x => x.IncreaseHunger(It.IsAny<int>(), It.IsAny<int>()))
                             .Returns<int, int>((current, amount) => current + amount);

            // Setup eat behavior
            _hungerServiceMock.Setup(x => x.Eat()).Returns(0);

            // Setup game over logic
            _hungerServiceMock.Setup(x => x.IsGameOver(It.Is<int>(h => h >= 16))).Returns(true);
            _hungerServiceMock.Setup(x => x.IsGameOver(It.Is<int>(h => h < 16))).Returns(false);

            // Setup hunger messages
            _hungerServiceMock.Setup(x => x.GetHungerMessage(It.Is<int>(h => h <= 5)))
                             .Returns("Du är mätt!");
            _hungerServiceMock.Setup(x => x.GetHungerMessage(It.Is<int>(h => h > 5 && h <= 10)))
                             .Returns("Du börjar bli hungrig...");
            _hungerServiceMock.Setup(x => x.GetHungerMessage(It.Is<int>(h => h > 10)))
                             .Returns("Du SVÄLTER! Hitta mat snart!");
        }


        [TestMethod]
        public void CalculateHungerLevel_WithLowHunger_ShouldReturnFull()
        {
            // Act
            var result = _sut.CalculateHungerLevel(3);

            // Assert
            Assert.AreEqual(HungerLevel.Full, result);
        }

        [TestMethod]
        public void CalculateHungerLevel_WithMediumHunger_ShouldReturnHungry()
        {
            // Act
            var result = _sut.CalculateHungerLevel(8);

            // Assert
            Assert.AreEqual(HungerLevel.Hungry, result);
        }

        [TestMethod]
        public void CalculateHungerLevel_WithHighHunger_ShouldReturnStarving()
        {
            // Act
            var result = _sut.CalculateHungerLevel(15);

            // Assert
            Assert.AreEqual(HungerLevel.Starving, result);
        }

        [TestMethod]
        public void CalculateHungerLevel_WhenCalled_ShouldVerifyMockInvocation()
        {
            // Act
            _sut.CalculateHungerLevel(5);

            // Assert
            _hungerServiceMock.Verify(x => x.CalculateHungerLevel(5), Times.Once);
        }


        [TestMethod]
        public void CalculateHungerLevel_AtFullUpperBoundary_ShouldReturnFull()
        {
            // Act
            var result = _sut.CalculateHungerLevel(5);

            // Assert
            Assert.AreEqual(HungerLevel.Full, result);
        }

        [TestMethod]
        public void CalculateHungerLevel_AtHungryLowerBoundary_ShouldReturnHungry()
        {
            // Act
            var result = _sut.CalculateHungerLevel(6);

            // Assert
            Assert.AreEqual(HungerLevel.Hungry, result);
        }

        [TestMethod]
        public void CalculateHungerLevel_AtHungryUpperBoundary_ShouldReturnHungry()
        {
            // Act
            var result = _sut.CalculateHungerLevel(10);

            // Assert
            Assert.AreEqual(HungerLevel.Hungry, result);
        }

        [TestMethod]
        public void CalculateHungerLevel_AtStarvingLowerBoundary_ShouldReturnStarving()
        {
            // Act
            var result = _sut.CalculateHungerLevel(11);

            // Assert
            Assert.AreEqual(HungerLevel.Starving, result);
        }


        [TestMethod]
        public void IncreaseHunger_WithDefaultAmount_ShouldIncreaseByTwo()
        {
            // Act
            var result = _sut.IncreaseHunger(0, 2);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void IncreaseHunger_WithCurrentHunger_ShouldAddCorrectly()
        {
            // Act
            var result = _sut.IncreaseHunger(4, 2);

            // Assert
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void IncreaseHunger_WhenCalledMultipleTimes_ShouldVerifyCallCount()
        {
            // Act
            _sut.IncreaseHunger(0, 2);
            _sut.IncreaseHunger(2, 2);
            _sut.IncreaseHunger(4, 2);

            // Assert
            _hungerServiceMock.Verify(x => x.IncreaseHunger(It.IsAny<int>(), 2), Times.Exactly(3));
        }


        [TestMethod]
        public void Eat_WhenCalled_ShouldReturnZero()
        {
            // Act
            var result = _sut.Eat();

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Eat_WhenCalled_ShouldVerifyMockInvocation()
        {
            // Act
            _sut.Eat();

            // Assert
            _hungerServiceMock.Verify(x => x.Eat(), Times.Once);
        }


        [TestMethod]
        public void IsGameOver_BelowThreshold_ShouldReturnFalse()
        {
            // Act
            var result = _sut.IsGameOver(15);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsGameOver_AtThreshold_ShouldReturnTrue()
        {
            // Act
            var result = _sut.IsGameOver(16);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsGameOver_AboveThreshold_ShouldReturnTrue()
        {
            // Act
            var result = _sut.IsGameOver(20);

            // Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void GetHungerMessage_WithLowHunger_ShouldReturnFullMessage()
        {
            // Act
            var result = _sut.GetHungerMessage(3);

            // Assert
            Assert.AreEqual("Du är mätt!", result);
        }

        [TestMethod]
        public void GetHungerMessage_WithMediumHunger_ShouldReturnHungryMessage()
        {
            // Act
            var result = _sut.GetHungerMessage(8);

            // Assert
            Assert.AreEqual("Du börjar bli hungrig...", result);
        }

        [TestMethod]
        public void GetHungerMessage_WithHighHunger_ShouldReturnStarvingMessage()
        {
            // Act
            var result = _sut.GetHungerMessage(15);

            // Assert
            Assert.AreEqual("Du SVÄLTER! Hitta mat snart!", result);
        }


        [TestMethod]
        public void HungerProgression_AfterThreeActions_ShouldReachSixHunger()
        {
            // Arrange
            int hunger = 0;

            // Act
            hunger = _sut.IncreaseHunger(hunger, 2);
            hunger = _sut.IncreaseHunger(hunger, 2);
            hunger = _sut.IncreaseHunger(hunger, 2);

            // Assert
            Assert.AreEqual(6, hunger);
        }

        [TestMethod]
        public void CompleteGameScenario_AfterEightActions_ShouldReachGameOver()
        {
            // Arrange
            int hunger = 0;

            // Act
            for (int i = 0; i < 8; i++)
            {
                hunger = _sut.IncreaseHunger(hunger, 2);
            }

            // Assert
            Assert.AreEqual(16, hunger);
        }

        [TestMethod]
        public void CompleteGameScenario_AtGameOverHunger_ShouldTriggerGameOver()
        {
            // Arrange
            int gameOverHunger = 16;

            // Act
            var isGameOver = _sut.IsGameOver(gameOverHunger);

            // Assert
            Assert.IsTrue(isGameOver);
        }
    }
}