using System;
using System.Data;
namespace MoodJournal
{
    public partial class User : Models.Object<User>
    {
        [MoodJournal.Attributes.SqlColumn("UserName",true, SqlDbType.VarChar)]
        public string? UserName { get; set; }

        [MoodJournal.Attributes.SqlColumn("FirstName", true, SqlDbType.VarChar)]
        public string? FirstName { get; set; }

        [MoodJournal.Attributes.SqlColumn("LastName", true, SqlDbType.VarChar)]
        public string? LastName { get; set; }

        [MoodJournal.Attributes.SqlColumn("Password", true, SqlDbType.VarChar)]
        public string? Password { get; set; }

        [MoodJournal.Attributes.SqlColumn("Email", true, SqlDbType.VarChar)]
        public string? Email { get; set; }

        [MoodJournal.Attributes.SqlColumn("Gender", true, SqlDbType.VarChar)]
        public string? Gender { get; set; }
    }

    public struct GenderTypeValues
    {
        public const string Male = "Male";
        public const string Female = "Female";
        public const string Other = "Other";
    }


    
}
