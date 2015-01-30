using System;
using System.Data;
using voidsoft.DataBlock;

namespace voidsoft.DataModels
{
    /// <summary>
    /// NameValue implementation. Usually you inherit from this.
    /// </summary>
    public class NameValue
    {


        #region fields

        private const int KEY_INDEX = 1;
        
        private const int VALUE_INDEX = 2;


        private string tableName;

        private DataTable table;

        protected PersistentObject perst = null;

        protected KeyValue keyValue;

        private DatabaseServer server;

        private string connectionString;

        private bool createIfNotExists = true;

        private bool dontThrowException = true;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NameValue"/> class
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="tableName">Name of the table.</param>
        public NameValue(DatabaseServer server, string connectionString, string tableName)
        {
            keyValue = new KeyValue();
            keyValue.TableName = tableName;
            perst = new PersistentObject(server, connectionString, keyValue);
            this.tableName = tableName;
            this.server = server;
            this.connectionString = connectionString;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NameValue"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        public NameValue(string tableName)
        {
            keyValue = new KeyValue();
            keyValue.TableName = tableName;
            this.tableName = tableName;
        }

        #endregion

        #region public implementation

        /// <summary>
        /// Gets the <see cref="System.String"/> with the specified keyValue
        /// </summary>
        /// <value></value>
        public string this[string key]
        {
            get
            {
                return QueryData(key);
            }
            set
            {
                SetValue(key, value);
            }
        }

        /// <summary>
        /// Gets or sets wheter a key/value pair is created on SetValue if it does not exists
        /// </summary>
        /// <value><c>true</c> if [create if not exists]; otherwise, <c>false</c>.</value>
        public bool CreateIfNotExists
        {
            get
            {
                return createIfNotExists;
            }
            set
            {
                createIfNotExists = value;
            }
        }


        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public DatabaseServer Server
        {
            get
            {
                return server;
            }
            set
            {
                server = value;
                perst = new PersistentObject(Server, ConnectionString, keyValue);
            }
        }


        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
                perst = new PersistentObject(Server, ConnectionString, keyValue);
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether a exception is not thrown when a empty string 
        /// </summary>
        /// <value><c>true</c> if [dont throw exception]; otherwise, <c>false</c>.</value>
        public bool DontThrowException
        {
            get
            {
                return dontThrowException;
            }
            set
            {
                dontThrowException = value;
            }
        }

        /// <summary>
        /// Gets the value
        /// </summary>
        /// <param name="key">The keyValue.</param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            return QueryData(key);
        }


        /// <summary>
        /// Loads all the  data
        /// </summary>
        public DataTable PreloadData()
        {
            table = perst.GetDataTable();
            return this.table;
        }


        /// <summary>
        /// Edits the value.
        /// </summary>
        /// <param name="oldKey">The old key.</param>
        /// <param name="newKey">The new key.</param>
        /// <param name="value">The value.</param>
        public void Edit(string oldKey, string newKey, string value)
        {
            keyValue.EntityName = newKey;
            keyValue.EntityValue = value;

            QueryCriteria qc = new QueryCriteria(keyValue.TableName, keyValue[KeyValue.KeyValueFields.EntityValue], keyValue[KeyValue.KeyValueFields.EntityName]);
            qc.Add(CriteriaOperator.Equality, keyValue[KeyValue.KeyValueFields.EntityName], oldKey);

            perst.Update(qc);

        }


        /// <summary>
        /// Updates the value
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetValue(string key, string value)
        {
            keyValue.EntityValue = value;

            if (CreateIfNotExists)
            {
                keyValue.EntityName = key;

                //check if the key/value pair already exists in the database
                bool isUnique = perst.IsUnique(keyValue[KeyValue.KeyValueFields.EntityName], key);

                if (isUnique)
                {
                    Create(key, value);
                    return;
                }
            }

            //the key/value pair exists so just update it.
            QueryCriteria qc = new QueryCriteria(keyValue.TableName, keyValue[KeyValue.KeyValueFields.EntityValue]);
            qc.Add(CriteriaOperator.Equality, keyValue[KeyValue.KeyValueFields.EntityName], key);
            perst.Update(qc);

            //if everything is ok also update the internal cache
            UpdateCache(key, value);
        }


        /// <summary>
        /// Invalidates the cache.
        /// </summary>
        public void InvalidateCache()
        {
            if (table != null)
            {
                table.Clear();
                table = null;
            }
        }


        /// <summary>
        /// Creates the specified key
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void Create(string key, string value)
        {
            KeyValue kv = new KeyValue();
            kv.EntityName = key;
            kv.EntityValue = value;
            kv.TableName = keyValue.TableName;

            perst.Create(kv);
        }


        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Delete(string name)
        {
            KeyValue kv = new KeyValue();
            QueryCriteria qc = new QueryCriteria(this.tableName);
            qc.Add(CriteriaOperator.Equality, keyValue[KeyValue.KeyValueFields.EntityName], name);

            perst.Delete(qc);
        }

        #endregion

        #region internal implementation

        /// <summary>
        /// Queries the data from either in memory cache (DataTable) or the database
        /// </summary>
        /// <param name="key">The keyValue.</param>
        /// <returns></returns>
        private string QueryData(string key)
        {

            try
            {
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        try
                        {
                            if (row[KEY_INDEX].ToString() == key)
                            {
                                return row[VALUE_INDEX].ToString();
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            catch
            {
                //ignore exception and go to database
            }


			
            //no preloading so get it from the database
            QueryCriteria qc = new QueryCriteria(keyValue.TableName, keyValue[KeyValue.KeyValueFields.EntityValue]);
            qc.Add(CriteriaOperator.Equality, keyValue[KeyValue.KeyValueFields.EntityName], key);

            object result = perst.GetValue(qc);

            if (result == null || result == DBNull.Value)
            {
                if (DontThrowException)
                {
                    return string.Empty;
                }

                throw new ArgumentException();
            }

            return result.ToString();
        }


        /// <summary>
        /// Updates the internal cache.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        private void UpdateCache(string key, string value)
        {
            if (table != null)
            {
                foreach (DataRow dr in table.Rows)
                {
                    if (dr["EntityName"].ToString() == key)
                    {
                        dr["EntityValue"] = value;
                        return;
                    }
                }
            }
        }

        #endregion
    }
}