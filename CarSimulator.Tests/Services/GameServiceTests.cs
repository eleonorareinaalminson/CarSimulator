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
        public void Constructor_WithoutParameters_ShouldCreateInstance()
        {
            // Arrange & Act
            var sut = new GameService();

            // Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void Constructor_WithMockService_ShouldAcceptDependency()
        {
            // Arrange
            var mockService = new Mock<IRandomUserService>();

            // Act
            var sut = new GameService(mockService.Object);

            // Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void Constructor_WithDependencyInjection_ShouldCreateValidInstance()
        {
            // Arrange & Act (done in Setup)

            // Assert
            Assert.IsNotNull(_sut);
        }


        [TestMethod]
        public void GameService_ShouldImplementIGameService()
        {
            // Arrange & Act (done in Setup)

            // Assert
            Assert.IsTrue(_sut is IGameService);
        }

        [TestMethod]
        public void GameService_ShouldBeOfCorrectType()
        {
            // Arrange & Act (done in Setup)

            // Assert
            Assert.IsTrue(_sut is GameService);
        }


        [TestMethod]
        public void MockSetup_InitialState_ShouldNotHaveCalledRandomUserService()
        {
            // Arrange & Act (done in Setup)

            // Assert
            _mockRandomUserService.Verify(x => x.GetRandomDriverAsync(), Times.Never);
        }

        [TestMethod]
        public async Task MockRandomUserService_WhenCalled_ShouldReturnConfiguredDriver()
        {
            // Arrange (done in Setup)

            // Act
            var result = await _mockRandomUserService.Object.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Driver", result.Name);
        }

        [TestMethod]
        public async Task MockRandomUserService_WhenCalled_ShouldReturnDriverWithCorrectEmail()
        {
            // Arrange (done in Setup)

            // Act
            var result = await _mockRandomUserService.Object.GetRandomDriverAsync();

            // Assert
            Assert.AreEqual("test@example.com", result.Email);
        }

        [TestMethod]
        public async Task MockRandomUserService_WhenCalled_ShouldReturnDriverWithZeroFatigue()
        {
            // Arrange (done in Setup)

            // Act
            var result = await _mockRandomUserService.Object.GetRandomDriverAsync();

            // Assert
            Assert.AreEqual(0, result.Fatigue);
        }

        [TestMethod]
        public async Task MockRandomUserService_WhenCalled_ShouldVerifyMethodInvocation()
        {
            // Arrange (done in Setup)

            // Act
            await _mockRandomUserService.Object.GetRandomDriverAsync();

            // Assert
            _mockRandomUserService.Verify(x => x.GetRandomDriverAsync(), Times.Once);
        }


        [TestMethod]
        public void GameService_WithInjectedMock_ShouldMaintainMockState()
        {
            // Arrange & Act (done in Setup)

            // Assert - Mock should be ready but not called
            _mockRandomUserService.Verify(x => x.GetRandomDriverAsync(), Times.Never);
        }
    }
}