using System;
using voidsoft.DataBlock;



namespace voidsoft.DataModels
{

    /// <summary>
    /// 
    /// </summary>
    public class ResourceOperations
    {

        #region fields
        private DatabaseServer server;
        private string connectionString;
        private Resources res;
        private PersistentObject persistent;

        #endregion

        #region ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOperations"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="res">The res.</param>
        /// <param name="persistentObject">The persistent object.</param>
        internal ResourceOperations(DatabaseServer server, string connectionString, Resources res, PersistentObject persistentObject)
        {
            this.server = server;
            this.connectionString = connectionString;
            this.res = res;
            this.persistent = persistentObject;
        } 
        #endregion

        #region create
        /// <summary>
        /// Creates the new resource.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="resourceValue">The resource value.</param>
        /// <param name="cultureInfo">The culture info.</param>
        public void CreateNewResource(string className, string resourceKey, string resourceValue, string cultureInfo)
        {
            this.res.ClassName = className;
            this.res.ResourceValue = resourceValue;
            this.res.ResourceKey = resourceKey;
            this.res.CultureInfo = cultureInfo;

            this.persistent.Create(this.res);
        }



        /// <summary>
        /// Clones the culture info.
        /// </summary>
        /// <param name="cultureInfoToClone">The culture info to clone.</param>
        /// <param name="newCultureInfoName">New name of the culture info.</param>
        public void CloneCultureInfo(string cultureInfoToClone, string newCultureInfoName)
        {
            Session session = null;

            try
            {
                QueryCriteria qc = new QueryCriteria(res);
                qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.CultureInfo], cultureInfoToClone);

                Resources[] resourceses = (Resources[]) this.persistent.GetTableMetadata(qc);

                session = Session.CreateNewSession(this.server, this.connectionString);
                
                session.BeginTransaction();

                PersistentObject pps = new PersistentObject(session, this.res);

                foreach (Resources resources in resourceses)
                {
                    this.res.ClassName = resources.ClassName;
                    this.res.CultureInfo = newCultureInfoName;
                    this.res.ResourceKey = resources.ResourceKey;
                    this.res.ResourceValue = resources.ResourceValue;

                    this.res.ResourceId = null;

                    pps.Create(this.res);
                }

                session.Commit();
            }
            catch (Exception ex)
            {
                if (session != null)
                {
                    session.Rollback();
                }

                throw;
            }
            finally
            {
                if (session != null)
                {
                    session.Dispose();
                }
            }
        }
        
        #endregion
        
        #region delete

        /// <summary>
        /// Deletes the culture info.
        /// </summary>
        /// <param name="cultureInfo">The culture info.</param>
        public void DeleteCultureInfo(string cultureInfo)
        {
            this.res.CultureInfo = cultureInfo;

            QueryCriteria qc = new QueryCriteria(this.res);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.CultureInfo], cultureInfo);

            this.persistent.Delete(qc);
        }


        /// <summary>
        /// Deletes the class.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="cultureInfo">The culture info.</param>
        public void DeleteClass(string className, string cultureInfo)
        {
            QueryCriteria qc = new QueryCriteria(this.res);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.ClassName], className);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.CultureInfo], cultureInfo);

            this.persistent.Delete(qc);
        }


        /// <summary>
        /// Deletes the resource key.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="cultureInfo">The culture info.</param>
        public void DeleteResourceKey(string className, string resourceKey, string cultureInfo)
        {
            QueryCriteria qc = new QueryCriteria(this.res);

            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.ClassName], className);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.CultureInfo], cultureInfo);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.ResourceKey], resourceKey);

            this.persistent.Delete(qc);
        }

        #endregion

        #region update
        
        /// <summary>
        /// Edits the culture info.
        /// </summary>
        /// <param name="oldCultureInfo">The old culture info.</param>
        /// <param name="newcultureInfo">The newculture info.</param>
        public void EditCultureInfo(string oldCultureInfo, string newcultureInfo)
        {
            this.res.CultureInfo = newcultureInfo;

            QueryCriteria qc = new QueryCriteria(this.res.TableName, res[Resources.ResourcesFields.CultureInfo]);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.CultureInfo], oldCultureInfo);

            this.persistent.Update(qc);
        }

        /// <summary>
        /// Updates the class.
        /// </summary>
        /// <param name="newClassName">New name of the class.</param>
        /// <param name="oldClassName">Old name of the class.</param>
        /// <param name="cultureInfo">The culture info.</param>
        public void UpdateClass(string newClassName, string oldClassName, string cultureInfo)
        {
            this.res.ClassName = newClassName;
            this.res.CultureInfo = cultureInfo;

            QueryCriteria qc = new QueryCriteria(this.res.TableName, this.res[Resources.ResourcesFields.ClassName]);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.ClassName], oldClassName);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.CultureInfo], cultureInfo);

            this.persistent.Update(qc);
        }



        /// <summary>
        /// Updates the resource.
        /// </summary>
        /// <param name="cultureInfo">The culture info</param>
        /// <param name="className">Name of the class</param>
        /// <param name="newResourceKey">The new resource key</param>
        /// <param name="oldResourceKey">The old resource key</param>
        /// <param name="newResourceValue">The new resource value</param>
        /// <param name="oldResourceValue">The old resource value</param>
        public void UpdateResource(string cultureInfo, string className, string newResourceKey, string oldResourceKey,
                                   string newResourceValue, string oldResourceValue)
        {
            this.res.ClassName = className;
            this.res.CultureInfo = cultureInfo;
            this.res.ResourceKey = newResourceKey;
            this.res.ResourceValue = newResourceValue;

            QueryCriteria qc = new QueryCriteria(this.res.TableName, this.res[Resources.ResourcesFields.ResourceKey], this.res[Resources.ResourcesFields.ResourceValue]);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.ResourceKey], oldResourceKey);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.ResourceValue], oldResourceValue);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.CultureInfo], cultureInfo);
            qc.Add(CriteriaOperator.Equality, res[Resources.ResourcesFields.ClassName], className);
            

            this.persistent.Update(qc);
        } 
        #endregion

    }
}