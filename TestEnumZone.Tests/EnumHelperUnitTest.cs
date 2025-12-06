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
        // ---------- GetEnumName ----------
        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumName_ReturnsName_WhenValid()
        {
            // Arrange
            Status? value = Status.Done;

            // Act
            var result = EnumHelper.GetEnumName<Status>(value);

            // Assert
            Assert.AreEqual("Done", result);
        }
        // ---------- GetEnumDescription ----------

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumDescription_ReturnsDescriptionAttribute_WhenPresent()
        {
            // Arrange
            Enum value = Status.Processing;

            // Act
            var result = EnumHelper.GetEnumDescription(value);

            // Assert
            Assert.AreEqual("Processing", result);
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumDescription_ReturnsBlankString_WhenNoDescription()
        {
            // Arrange
            // Define an enum without DescriptionAttribute
            var value = ConsoleColor.Red; // has no DescriptionAttribute

            // Act
            var result = EnumHelper.GetEnumDescription(value);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
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

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumValueString_ReturnsName_WhenValid()
        {
            // Arrange
            Status? value = Status.Done;

            // Act
            var result = EnumHelper.GetEnumName(value);

            // Assert
            Assert.AreEqual("Done", result);
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumValueString_ReturnsNumericValue_WhenValid()
        {
            // Arrange
            Status? value = Status.Done;

            // Act
            var result = EnumHelper.GetEnumValueString(value);

            // Assert
            Assert.AreEqual("100", result);
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumValueString_ReturnsNumericValue_Zero()
        {
            // Arrange
            Status? value = Status.Init;

            // Act
            var result = EnumHelper.GetEnumValueString(value);

            // Assert
            Assert.AreEqual("0", result);
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumValueString_ReturnsNumericValue_LongEnum()
        {
            // Arrange
            BigStatus? value = BigStatus.VeryLarge;

            // Act
            var result = EnumHelper.GetEnumValueString(value);

            // Assert
            Assert.AreEqual(((long)BigStatus.VeryLarge).ToString(), result);
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumValueString_ReturnsNumericValue_NegativeLongEnum()
        {
            // Arrange
            BigStatus? value = BigStatus.VeryNegative;

            // Act
            var result = EnumHelper.GetEnumValueString(value);

            // Assert
            Assert.AreEqual(((long)BigStatus.VeryNegative).ToString(), result);
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumValueString_Null_ReturnsEmptyString_Value()
        {
            // Arrange
            Status? value = null;

            // Act
            var result = EnumHelper.GetEnumValueString(value);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumValueInt_ReturnsUnderlyingInt_WhenWithinRange()
        {
            // Arrange
            Status? value = Status.Processing;

            // Act
            var result = EnumHelper.GetEnumValueInt(value);

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumValueInt_Null_ReturnsNegativeOne()
        {
            // Arrange
            Status? value = null;

            // Act
            var result = EnumHelper.GetEnumValueInt(value);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumValueInt_NotThrowsOverflowException_WhenValueExceedsIntRange_Positive()
        {
            // Arrange
            BigStatus? value = BigStatus.VeryLarge;

            // Act + Assert
            Assert.DoesNotThrow(() => EnumHelper.GetEnumValueInt(value));
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
        public void GetEnumValueInt_NotThrowsOverflowException_WhenValueExceedsIntRange_Negative()
        {
            // Arrange
            BigStatus? value = BigStatus.VeryNegative;

            // Act + Assert
            Assert.DoesNotThrow(() => EnumHelper.GetEnumValueInt(value));
        }

        [Test, Timeout(TestConstants.TIMEOUT_BEST)]
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
