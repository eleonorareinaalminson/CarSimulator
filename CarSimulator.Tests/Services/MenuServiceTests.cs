using CarSimulator.Services;

namespace CarSimulator.Tests.Services
{
    [TestClass]
    public class MenuServiceTests
    {
        [TestMethod]
        public void IsValidMenuChoice_ValidChoices1To7_ShouldReturnTrue()
        {
            // Arrange
            var validChoices = new[] { "1", "2", "3", "4", "5", "6", "7" };

            // Act & Assert
            foreach (var choice in validChoices)
            {
                var result = MenuService.IsValidMenuChoice(choice);
                Assert.IsTrue(result, $"Choice '{choice}' should be valid");
            }
        }

        [TestMethod]
        public void IsValidMenuChoice_InvalidChoices_ShouldReturnFalse()
        {
            // Arrange
            var invalidChoices = new[] { "0", "8", "9", "abc", "", " ", null };

            // Act & Assert
            foreach (var choice in invalidChoices)
            {
                var result = MenuService.IsValidMenuChoice(choice);
                Assert.IsFalse(result, $"Choice '{choice}' should be invalid");
            }
        }

        [TestMethod]
        public void ParseMenuChoice_ValidChoice_ShouldReturnCorrectNumber()
        {
            // Arrange & Act & Assert
            Assert.AreEqual(1, MenuService.ParseMenuChoice("1"));
            Assert.AreEqual(3, MenuService.ParseMenuChoice("3"));
            Assert.AreEqual(7, MenuService.ParseMenuChoice("7"));
        }

        [TestMethod]
        public void ParseMenuChoice_InvalidChoice_ShouldReturnNegativeOne()
        {
            // Arrange
            var invalidChoices = new[] { "abc", "", " ", null };

            // Act & Assert
            foreach (var choice in invalidChoices)
            {
                var result = MenuService.ParseMenuChoice(choice);
                Assert.AreEqual(-1, result, $"Invalid choice '{choice}' should return -1");
            }
        }

        [TestMethod]
        public void ParseMenuChoice_NumericButInvalidRange_ShouldReturnActualNumber()
        {
            // Act & Assert - ParseMenuChoice returnerar det parsade talet även om det är utanför giltigt intervall
            Assert.AreEqual(0, MenuService.ParseMenuChoice("0"));
            Assert.AreEqual(8, MenuService.ParseMenuChoice("8"));
            Assert.AreEqual(99, MenuService.ParseMenuChoice("99"));
        }

        [TestMethod]
        public void GetInvalidChoiceMessage_ShouldReturnCorrectMessage()
        {
            // Act
            var result = MenuService.GetInvalidChoiceMessage();

            // Assert
            Assert.AreEqual("Ogiltigt val! Välj 1-7.", result);
        }

        [TestMethod]
        public void IsValidMenuChoice_EdgeCases_ShouldHandleCorrectly()
        {
            // Test edge cases
            Assert.IsTrue(MenuService.IsValidMenuChoice("1"));   // Minimum valid
            Assert.IsTrue(MenuService.IsValidMenuChoice("7"));   // Maximum valid
            Assert.IsFalse(MenuService.IsValidMenuChoice("0"));  // Below minimum
            Assert.IsFalse(MenuService.IsValidMenuChoice("8"));  // Above maximum
        }

        [TestMethod]
        public void IsValidMenuChoice_WhitespaceHandling_ShouldReturnFalse()
        {
            // Arrange
            var whitespaceChoices = new[] { " ", "  ", "\t", "\n", "\r\n" };

            // Act & Assert
            foreach (var choice in whitespaceChoices)
            {
                var result = MenuService.IsValidMenuChoice(choice);
                Assert.IsFalse(result, $"Whitespace choice '{choice}' should be invalid");
            }
        }
    }
}