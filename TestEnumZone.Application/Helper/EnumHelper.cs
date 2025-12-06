
using System.ComponentModel;
using System.Reflection;

namespace TestEnumZone.Application.Helper
{
    public static class EnumHelper
    {
        /// <summary>
        /// Function to get <c>Enum description</c> with datatype: <c>string</c>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Enum description string</returns>
        public static string GetEnumDescription(Enum value)
        {
            if (value == null)
            {
                Console.Error.WriteLine("EnumHelper.GetEnumDescription Invalid params!");
                return string.Empty;
            }
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);
            if (attributes != null && attributes.Any())
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }
        /// <summary>
        /// Function to get <c>Enum name</c> with datatype: <c>string</c>
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns>Enum name string</returns>
        public static string GetEnumString<TEnum>(TEnum? value)
            where TEnum : struct, Enum
        {
            if (value == null)
            {
                Console.Error.WriteLine("EnumHelper.GetEnumValueString Invalid params!");
                return string.Empty;
            }

            return value.ToString() ?? string.Empty;
        }
        /// <summary>
        /// Function to get <c>Enum value</c> with datatype: <c>int</c>
        /// <para></para>
        /// As current enum switch convention is Positive integer, so I think <c>Negative integer return</c> should be resonable for exceptional case.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns>Enum value parsed to int</returns>
        public static int GetEnumValueInt<TEnum>(TEnum? value)
            where TEnum : struct, Enum
        {
            if (value == null)
            {
                Console.WriteLine("EnumHelper.GetEnumValueInt Invalid params!");
                return GlobalConstants.ENUM_FATAL; // Return Negative value for invalid case 
            }
            try
            {
                long raw = Convert.ToInt64(value.Value);
                if (raw < int.MinValue || raw > int.MaxValue)
                {
                    Console.Error.WriteLine("GetEnumDescription.GetEnumInt Passed enum exceeds int range! - Enum: {0}, value {1} ", value, value.Value);
                    return GlobalConstants.ENUM_FATAL;
                }

                return (int)raw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("GetEnumDescription.GetEnumInt Unhandled exception: - Enum: {0}", value);
                Console.Error.WriteLine(ex);

                return GlobalConstants.ENUM_FATAL;
            }
        }
    }
}
