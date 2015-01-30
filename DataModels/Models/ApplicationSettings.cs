using System;
using System.Data;
using voidsoft.DataBlock;

namespace voidsoft.DataModels
{

    [Serializable()]
    public class ApplicationSettings : TableMetadata
    {

        public enum SettingsFields
        {
            SettingId,
            Name,
            DataType,
            SettingValue,
			ShowInUserInterface
        }


        private DatabaseField[] myFields;

        public ApplicationSettings()
        {
            myFields = new DatabaseField[5];
            myFields[0] = new DatabaseField(DbType.Int32, "SettingId", true, false, null);
            myFields[1] = new DatabaseField(DbType.String, "Name", false, false, null);
            myFields[2] = new DatabaseField(DbType.String, "DataType", false, false, null);
            myFields[3] = new DatabaseField(DbType.String, "SettingValue", false, false, null);
            myFields[4] = new DatabaseField(DbType.Boolean, "ShowInUserInterface", false, false, null);

            this.currentTableName = "dbo.ApplicationSettings";


        }


        public override DatabaseField[] TableFields
        {
            get { return myFields; }
            set { myFields = value; }
        }
        public ApplicationSettings Clone()
        {
            return this.Clone<ApplicationSettings>();
        }

        public System.Int32 SettingId
        {
            get
            {
                return (System.Int32)this.GetField("SettingId").fieldValue;
            }

            set
            {
                this.SetFieldValue("SettingId", value);
            }
        }


        public System.Boolean ShowInUserInterface
        {
            get
            {
                return Convert.ToBoolean(this.GetField("ShowInUserInterface").fieldValue);
            }

            set
            {
                this.SetFieldValue("ShowInUserInterface", value);
            }
        }


        public System.String Name
        {
            get
            {
                object result = this.GetField("Name").fieldValue;
                return (result != null) ? result.ToString() : null;
            }

            set
            {
                this.SetFieldValue("Name", value);
            }
        }



        public System.String DataType
        {
            get
            {
                object result = this.GetField("DataType").fieldValue;
                return (result != null) ? result.ToString() : null;
            }

            set
            {
                this.SetFieldValue("DataType", value);
            }
        }


        public System.String SettingValue
        {
            get
            {
                object result = this.GetField("SettingValue").fieldValue;
                return (result != null) ? result.ToString() : null;
            }

            set
            {
                this.SetFieldValue("SettingValue", value);
            }
        }

    }
}
