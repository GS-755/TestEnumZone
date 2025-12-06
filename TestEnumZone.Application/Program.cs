using TestEnumZone.Application.Enums;
using TestEnumZone.Application.Helper;

namespace TestEnumZone.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enum 'SortBy.FirstName' info:");
            /* Run:) */
            string desc = EnumHelper.GetEnumDescription(SortBy.FirstName);
            string valueString = EnumHelper.GetEnumString<SortBy>(SortBy.FirstName);
            int valueInt = EnumHelper.GetEnumValueInt<SortBy>(SortBy.FirstName);
            /* Print result */
            Console.WriteLine("Description   : {0}", desc);
            Console.WriteLine("Value (String): {0}", valueString);
            Console.WriteLine("Value (Int32) : {0}", valueInt);
        }
    }
}
