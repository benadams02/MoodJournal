using System.Data;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace MoodJournal
{
    [MoodJournal.Attributes.SqlTable("User","spGetUser", "spInsertUser", "spUpdateUser","spDeleteUser")]
    public partial class User
    {
        public User()
        { 
            
        }

        public MoodJournal.Journal AddJournal()
        { 
            MoodJournal.Journal journal = new MoodJournal.Journal(this);
            journal.Save(true);
            return journal;
        }

        public static MoodJournal.User LoggedInUser()
        {
            ClaimsPrincipal thisPrincipal = (ClaimsPrincipal)(Server.HttpContext.HttpContext.User);
            if (thisPrincipal != null)
            {
                var thisID = Guid.Parse(thisPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (thisID == null)
                {
                    return null;
                }
                else
                {
                    return new MoodJournal.User().Get(thisID);
                }
            }
            else
            {
                return null;
            }
        }

        public static MoodJournal.User GetByUsername(string UserName)
        {
            Type thisType = typeof(MoodJournal.User);
            Dictionary<Attributes.SqlColumn, PropertyInfo> SqlColumns = GetSqlProperties(thisType);
            Attributes.SqlTable attr = GetSqlTableAttr(thisType);

            if (attr != null)
            {
                var cmd = Server.DataSource.StoredProcedure("spGetUserByUserName");
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserName", UserName));
                System.Data.DataTable data = Server.DataSource.Read(cmd);

                if (data != null && data.Rows.Count > 0)
                {
                    MoodJournal.User user = new MoodJournal.User();
                    var row = data.Rows[0];

                    foreach (KeyValuePair<Attributes.SqlColumn, PropertyInfo> kvp in SqlColumns)
                    {
                        var prop = kvp.Value;
                        var column = kvp.Key;
                        if (row[column.FieldName] != DBNull.Value)
                            prop.SetValue(user, row[column.FieldName]);

                    }
                    return user;
                }

                return null;
            }
            else
            {
                return null;
            }

        }

        public static List<MoodJournal.User> GetAllUsers()
        {
            return MoodJournal.User.Get();
        }
    }
}
