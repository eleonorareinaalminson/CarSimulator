using CarSimulator.Services;
using CarSimulator.Interfaces;

namespace CarSimulator.Tests.Services
{
    [TestClass]
    public class RandomUserIntegrationTests
    {
        private IRandomUserService _sut; // System Under Test

        [TestInitialize]
        public void Setup()
        {
            _sut = new RandomUserService();
        }


        [TestMethod]
        public async Task GetRandomDriver_FromAPI_ShouldReturnNonNullDriver()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetRandomDriver_FromAPI_ShouldReturnDriverWithName()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(result.Name);
            Assert.IsTrue(result.Name.Length > 0);
        }

        [TestMethod]
        public async Task GetRandomDriver_FromAPI_ShouldReturnDriverWithEmail()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(result.Email);
            Assert.IsTrue(result.Email.Contains("@"));
        }

        [TestMethod]
        public async Task GetRandomDriver_FromAPI_ShouldReturnDriverWithZeroFatigue()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.AreEqual(0, result.Fatigue);
        }


        [TestMethod]
        public async Task GetRandomDriver_Email_ShouldContainAtSymbol()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsTrue(result.Email.Contains("@"));
        }

        [TestMethod]
        public async Task GetRandomDriver_Email_ShouldContainDot()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsTrue(result.Email.Contains("."));
        }

        [TestMethod]
        public async Task GetRandomDriver_Email_ShouldHaveMinimumLength()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsTrue(result.Email.Length > 5); // Minsta rimliga e-post
        }


        [TestMethod]
        public async Task GetRandomDriver_Name_ShouldHaveMinimumLength()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsTrue(result.Name.Length >= 2); // Minst 2 tecken
        }

        [TestMethod]
        public async Task GetRandomDriver_Name_ShouldHaveMaximumLength()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsTrue(result.Name.Length <= 100); // Max 100 tecken
        }

        [TestMethod]
        public async Task GetRandomDriver_Name_ShouldNotBeWhitespace()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Name));
        }

        [TestMethod]
        public async Task GetRandomDriver_Name_ShouldContainSpace()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsTrue(result.Name.Contains(" "), "Name should contain at least one space (first + last name)");
        }


        [TestMethod]
        public void RandomUserService_ShouldImplementIRandomUserService()
        {
            // Arrange & Act (done in Setup)

            // Assert
            Assert.IsTrue(_sut is IRandomUserService);
        }

        [TestMethod]
        public void RandomUserService_ShouldBeOfCorrectType()
        {
            // Arrange & Act (done in Setup)

            // Assert
            Assert.IsTrue(_sut is RandomUserService);
        }


        [TestMethod]
        public async Task GetRandomDriver_MultipleCalls_ShouldReturnValidDrivers()
        {
            // Act
            var driver1 = await _sut.GetRandomDriverAsync();
            var driver2 = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(driver1);
            Assert.IsNotNull(driver2);
        }

        [TestMethod]
        public async Task GetRandomDriver_MultipleCalls_ShouldReturnDifferentDrivers()
        {
            // Act
            var driver1 = await _sut.GetRandomDriverAsync();
            var driver2 = await _sut.GetRandomDriverAsync();

            // Assert
            var isDifferent = driver1.Name != driver2.Name || driver1.Email != driver2.Email;
            Assert.IsTrue(isDifferent, "Multiple API calls should return different drivers");
        }


        [TestMethod]
        public async Task GetRandomDriver_APIResponse_ShouldHaveConsistentNameProperty()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(result.Name, "Name should not be null");
        }

        [TestMethod]
        public async Task GetRandomDriver_APIResponse_ShouldHaveConsistentEmailProperty()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(result.Email, "Email should not be null");
        }

        [TestMethod]
        public async Task GetRandomDriver_APIResponse_ShouldHaveConsistentFatigueProperty()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.AreEqual(0, result.Fatigue, "New driver should have 0 fatigue");
        }
    }
}