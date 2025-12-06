
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

            return string.Empty;
        }
        /// <summary>
        /// Function to get <c>Enum name</c> with datatype: <c>string</c>
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns>Enum name string</returns>
        public static string GetEnumName<TEnum>(TEnum? value)
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
        /// Function to get <c>Enum value</c> as <c>string</c> without returning the enum name.
        /// Example: for an enum member whose numeric value is 1 this returns "1".
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns>Underlying enum value as string, or empty string on error</returns>
        public static string GetEnumValueString<TEnum>(TEnum? value)
            where TEnum : struct, Enum
        {
            if (value == null)
            {
                Console.Error.WriteLine("EnumHelper.GetEnumValueString Invalid params!");
                return string.Empty;
            }
            try
            {
                return Convert.ChangeType(value.Value, Enum.GetUnderlyingType(typeof(TEnum))).ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("EnumHelper.GetEnumValueString Unhandled exception: - Enum: {0}", value);
                Console.Error.WriteLine(ex);
                return string.Empty;
            }
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
                Console.Error.WriteLine("EnumHelper.GetEnumValueInt Invalid params!");
                return GlobalConstants.ENUM_FATAL;
            }
            try
            {
                // Determine underlying integral type to preserve signed/unsigned representation
                Type underlying = Enum.GetUnderlyingType(typeof(TEnum));
                switch (Type.GetTypeCode(underlying))
                {
                    case TypeCode.SByte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    {
                        return (int)Convert.ToInt64(value.Value);
                    }
                    case TypeCode.Byte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    {
                        return (int)Convert.ToUInt64(value.Value);
                    }
                    default:
                    {
                        // Fallback to unsigned 64-bit representation
                        return (int)Convert.ToUInt64(value.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("EnumHelper.GetEnumValueInt Unhandled exception: - Enum: {0}", value);
                Console.Error.WriteLine(ex);
                return GlobalConstants.ENUM_FATAL;
            }
        }
    }
}
