using CarSimulator.Services;

namespace CarSimulator.Tests.Services
{
    [TestClass]
    public class MenuServiceTests
    {
        [TestMethod]
        public void IsValidMenuChoice_ValidChoices1To3_ShouldReturnTrue()
        {
            // Act & Assert
            Assert.IsTrue(MenuService.IsValidMenuChoice("1"));
            Assert.IsTrue(MenuService.IsValidMenuChoice("2"));
            Assert.IsTrue(MenuService.IsValidMenuChoice("3"));
        }

        [TestMethod]
        public void IsValidMenuChoice_ValidChoices4To7_ShouldReturnTrue()
        {
            // Act & Assert
            Assert.IsTrue(MenuService.IsValidMenuChoice("4"));
            Assert.IsTrue(MenuService.IsValidMenuChoice("5"));
            Assert.IsTrue(MenuService.IsValidMenuChoice("6"));
            Assert.IsTrue(MenuService.IsValidMenuChoice("7"));
        }

        [TestMethod]
        public void IsValidMenuChoice_OutOfRangeLow_ShouldReturnFalse()
        {
            // Act & Assert
            Assert.IsFalse(MenuService.IsValidMenuChoice("0"));
        }

        [TestMethod]
        public void IsValidMenuChoice_OutOfRangeHigh_ShouldReturnFalse()
        {
            // Act & Assert
            Assert.IsFalse(MenuService.IsValidMenuChoice("8"));
            Assert.IsFalse(MenuService.IsValidMenuChoice("9"));
        }

        [TestMethod]
        public void IsValidMenuChoice_NonNumeric_ShouldReturnFalse()
        {
            // Act & Assert
            Assert.IsFalse(MenuService.IsValidMenuChoice("abc"));
            Assert.IsFalse(MenuService.IsValidMenuChoice("test"));
        }

        [TestMethod]
        public void IsValidMenuChoice_EmptyOrNull_ShouldReturnFalse()
        {
            // Act & Assert
            Assert.IsFalse(MenuService.IsValidMenuChoice(""));
            Assert.IsFalse(MenuService.IsValidMenuChoice(null));
        }

        [TestMethod]
        public void IsValidMenuChoice_Whitespace_ShouldReturnFalse()
        {
            // Act & Assert
            Assert.IsFalse(MenuService.IsValidMenuChoice(" "));
            Assert.IsFalse(MenuService.IsValidMenuChoice("  "));
            Assert.IsFalse(MenuService.IsValidMenuChoice("\t"));
        }

        [TestMethod]
        public void ParseMenuChoice_ValidChoicesLow_ShouldReturnCorrectNumber()
        {
            // Act & Assert
            Assert.AreEqual(1, MenuService.ParseMenuChoice("1"));
            Assert.AreEqual(2, MenuService.ParseMenuChoice("2"));
            Assert.AreEqual(3, MenuService.ParseMenuChoice("3"));
        }

        [TestMethod]
        public void ParseMenuChoice_ValidChoicesHigh_ShouldReturnCorrectNumber()
        {
            // Act & Assert
            Assert.AreEqual(5, MenuService.ParseMenuChoice("5"));
            Assert.AreEqual(6, MenuService.ParseMenuChoice("6"));
            Assert.AreEqual(7, MenuService.ParseMenuChoice("7"));
        }

        [TestMethod]
        public void ParseMenuChoice_NonNumeric_ShouldReturnNegativeOne()
        {
            // Act & Assert
            Assert.AreEqual(-1, MenuService.ParseMenuChoice("abc"));
            Assert.AreEqual(-1, MenuService.ParseMenuChoice(""));
            Assert.AreEqual(-1, MenuService.ParseMenuChoice(null));
        }

