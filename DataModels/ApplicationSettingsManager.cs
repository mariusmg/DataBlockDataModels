using voidsoft.DataBlock;

namespace voidsoft.DataModels
{
    public class ApplicationSettingsManager
    {
        private readonly PersistentObject perst;
        private readonly ApplicationSettings settings;


        public ApplicationSettingsManager(DatabaseServer server, string connectionString)
        {
            settings = new ApplicationSettings();

            perst = new PersistentObject(server, connectionString, new ApplicationSettings());
        }

        public ApplicationSettings GetApplicationSettings(int settingId)
        {
            return (ApplicationSettings)perst.GetTableMetadata(settingId);
        }

        public ApplicationSettings GetApplicationSettings(string settingName)
        {
            QueryCriteria qc = new QueryCriteria(settings);

            qc.Add(CriteriaOperator.Equality, settings[ApplicationSettings.SettingsFields.Name], settingName);

            ApplicationSettings[] sets = (ApplicationSettings[])perst.GetTableMetadata(qc);

            if (sets.Length > 0)
            {
                return sets[0];
            }

            return null;
        }

        public ApplicationSettings[] GetSettings()
        {

            ApplicationSettings app = new ApplicationSettings();

            QueryCriteria qc = new QueryCriteria(app);
            qc.Add(CriteriaOperator.OrderBy, app[ApplicationSettings.SettingsFields.Name], "asc");

            
            return (ApplicationSettings[])perst.GetTableMetadata(qc);
        }

        public void Update(ApplicationSettings set)
        {
            perst.Update(set);
        }


        public void Delete(int id)
        {
            perst.Delete(id);
        }

        public void Create(ApplicationSettings set)
        {
            perst.Create(set);
        }
    }
}