using System.ComponentModel;

namespace TestEnumZone.Application.Enums
{
    public enum SortBy
    {
        [Description("Sort List<User> by User.LastName (Ascending)")]
        LastName = 1,
        [Description("Sort List<User> by User.LastName (Descending)")]
        LastNameDesc = 2,
        [Description("Sort List<User> by User.FirstName (Ascending)")]
        FirstName = 3,
        [Description("Sort List<User> by User.FirstName (Descending)")]
        FirstNameDesc = 4
    }
}