        [TestMethod]
        public void ParseMenuChoice_NumericButOutOfRange_ShouldReturnActualNumber()
        {
            // Act & Assert
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
        public void IsValidMenuChoice_BoundaryValues_ShouldHandleCorrectly()
        {
            // Act & Assert
            Assert.IsTrue(MenuService.IsValidMenuChoice("1"));   // Minimum valid
            Assert.IsTrue(MenuService.IsValidMenuChoice("7"));   // Maximum valid
            Assert.IsFalse(MenuService.IsValidMenuChoice("0"));  // Below minimum
            Assert.IsFalse(MenuService.IsValidMenuChoice("8"));  // Above maximum
        }

        [TestMethod]
        public void IsValidMenuChoice_WithLeadingZero_ShouldHandleCorrectly()
        {
            // Act & Assert
            Assert.IsTrue(MenuService.IsValidMenuChoice("01"));
            Assert.IsTrue(MenuService.IsValidMenuChoice("07"));
        }

        [TestMethod]
        public void IsValidMenuChoice_WithSpaces_ShouldReturnFalse()
        {
            // Act & Assert
            Assert.IsFalse(MenuService.IsValidMenuChoice(" 1"));
            Assert.IsFalse(MenuService.IsValidMenuChoice("1 "));
            Assert.IsFalse(MenuService.IsValidMenuChoice(" 1 "));
        }

        [TestMethod]
        public void ParseMenuChoice_WithDecimal_ShouldReturnNegativeOne()
        {
            // Act & Assert
            Assert.AreEqual(-1, MenuService.ParseMenuChoice("1.0"));
            Assert.AreEqual(-1, MenuService.ParseMenuChoice("5.5"));
        }


        [TestMethod]
        public void IsValidMenuChoice_WithDecimal_ShouldReturnFalse()
        {
            // Act & Assert
            Assert.IsFalse(MenuService.IsValidMenuChoice("1.0"));
            Assert.IsFalse(MenuService.IsValidMenuChoice("5.5"));
            Assert.IsFalse(MenuService.IsValidMenuChoice("3,5")); 
        }

        [TestMethod]
        public void IsValidMenuChoice_WithTabsAndNewlines_ShouldReturnFalse()
        {
            // Act & Assert
            Assert.IsFalse(MenuService.IsValidMenuChoice("\t1"));
            Assert.IsFalse(MenuService.IsValidMenuChoice("1\n"));
            Assert.IsFalse(MenuService.IsValidMenuChoice("\r1\r"));
        }

        [TestMethod]
        public void ParseMenuChoice_WithSpaces_ShouldReturnNegativeOne()
        {
            // Act & Assert
            Assert.AreEqual(-1, MenuService.ParseMenuChoice(" 1"));
            Assert.AreEqual(-1, MenuService.ParseMenuChoice("1 "));
            Assert.AreEqual(-1, MenuService.ParseMenuChoice(" 1 "));
        }

        [TestMethod]
        public void IsValidMenuChoice_ConsistencyTest_ValidChoicesShouldAlsoParseCorrectly()
        {
            // Arrange
            string[] validChoices = { "1", "2", "3", "4", "5", "6", "7" };

            foreach (string choice in validChoices)
            {
                // Act & Assert
                Assert.IsTrue(MenuService.IsValidMenuChoice(choice),
                    $"Choice '{choice}' should be valid");

                int parsed = MenuService.ParseMenuChoice(choice);
                Assert.AreEqual(int.Parse(choice), parsed,
                    $"Choice '{choice}' should parse to {choice}");
            }
        }

        [TestMethod]
        public void IsValidMenuChoice_ConsistencyTest_InvalidChoicesShouldNotParse()
        {
            // Arrange
            string[] invalidChoices = { "abc", " 1", "1.0", "", null }; 

            foreach (string choice in invalidChoices)
            {
                // Act & Assert
                Assert.IsFalse(MenuService.IsValidMenuChoice(choice),
                    $"Choice '{choice}' should be invalid");

                int parsed = MenuService.ParseMenuChoice(choice);
                Assert.AreEqual(-1, parsed,
                    $"Invalid choice '{choice}' should parse to -1");
            }
        }
    }
}