using System.ComponentModel;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace MoodJournal.Models
{
    public class Object<T> : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        private Attributes.SqlTable SqlTableAttr { get; set; }
        private List<PropertyInfo> _editableProperties { get; set; }

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        [MoodJournal.Attributes.SqlColumn("DateCreated", true, SqlDbType.DateTime)]
        public DateTime? DateCreated { get; set; }

        [MoodJournal.Attributes.SqlColumn("DateModified", true, SqlDbType.DateTime)]
        public DateTime? DateModified { get; set; }
        private Guid _ID;

        [MoodJournal.Attributes.SqlColumn("ID", true, SqlDbType.UniqueIdentifier)]
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
            Dictionary<Attributes.SqlColumn, PropertyInfo> SqlColumns = GetSqlProperties(thisType);

            if (attr != null)
            {
                SqlCommand cmd = new SqlCommand(attr.GetSP, Server.DataSource.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", DBNull.Value));
                System.Data.DataTable data = Server.DataSource.Read(cmd);

                if (data != null && data.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in data.Rows)
                    {
                        var obj = Activator.CreateInstance(thisType);

                        foreach (KeyValuePair<Attributes.SqlColumn, PropertyInfo> kvp in SqlColumns)
                        {
                            var prop = kvp.Value;
                            var column = kvp.Key;
                            if (row[column.FieldName] != DBNull.Value)
                                prop.SetValue(obj, row[column.FieldName]);

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
            Dictionary<Attributes.SqlColumn, PropertyInfo> SqlColumns = GetSqlProperties(thisType);

            if (SqlTableAttr != null)
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(SqlTableAttr.GetSP, Server.DataSource.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", thisID));
                System.Data.DataTable data = Server.DataSource.Read(cmd);

                if (data != null && data.Rows.Count > 0)
                {
                    var obj = Activator.CreateInstance(thisType);
                    var row = data.Rows[0];

                    foreach (KeyValuePair<Attributes.SqlColumn, PropertyInfo> kvp in SqlColumns)
                    {
                        var prop = kvp.Value;
                        var column = kvp.Key;
                        if (row[column.FieldName] != DBNull.Value)
                            prop.SetValue(obj, row[column.FieldName]);

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
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(SqlTableAttr.DeleteSP, Server.DataSource.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", ID));
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
                string query = "";
                if (IsNew)
                {
                    query = $"{SqlTableAttr.InsertSP}";
                }
                else
                {
                    query = $"{SqlTableAttr.UpdateSP}";
                }

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, Server.DataSource.Connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (KeyValuePair<Attributes.SqlColumn, PropertyInfo> item in GetSqlProperties(typeof(T)))
                {
                    var value = item.Value.GetValue(this);
                    if (value == null) value = DBNull.Value;
                    var param = new System.Data.SqlClient.SqlParameter($"@{item.Key.FieldName}", value);
                    param.SqlDbType = item.Key.SqlDbType;
                    cmd.Parameters.Add(param);
                }
                
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

        private static Dictionary<Attributes.SqlColumn, PropertyInfo> GetSqlProperties(Type type)
        {
            Dictionary<Attributes.SqlColumn, PropertyInfo> SqlColumns = new Dictionary<Attributes.SqlColumn, PropertyInfo>();
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                var attr = (prop.GetCustomAttribute(typeof(Attributes.SqlColumn), true) as Attributes.SqlColumn);
                if (attr != null) SqlColumns.Add(attr, prop);

            }
            return SqlColumns;
        }

        #endregion
    }
}
