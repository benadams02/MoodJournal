namespace MoodJournal
{
    public partial class Journal : Models.Object
    {
        private Guid _User_UserID;

        [MoodJournal.Attributes.SqlColumn("User_UserID", true)]
        public Guid User_UserID
        {
            get { return _User_UserID; }
            set
            {
                _User_UserID = value;
                User = MoodJournal.User.Get(User_UserID);
            }
        }
        public MoodJournal.User User { get; set; }

        [MoodJournal.Attributes.SqlColumn("Mood", true)]
        public string? Mood { get; set; }
    }
}
