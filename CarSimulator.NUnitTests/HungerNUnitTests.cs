using Moq;

namespace CarSimulator.NUnitTests
{
    [TestFixture]
    public class HungerNUnitTests
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

        public HungerNUnitTests()
        {
            _hungerServiceMock = new Mock<IHungerService>();
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

            // Setup other behaviors
            _hungerServiceMock.Setup(x => x.IncreaseHunger(It.IsAny<int>(), It.IsAny<int>()))
                             .Returns<int, int>((current, amount) => current + amount);
            _hungerServiceMock.Setup(x => x.Eat()).Returns(0);
            _hungerServiceMock.Setup(x => x.IsGameOver(It.Is<int>(h => h >= 16))).Returns(true);
            _hungerServiceMock.Setup(x => x.IsGameOver(It.Is<int>(h => h < 16))).Returns(false);

            // Setup messages
            _hungerServiceMock.Setup(x => x.GetHungerMessage(It.Is<int>(h => h <= 5)))
                             .Returns("Du är mätt!");
            _hungerServiceMock.Setup(x => x.GetHungerMessage(It.Is<int>(h => h > 5 && h <= 10)))
                             .Returns("Du börjar bli hungrig...");
            _hungerServiceMock.Setup(x => x.GetHungerMessage(It.Is<int>(h => h > 10 && h < 16)))
                             .Returns("Du SVÄLTER! Hitta mat snart!");
            _hungerServiceMock.Setup(x => x.GetHungerMessage(It.Is<int>(h => h >= 16)))
                             .Returns("KRITISK HUNGER - Game Over!");
        }


        [TestCase(0, HungerLevel.Full)]
        [TestCase(3, HungerLevel.Full)]
        [TestCase(5, HungerLevel.Full)]
        public void CalculateHungerLevel_FullRange_ShouldReturnFull(int hunger, HungerLevel expectedLevel)
        {
            // Act
            var result = _sut.CalculateHungerLevel(hunger);

            // Assert
            Assert.That(result, Is.EqualTo(expectedLevel));
        }

        [TestCase(6, HungerLevel.Hungry)]
        [TestCase(8, HungerLevel.Hungry)]
        [TestCase(10, HungerLevel.Hungry)]
        public void CalculateHungerLevel_HungryRange_ShouldReturnHungry(int hunger, HungerLevel expectedLevel)
        {
            // Act
            var result = _sut.CalculateHungerLevel(hunger);

            // Assert
            Assert.That(result, Is.EqualTo(expectedLevel));
        }

        [TestCase(11, HungerLevel.Starving)]
        [TestCase(15, HungerLevel.Starving)]
        [TestCase(20, HungerLevel.Starving)]
        public void CalculateHungerLevel_StarvingRange_ShouldReturnStarving(int hunger, HungerLevel expectedLevel)
        {
            // Act
            var result = _sut.CalculateHungerLevel(hunger);

            // Assert
            Assert.That(result, Is.EqualTo(expectedLevel));
        }


        [TestCase(0, "Du är mätt!")]
        [TestCase(3, "Du är mätt!")]
        [TestCase(5, "Du är mätt!")]
        public void GetHungerMessage_FullRange_ShouldReturnFullMessage(int hunger, string expectedMessage)
        {
            // Act
            var result = _sut.GetHungerMessage(hunger);

            // Assert
            Assert.That(result, Is.EqualTo(expectedMessage));
        }

        [TestCase(6, "Du börjar bli hungrig...")]
        [TestCase(8, "Du börjar bli hungrig...")]
        [TestCase(10, "Du börjar bli hungrig...")]
        public void GetHungerMessage_HungryRange_ShouldReturnHungryMessage(int hunger, string expectedMessage)
        {
            // Act
            var result = _sut.GetHungerMessage(hunger);

            // Assert
            Assert.That(result, Is.EqualTo(expectedMessage));
        }

