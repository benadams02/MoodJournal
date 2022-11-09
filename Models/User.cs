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
            List<MoodJournal.User> users = MoodJournal.User.Get();

            return users;
        }
    }
}
