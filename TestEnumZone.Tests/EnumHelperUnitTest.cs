using NUnit.Framework;
using TestEnumZone.Application.Helper;

namespace TestEnumZone.Tests
{
    // Sample enums for testing
    public enum Status
    {
        [System.ComponentModel.Description("Initialized")]
        Init = 0,

        [System.ComponentModel.Description("Processing")]
        Processing = 10,

        [System.ComponentModel.Description("Completed")]
        Done = 100
    }

    // Explicit long underlying type to test overflow behavior
    public enum BigStatus : long
    {
        Small = 42L,
        VeryLarge = (long)int.MaxValue + 10L,  // Will overflow when Convert.ToInt32 is used
        VeryNegative = (long)int.MinValue - 10L
    }

    [TestFixture]
    public class EnumHelperUnitTest
    {
        // ---------- GetEnumDescription ----------

        [Test]
        public void GetEnumDescription_ReturnsDescriptionAttribute_WhenPresent()
        {
            // Arrange
            Enum value = Status.Processing;

            // Act
            var result = EnumHelper.GetEnumDescription(value);

            // Assert
            Assert.AreEqual("Processing", result);
        }

        [Test]
        public void GetEnumDescription_ReturnsEnumName_WhenNoDescription()
        {
            // Arrange
            // Define an enum without DescriptionAttribute
            var value = ConsoleColor.Red; // has no DescriptionAttribute

            // Act
            var result = EnumHelper.GetEnumDescription(value);

            // Assert
            Assert.AreEqual("Red", result);
        }

        [Test]
        public void GetEnumDescription_Null_ReturnsEmptyString()
        {
            // Arrange
            Enum value = null;

            // Act
            var result = EnumHelper.GetEnumDescription(value);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        // ---------- GetEnumValueString<TEnum> ----------

        [Test]
        public void GetEnumValueString_ReturnsName_WhenValid()
        {
            // Arrange
            Status? value = Status.Done;

            // Act
            var result = EnumHelper.GetEnumValueString(value);

            // Assert
            Assert.AreEqual("100", result);
        }

        [Test]
        public void GetEnumValueString_Null_ReturnsEmptyString()
        {
            // Arrange
            Status? value = null;

            // Act
            var result = EnumHelper.GetEnumValueString(value);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        // ---------- GetEnumValueInt<TEnum> ----------

        [Test]
        public void GetEnumValueInt_ReturnsUnderlyingInt_WhenWithinRange()
        {
            // Arrange
            Status? value = Status.Processing;

            // Act
            var result = EnumHelper.GetEnumValueInt(value);

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void GetEnumValueInt_Null_ReturnsNegativeOne()
        {
            // Arrange
            Status? value = null;

            // Act
            var result = EnumHelper.GetEnumValueInt(value);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void GetEnumValueInt_ThrowsOverflowException_WhenValueExceedsIntRange_Positive()
        {
            // Arrange
            BigStatus? value = BigStatus.VeryLarge;

            // Act + Assert
            Assert.Throws<OverflowException>(() => EnumHelper.GetEnumValueInt(value));
        }

        [Test]
        public void GetEnumValueInt_ThrowsOverflowException_WhenValueExceedsIntRange_Negative()
        {
            // Arrange
            BigStatus? value = BigStatus.VeryNegative;

            // Act + Assert
            Assert.Throws<OverflowException>(() => EnumHelper.GetEnumValueInt(value));
        }

        [Test]
        public void GetEnumValueInt_WorksForLongEnum_WhenWithinIntRange()
        {
            // Arrange
            BigStatus? value = BigStatus.Small;

            // Act
            var result = EnumHelper.GetEnumValueInt(value);

            // Assert
            Assert.AreEqual(42, result);
        }
    }
}