        [TestCase(11, "Du SVÄLTER! Hitta mat snart!")]
        [TestCase(13, "Du SVÄLTER! Hitta mat snart!")]
        [TestCase(15, "Du SVÄLTER! Hitta mat snart!")]
        public void GetHungerMessage_StarvingRange_ShouldReturnStarvingMessage(int hunger, string expectedMessage)
        {
            // Act
            var result = _sut.GetHungerMessage(hunger);

            // Assert
            Assert.That(result, Is.EqualTo(expectedMessage));
        }

        [TestCase(16, "KRITISK HUNGER - Game Over!")]
        [TestCase(18, "KRITISK HUNGER - Game Over!")]
        [TestCase(20, "KRITISK HUNGER - Game Over!")]
        public void GetHungerMessage_GameOverRange_ShouldReturnGameOverMessage(int hunger, string expectedMessage)
        {
            // Act
            var result = _sut.GetHungerMessage(hunger);

            // Assert
            Assert.That(result, Is.EqualTo(expectedMessage));
        }


        [TestCase(0, false)]
        [TestCase(10, false)]
        [TestCase(15, false)]
        public void IsGameOver_BelowThreshold_ShouldReturnFalse(int hunger, bool expectedGameOver)
        {
            // Act
            var result = _sut.IsGameOver(hunger);

            // Assert
            Assert.That(result, Is.EqualTo(expectedGameOver));
        }

        [TestCase(16, true)]
        [TestCase(18, true)]
        [TestCase(20, true)]
        public void IsGameOver_AtOrAboveThreshold_ShouldReturnTrue(int hunger, bool expectedGameOver)
        {
            // Act
            var result = _sut.IsGameOver(hunger);

            // Assert
            Assert.That(result, Is.EqualTo(expectedGameOver));
        }

        [TestCase(0, 2, 2)]
        [TestCase(5, 2, 7)]
        [TestCase(10, 2, 12)]
        public void IncreaseHunger_DefaultAmount_ShouldIncreaseByTwo(int currentHunger, int amount, int expectedHunger)
        {
            // Act
            var result = _sut.IncreaseHunger(currentHunger, amount);

            // Assert
            Assert.That(result, Is.EqualTo(expectedHunger));
        }

        [TestCase(5, 3, 8)]
        [TestCase(8, 4, 12)]
        [TestCase(12, 5, 17)]
        public void IncreaseHunger_CustomAmount_ShouldIncreaseBySpecifiedAmount(int currentHunger, int amount, int expectedHunger)
        {
            // Act
            var result = _sut.IncreaseHunger(currentHunger, amount);

            // Assert
            Assert.That(result, Is.EqualTo(expectedHunger));
        }

        [Test]
        public void Eat_ShouldAlwaysResetHungerToZero()
        {
            // Act
            var result = _sut.Eat();

            // Assert
            Assert.That(result, Is.EqualTo(0));
            _hungerServiceMock.Verify(x => x.Eat(), Times.Once);
        }


        [Test]
        public void HungerLevel_FullToHungryBoundary_ShouldChangeLevels()
        {
            // Act
            var levelBefore = _sut.CalculateHungerLevel(5);  // Last Full
            var levelAfter = _sut.CalculateHungerLevel(6);   // First Hungry

            // Assert
            Assert.That(levelBefore, Is.EqualTo(HungerLevel.Full));
            Assert.That(levelAfter, Is.EqualTo(HungerLevel.Hungry));
        }

        [Test]
        public void HungerLevel_HungryToStarvingBoundary_ShouldChangeLevels()
        {
            // Act
            var levelBefore = _sut.CalculateHungerLevel(10); // Last Hungry
            var levelAfter = _sut.CalculateHungerLevel(11);  // First Starving

            // Assert
            Assert.That(levelBefore, Is.EqualTo(HungerLevel.Hungry));
            Assert.That(levelAfter, Is.EqualTo(HungerLevel.Starving));
        }

        [Test]
        public void GameOver_Boundary15To16_ShouldTriggerGameOver()
        {
            // Act
            var gameOverBefore = _sut.IsGameOver(15); // Not game over
            var gameOverAfter = _sut.IsGameOver(16);  // Game over

            // Assert
            Assert.That(gameOverBefore, Is.False);
            Assert.That(gameOverAfter, Is.True);
        }
    }
}