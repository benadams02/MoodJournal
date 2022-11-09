namespace MoodJournal
{
    public partial class User : Models.Object<User>
    {
        [MoodJournal.Attributes.SqlColumn("UserName",true)]
        public string? UserName { get; set; }

        [MoodJournal.Attributes.SqlColumn("FirstName", true)]
        public string? FirstName { get; set; }

        [MoodJournal.Attributes.SqlColumn("LastName", true)]
        public string? LastName { get; set; }

        [MoodJournal.Attributes.SqlColumn("Password", true)]
        public string? Password { get; set; }

        [MoodJournal.Attributes.SqlColumn("Email", true)]
        public string? Email { get; set; }

        [MoodJournal.Attributes.SqlColumn("Gender", true)]
        public string? Gender { get; set; }
    }

    public struct GenderTypeValues
    {
        public const string Male = "Male";
        public const string Female = "Female";
        public const string Other = "Other";
    }


    
}
