
using System.ComponentModel;
using System.Reflection;

namespace TestEnumZone.Application.Helper
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            if (value == null)
            {
                Console.WriteLine("EnumHelper.GetEnumDescription Invalid params!");
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
        public static string GetEnumString<TEnum>(TEnum? value)
            where TEnum : struct, Enum
        {
            if (value == null)
            {
                Console.WriteLine("EnumHelper.GetEnumValueString Invalid params!");
                return string.Empty;
            }

            return value.ToString();
        }

        public static int GetEnumValueInt<TEnum>(TEnum? value)
            where TEnum : struct, Enum
        {
            if (value == null)
            {
                Console.WriteLine("EnumHelper.GetEnumValueInt Invalid params!");
                return -1; // Return Negative value for invalid case 
            }
            int raw = Convert.ToInt32(value.Value);
            if (raw < int.MinValue || raw > int.MaxValue)
            {
                Console.WriteLine("GetEnumDescription.GetEnumInt Passed enum exceeds int range! - Enum: {0}, value {1} ", value, value.Value);
            }

            return (int)raw;
        }
    }
}
