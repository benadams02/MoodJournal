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
            this.User_UserID = user.ID.Value;
        }

        public bool Save()
        {
            return base.Save(this.GetType());
        }
    }
}
