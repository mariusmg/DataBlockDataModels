using System;
using System.Data;
using System.Globalization;
using voidsoft.DataBlock;

namespace voidsoft.DataModels
{
    /// <summary>
    /// Implements localization features
    /// </summary>
    public class Localization : IDisposable
    {
        #region fields

        private bool throwExceptionWhenNotFound = false;
        private string connectionString;
        private DatabaseServer databaseServer;
        private DataTable table = null;
        private PersistentObject persistentObject = null;
        private static Localization localization = null;
        private Resources res = null;
        private ResourceOperations operations = null;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets a value indicating whether [throw exception when not found].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [throw exception when not found]; otherwise, <c>false</c>.
        /// </value>
        public bool ThrowExceptionWhenNotFound
        {
            get
            {
                return this.throwExceptionWhenNotFound;
            }
            set
            {
                this.throwExceptionWhenNotFound = value;
            }
        }

        /// <summary>
        /// Gets or sets the operations.
        /// </summary>
        /// <value>The operations.</value>
        public ResourceOperations Operations
        {
            get
            {
                return operations;
            }
            set
            {
                operations = value;
            }
        }

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Localization"/> class
        /// </summary>
        private Localization()
        {
        }


        /// <summary>
        /// Initializes the localization.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connectionString">The connection string.</param>
        public Localization(DatabaseServer server, string connectionString)
        {
            this.res = new Resources();
            this.connectionString = connectionString;
            this.databaseServer = server;
            this.persistentObject = new PersistentObject(server, connectionString, this.res);
            this.operations = new ResourceOperations(server, connectionString, this.res, this.persistentObject);
        }
        #endregion

        #region public implementation

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns></returns>
        public DataTable GetResources(string className, string cultureInfo)
        {
            QueryCriteria qc = new QueryCriteria(this.res.TableName, res[Resources.ResourcesFields.ResourceKey], res[Resources.ResourcesFields.ResourceValue]);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.ClassName], className);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.CultureInfo], cultureInfo);

            return persistentObject.GetDataTable(qc);
        }

        /// <summary>
        /// Gets all the class names.
        /// </summary>
        /// <returns></returns>
        public string[] GetClassNames(string cultureInfo)
        {
            QueryCriteria qc = new QueryCriteria(this.res.TableName, res[Resources.ResourcesFields.ClassName]);
            qc.Add(CriteriaOperator.Distinct, res[Resources.ResourcesFields.ClassName]);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.CultureInfo], cultureInfo);

            DataTable resultTable = persistentObject.GetDataTable(qc);

            string[] result = new string[resultTable.Rows.Count];

            for (int i = 0; i < resultTable.Rows.Count; i++)
            {
                result[i] = resultTable.Rows[i][0].ToString();
            }

            return result;
        }

        /// <summary>
        /// Gets all the cultures
        /// </summary>
        /// <returns></returns>
        public string[] GetCultures()
        {
            QueryCriteria qc = new QueryCriteria(this.res.TableName, res[Resources.ResourcesFields.CultureInfo]);
            qc.Add(CriteriaOperator.Distinct, res[Resources.ResourcesFields.CultureInfo]);

            DataTable resultTable = persistentObject.GetDataTable(qc);

            string[] result = new string[resultTable.Rows.Count];

            for (int i = 0; i < resultTable.Rows.Count; i++)
            {
                result[i] = resultTable.Rows[i][0].ToString();
            }

            return result;
        }


        /// <summary>
        /// Clears the resources and forces reloading.
        /// </summary>
        public void Clear()
        {
            if (this.table != null)
            {
                this.table.Clear();
                this.table = null;
            }
        }


        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <param name="resourceClassName">Name of the resource class.</param>
        /// <param name="resourceKey">The resource keyValue.</param>
        /// <returns></returns>
        public string GetResourceValue(string resourceClassName, string resourceKey, string cultureInfoName)
        {
            try
            {
                return this.QueryData(resourceClassName, resourceKey, cultureInfoName);
            }
            catch (ArgumentException e)
            {
                if (this.ThrowExceptionWhenNotFound)
                {
                    throw;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <param name="resourceClassName">Name of the resource class.</param>
        /// <param name="resourceKey">The resource keyValue.</param>
        /// <returns></returns>
        public string GetResourceValue(string resourceClassName, string resourceKey, CultureInfo cultureInfo)
        {
            return this.GetResourceValue(resourceClassName, resourceKey, cultureInfo.Name);
        }

        /// <summary>
        /// Gets the <see cref="System.String"/> with the specified resource class name
        /// </summary>
        /// <value>Returns the value</value>
        public string this[string resourceClassName, string resourceKey]
        {
            get
            {
                return this.GetResourceValue(resourceClassName, resourceKey, CultureInfo.CurrentUICulture.Name);
            }
        }


        /// <summary>
        /// Loads all the resources
        /// </summary>
        public void LoadResourcesForCultureInfo(string cultureInfoName)
        {
            QueryCriteria qc = new QueryCriteria(this.res);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.CultureInfo], cultureInfoName);

            table = persistentObject.GetDataTable(qc);
        }

        #endregion


        #region internal implementation


        /// <summary>
        /// Queries the data.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="resourceKey">The resource keyValue.</param>
        /// <returns>The cached value</returns>
        internal string GetCachedData(string className, string resourceKey, string cultureInfo)
        {
            if (this.table != null)
            {
                foreach (DataRow var in this.table.Rows)
                {
                    if (var["ClassName"].ToString() == className && var["ResourceKey"].ToString() == resourceKey && var["CultureInfo"].ToString() == cultureInfo)
                    {
                        return var["ResourceValue"].ToString();
                    }
                }

                throw new ArgumentException();
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Queries the data.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="resourceKey">The resource keyValue.</param>
        /// <param name="cultureInfoName">Name of the culture info.</param>
        /// <returns></returns>
        internal string QueryData(string className, string resourceKey, string cultureInfoName)
        {
            object result = null;

            try
            {
                result = this.GetCachedData(className, resourceKey, cultureInfoName);
            }
            catch (ArgumentException aex)
            {
                //ignore and let it search into the database
            }
            catch
            {
                throw;
            }

            if (result != null)
            {
                return result.ToString();
            }


            QueryCriteria qc = new QueryCriteria(res.TableName, res[Resources.ResourcesFields.ResourceValue]);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.CultureInfo], cultureInfoName);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.ClassName], className);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.ResourceKey], resourceKey);

            result = persistentObject.GetValue(qc);

            if (result != null)
            {
                return result.ToString();
            }

            throw new ArgumentException();
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.table != null)
            {
                this.table.Dispose();
            }
        }

        #endregion
    }
}