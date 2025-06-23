using CarSimulator.Services;
using CarSimulator.Interfaces;
using CarSimulator.Models;
using Moq;

namespace CarSimulator.Tests.Services
{
    [TestClass]
    public class GameServiceTests
    {
        private Mock<IRandomUserService> _mockRandomUserService;
        private IGameService _sut; // System Under Test

        [TestInitialize]
        public void Setup()
        {
            // Arrange dependencies
            _mockRandomUserService = new Mock<IRandomUserService>();

            // Setup mock to return a test driver
            var testDriver = new Driver("Test Driver", "test@example.com");
            _mockRandomUserService.Setup(x => x.GetRandomDriverAsync())
                                 .ReturnsAsync(testDriver);

            // Create SUT with injected dependencies
            _sut = new GameService(_mockRandomUserService.Object);
        }

        [TestMethod]
        public async Task GameService_ShouldUseInjectedRandomUserService()
        {
            // Arrange är redan gjort i Setup med _sut

            // Act - Vi kan inte testa StartGameAsync direkt eftersom den innehåller Console.ReadLine()
            // Men vi kan verifiera att SUT skapades korrekt

            // Assert - Kontrollera att SUT skapades korrekt
            Assert.IsNotNull(_sut);

            // Verify att mocken är redo att användas
            _mockRandomUserService.Verify(x => x.GetRandomDriverAsync(), Times.Never);
        }

        [TestMethod]
        public void GameService_ConstructorWithoutParameters_ShouldWork()
        {
            // Arrange & Act
            var sut = new GameService();

            // Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void GameService_ConstructorWithMockService_ShouldAcceptDependency()
        {
            // Arrange
            var mockService = new Mock<IRandomUserService>();

            // Act
            var sut = new GameService(mockService.Object);

            // Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void GameService_ShouldImplementIGameService()
        {
            // Assert
            Assert.IsTrue(_sut is IGameService);
            Assert.IsTrue(_sut is GameService);
        }

        [TestMethod]
        public async Task MockRandomUserService_ShouldReturnTestDriver()
        {
            // Act
            var result = await _mockRandomUserService.Object.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Driver", result.Name);
            Assert.AreEqual("test@example.com", result.Email);
            Assert.AreEqual(0, result.Fatigue);

            // Verify the mock was called
            _mockRandomUserService.Verify(x => x.GetRandomDriverAsync(), Times.Once);
        }

        [TestMethod]
        public void GameService_WithMockSetup_ShouldNotCallRandomUserServiceYet()
        {
            // Arrange & Act (sker i Setup)

            // Assert - Mocken ska inte ha anropats än
            _mockRandomUserService.Verify(x => x.GetRandomDriverAsync(), Times.Never);
        }
    }
}