using System.ComponentModel;
using System.Reflection;

namespace MoodJournal.Models
{
    public class Object : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        [MoodJournal.Attributes.SqlColumn("DateCreated", true)]
        public DateTime DateCreated { get; set; }

        [MoodJournal.Attributes.SqlColumn("DateModified", true)]
        public DateTime DateModified { get; set; }
        private readonly Guid _ID;

        [MoodJournal.Attributes.SqlColumn("ID", true)]
        public Guid? ID
        {
            get { return _ID; }
        }

        public Object()
        {
            if (_ID == null || _ID == Guid.Empty) this._ID = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
        }

        public bool Save(Type type)
        {
            return true;
        }

        public static object Get(Type type, Guid thisID)
        {
            Type thisType = type.GetType();
            Attributes.SqlTable attr = GetSqlTableAttr(type);

            if (attr != null)
            {
                string query = $"EXEC {attr.GetSP} '{thisID}'";

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query,Server.DataSource.Connection);
                Server.DataSource.Read(cmd);
            }
            else
            {
                return null;
            }
            
        }

        private Attributes.SqlTable GetSqlTableAttr(Type type)
        {
            var attr = type.GetCustomAttributes(typeof(Attributes.SqlTable), true).FirstOrDefault() as Attributes.SqlTable;
            if (attr != null)
            {
                return attr;
            }
            else
            {
                return null;
            }
        }

        private List<string> GetSqlColumns(Type type)
        {
            List<string> SqlColumns = new List<string>();
            var props = type.GetProperties(BindingFlags.Public);

            foreach (var prop in props)
            {
                var attr = (prop.GetCustomAttribute(typeof(Attributes.SqlColumn), true) as Attributes.SqlColumn);
                if (attr != null) SqlColumns.Add(attr.FieldName);

            }
            return SqlColumns;
        }

        public bool Delete()
        {
            return true;
        }
    }
}
