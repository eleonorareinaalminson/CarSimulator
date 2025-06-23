using Moq;

namespace CarSimulator.Tests.Services
{
    [TestClass]
    public class HungerMoqTests
    {
        // Future enum & interface för MOQ-tester!
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
        public void HungerService_CalculateHungerLevel_ShouldReturnCorrectLevels()
        {
            // Act & Assert
            Assert.AreEqual(HungerLevel.Full, _sut.CalculateHungerLevel(3));
            Assert.AreEqual(HungerLevel.Hungry, _sut.CalculateHungerLevel(8));
            Assert.AreEqual(HungerLevel.Starving, _sut.CalculateHungerLevel(15));

            // Verify
            _hungerServiceMock.Verify(x => x.CalculateHungerLevel(It.IsAny<int>()), Times.Exactly(3));
        }

        [TestMethod]
        public void HungerService_HungerProgression_EachActionIncreasesBy2()
        {
            // Act - Simulate 3 actions
            int hunger = 0;
            hunger = _sut.IncreaseHunger(hunger, 2); // Action 1: 0 -> 2
            hunger = _sut.IncreaseHunger(hunger, 2); // Action 2: 2 -> 4  
            hunger = _sut.IncreaseHunger(hunger, 2); // Action 3: 4 -> 6

            // Assert
            Assert.AreEqual(6, hunger);
            _hungerServiceMock.Verify(x => x.IncreaseHunger(It.IsAny<int>(), 2), Times.Exactly(3));
        }

        [TestMethod]
        public void HungerService_Eat_ShouldResetHungerToZero()
        {
            // Act
            var hungerAfterEating = _sut.Eat();

            // Assert
            Assert.AreEqual(0, hungerAfterEating);
            _hungerServiceMock.Verify(x => x.Eat(), Times.Once);
        }

        [TestMethod]
        public void HungerService_GameOver_ShouldTriggerAt16Hunger()
        {
            // Act & Assert
            Assert.IsFalse(_sut.IsGameOver(15));
            Assert.IsTrue(_sut.IsGameOver(16));
            Assert.IsTrue(_sut.IsGameOver(20));

            _hungerServiceMock.Verify(x => x.IsGameOver(It.IsAny<int>()), Times.Exactly(3));
        }

        [TestMethod]
        public void HungerService_GetHungerMessage_ShouldReturnCorrectMessages()
        {
            // Act & Assert
            Assert.AreEqual("Du är mätt!", _sut.GetHungerMessage(3));
            Assert.AreEqual("Du börjar bli hungrig...", _sut.GetHungerMessage(8));
            Assert.AreEqual("Du SVÄLTER! Hitta mat snart!", _sut.GetHungerMessage(15));

            _hungerServiceMock.Verify(x => x.GetHungerMessage(It.IsAny<int>()), Times.Exactly(3));
        }

        [TestMethod]
        public void HungerService_CompleteGameScenario_ShouldReachGameOver()
        {
            // Act - Simulate 8 actions to reach game over
            int hunger = 0;
            for (int i = 0; i < 8; i++)
            {
                hunger = _sut.IncreaseHunger(hunger, 2);
            }

            // Assert
            Assert.AreEqual(16, hunger); // 8 actions * 2 = 16
            Assert.IsTrue(_sut.IsGameOver(hunger));
        }

        [TestMethod]
        public void HungerService_BoundaryTesting_ShouldHandleEdgeCases()
        {
            // Test exact boundaries
            Assert.AreEqual(HungerLevel.Full, _sut.CalculateHungerLevel(5));     // Last Full
            Assert.AreEqual(HungerLevel.Hungry, _sut.CalculateHungerLevel(6));   // First Hungry
            Assert.AreEqual(HungerLevel.Hungry, _sut.CalculateHungerLevel(10));  // Last Hungry
            Assert.AreEqual(HungerLevel.Starving, _sut.CalculateHungerLevel(11)); // First Starving

            // Verify boundary tests
            _hungerServiceMock.Verify(x => x.CalculateHungerLevel(It.IsAny<int>()), Times.Exactly(4));
        }
    }
}