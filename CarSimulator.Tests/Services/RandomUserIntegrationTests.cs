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
        public async Task GetRandomDriver_FromAPI_ShouldReturnValidDriver()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Email);
            Assert.IsTrue(result.Name.Length > 0);
            Assert.IsTrue(result.Email.Contains("@"));
            Assert.AreEqual(0, result.Fatigue);
        }

        [TestMethod]
        public async Task GetRandomDriver_MultipleCalls_ShouldReturnDifferentDrivers()
        {
            // Act
            var driver1 = await _sut.GetRandomDriverAsync();
            var driver2 = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(driver1);
            Assert.IsNotNull(driver2);

            // Förare ska vara olika (extremt låg sannolikhet att få samma)
            var isDifferent = driver1.Name != driver2.Name || driver1.Email != driver2.Email;
            Assert.IsTrue(isDifferent, "Multiple API calls should return different drivers");
        }

        [TestMethod]
        public async Task GetRandomDriver_ShouldReturnDriverWithValidEmailFormat()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(result.Email);
            Assert.IsTrue(result.Email.Contains("@"));
            Assert.IsTrue(result.Email.Contains("."));
            Assert.IsTrue(result.Email.Length > 5); // Minsta rimliga e-post
        }

        [TestMethod]
        public async Task GetRandomDriver_ShouldReturnDriverWithReasonableName()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert
            Assert.IsNotNull(result.Name);
            Assert.IsTrue(result.Name.Length >= 2); // Minst 2 tecken
            Assert.IsTrue(result.Name.Length <= 100); // Max 100 tecken
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Name));
        }

        [TestMethod]
        public void RandomUserService_ShouldImplementIRandomUserService()
        {
            // Assert
            Assert.IsTrue(_sut is IRandomUserService);
            Assert.IsTrue(_sut is RandomUserService);
        }

        [TestMethod]
        public async Task GetRandomDriver_APIResponse_ShouldBeConsistent()
        {
            // Act
            var result = await _sut.GetRandomDriverAsync();

            // Assert - Kontrollera att alla förväntade egenskaper finns
            Assert.IsNotNull(result.Name, "Name should not be null");
            Assert.IsNotNull(result.Email, "Email should not be null");
            Assert.AreEqual(0, result.Fatigue, "New driver should have 0 fatigue");

            // Kontrollera att namn innehåller minst ett mellanslag (förnamn + efternamn)
            Assert.IsTrue(result.Name.Contains(" "), "Name should contain at least one space (first + last name)");
        }
    }
}