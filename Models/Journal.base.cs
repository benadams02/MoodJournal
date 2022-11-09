using System;
using System.Data;
namespace MoodJournal
{
    public partial class Journal : Models.Object<Journal>
    {
        private Guid _User_UserID;

        [MoodJournal.Attributes.SqlColumn("User_UserID", true, SqlDbType.UniqueIdentifier)]
        public Guid User_UserID
        {
            get { return _User_UserID; }
            set
            {
                _User_UserID = value;
                User = (MoodJournal.User)new MoodJournal.User().Get(User_UserID);
            }
        }
        public MoodJournal.User User { get; set; }

        [MoodJournal.Attributes.SqlColumn("Mood", true, SqlDbType.VarChar)]
        public string? Mood { get; set; }
    }
}
