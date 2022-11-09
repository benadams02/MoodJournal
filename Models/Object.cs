using System.ComponentModel;
using System.Reflection;

namespace MoodJournal.Models
{
    public class Object<T> : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        private Attributes.SqlTable SqlTableAttr { get; set; }
        private List<PropertyInfo> _editableProperties { get; set; }

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        [MoodJournal.Attributes.SqlColumn("DateCreated", true)]
        public DateTime? DateCreated { get; set; }

        [MoodJournal.Attributes.SqlColumn("DateModified", true)]
        public DateTime? DateModified { get; set; }
        private Guid _ID;

        [MoodJournal.Attributes.SqlColumn("ID", true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Object()
        {
            if (_ID == null || _ID == Guid.Empty) this._ID = Guid.NewGuid();
            if(DateCreated == null) DateCreated = DateTime.UtcNow;
            if(DateModified == null)DateModified = DateCreated;
            SqlTableAttr = GetSqlTableAttr(typeof(T));
            _editableProperties = GetSqlProperties(typeof(T)).Values.ToList();
        }

        public void UpdateFromObject(T ObjectIn)
        {
            foreach (PropertyInfo prop in _editableProperties)
            {
                prop.SetValue(this, prop.GetValue(ObjectIn));
            }
        }

        public static T GetItem(Guid ID)
        {
            if (ID != null && ID != Guid.Empty)
            {
                return new Object<T>().Get(ID);

            }
            else
            {
                return default(T);
            }
        }

        #region SQL

        public static List<T> Get()
        {
            List<T> objects = new List<T>();
            Type thisType = typeof(T);
            Attributes.SqlTable attr = GetSqlTableAttr(thisType);
            Dictionary<string, PropertyInfo> SqlColumns = GetSqlProperties(thisType);

            if (attr != null)
            {
                string query = $"EXEC {attr.GetSP} null";

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, Server.DataSource.Connection);
                System.Data.DataTable data = Server.DataSource.Read(cmd);

                if (data != null && data.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in data.Rows)
                    {
                        var obj = Activator.CreateInstance(thisType);

                        foreach (KeyValuePair<string, PropertyInfo> kvp in SqlColumns)
                        {
                            var prop = kvp.Value;
                            var column = kvp.Key;
                            if (row[column] != DBNull.Value)
                                prop.SetValue(obj, row[column]);

                        }
                        if (obj != null)
                            objects.Add((T)obj);
                    }
                    return objects;
                }

                return null;
            }
            else
            {
                return null;
            }
        }
        public T Get(Guid thisID)
        {
            Type thisType = typeof(T);
            Dictionary<string, PropertyInfo> SqlColumns = GetSqlProperties(thisType);

            if (SqlTableAttr != null)
            {
                string query = $"EXEC {SqlTableAttr.GetSP} '{thisID}'";

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, Server.DataSource.Connection);
                System.Data.DataTable data = Server.DataSource.Read(cmd);

                if (data != null && data.Rows.Count > 0)
                {
                    var obj = Activator.CreateInstance(thisType);
                    var row = data.Rows[0];

                    foreach (KeyValuePair<string, PropertyInfo> kvp in SqlColumns)
                    {
                        var prop = kvp.Value;
                        var column = kvp.Key;
                        if (row[column] != DBNull.Value)
                            prop.SetValue(obj, row[column]);

                    }
                    return (T)obj;
                }

                return default(T);
            }
            else
            {
                return default(T);
            }

        }

        public bool Delete()
        {
            if (SqlTableAttr != null)
            {
                string query = $"EXEC {SqlTableAttr.DeleteSP} '{ID}'";

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, Server.DataSource.Connection);
                return Server.DataSource.ExecuteQuery(cmd);
            }
            else
            {
                return false;
            }
        }

        public bool Save(bool IsNew = false)
        {
            if (SqlTableAttr != null)
            {
                if (IsNew)
                {
                    string query = $"EXEC {SqlTableAttr.InsertSP} ";
                }
                else
                {
                    string query = $"EXEC {SqlTableAttr.UpdateSP} ";
                }

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, Server.DataSource.Connection);
                return Server.DataSource.ExecuteQuery(cmd);
            }
            else
            {
                return false;
            }
        }


        private static Attributes.SqlTable GetSqlTableAttr(Type type)
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

        private static Dictionary<string, PropertyInfo> GetSqlProperties(Type type)
        {
            Dictionary<string, PropertyInfo> SqlColumns = new Dictionary<string, PropertyInfo>();
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                var attr = (prop.GetCustomAttribute(typeof(Attributes.SqlColumn), true) as Attributes.SqlColumn);
                if (attr != null) SqlColumns.Add(attr.FieldName, prop);

            }
            return SqlColumns;
        }

        #endregion
    }
}
