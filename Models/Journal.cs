namespace MoodJournal
{
    [MoodJournal.Attributes.SqlTable("Journal", "spGetJournal", "spInsertJournal", "spUpdateJournal", "spDeleteJournal")]
    public partial class Journal
    {
        public Journal()
        { 
        }

        public Journal(User user)
        {
            this.User_UserID = user.ID;
            this.Mood = JournalMood.Ok;
        }

        public struct JournalMood
        {
            public const string VeryHappy = "Very Happy";
            public const string Happy = "Happy";
            public const string Ok = "Ok";
            public const string NotGreat = "Not Great";
            public const string ReallyUnhappy = "Really Unhappy";
        }
    }
}
