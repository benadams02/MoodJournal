namespace MoodJournal
{
    [MoodJournal.Attributes.SqlTable("User","spGetUser", "spInsertUser", "spUpdateUser","spDeleteUser")]
    public partial class User
    {
        public User()
        { 
            
        }

        public static User Get(Guid ID)
        {
            if (ID != null)
            {
                User user = new User();
                user = (MoodJournal.User)Models.Object.Get(typeof(MoodJournal.User), ID);
                //Logic
                return user;
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            return base.Save(this.GetType());
        }
    }
}
