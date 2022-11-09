namespace MoodJournal
{
    [MoodJournal.Attributes.SqlTable("User","spGetUser", "spInsertUser", "spUpdateUser","spDeleteUser")]
    public partial class User
    {
        public User()
        { 
            
        }

        

        public static List<MoodJournal.User> GetAllUsers()
        {
            return MoodJournal.User.Get();
        }
    }
}
