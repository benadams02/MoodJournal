namespace MoodJournal
{
    public class Attributes : Attribute
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class SqlColumn : Attribute
        {
            private bool inSql;
            private string fieldName;

            public SqlColumn(string FieldName, bool InSql)
            { 
                this.FieldName = FieldName;
                this.InSql = InSql;
            }

            public bool InSql
            {
                get { return inSql; }
                set { inSql = value; }
            }

            public string FieldName
            {
                get { return fieldName; }
                set { fieldName = value; }
            }
        }

        [AttributeUsage(AttributeTargets.Class)]
        public class SqlTable : Attribute
        {
            private string tableName;
            private string getSP;
            private string insertSP;
            private string deleteSP;
            private string updateSP;
            public SqlTable(string TableName, string GetSP, string InsertSP, string UpdateSP, string DeleteSP)
            {
                this.TableName = TableName;
                this.GetSP = GetSP;
                this.InsertSP = InsertSP;
                this.UpdateSP = UpdateSP;
                this.DeleteSP = DeleteSP;
            }

            public string TableName
            {
                get { return tableName; }
                set { tableName = value; }
            }

            public string GetSP
            {
                get { return getSP; }
                set { getSP = value; }
            }
            public string InsertSP
            {
                get { return insertSP; }
                set { insertSP = value; }
            }
            public string UpdateSP
            {
                get { return updateSP; }
                set { updateSP = value; }
            }
            public string DeleteSP
            {
                get { return deleteSP; }
                set { deleteSP = value; }
            }
        }

    }
}
