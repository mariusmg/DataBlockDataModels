using System;
using System.Data;
using voidsoft.DataBlock;

namespace voidsoft.DataModels
{

    [Serializable()]
    internal class Resources : TableMetadata
    {

        public enum ResourcesFields
        {
            ResourceId,
            ClassName,
            ResourceKey,
            ResourceValue,
            CultureInfo
        }


        private DatabaseField[] myFields;

        public Resources()
        {
            myFields = new DatabaseField[5];
            myFields[0] = new DatabaseField(DbType.Int32, "ResourceId", true, true, null);
            myFields[1] = new DatabaseField(DbType.String, "ClassName", false, false, null);
            myFields[2] = new DatabaseField(DbType.String, "ResourceKey", false, false, null);
            myFields[3] = new DatabaseField(DbType.String, "ResourceValue", false, false, null);
            myFields[4] = new DatabaseField(DbType.String, "CultureInfo", false, false, null);

            this.currentTableName = "Resources";


        }


        public override DatabaseField[] TableFields
        {
            get
            {
                return myFields;
            }
            set
            {
                myFields = value;
            }
        }
        public Resources Clone()
        {
            return this.Clone<Resources>();
        }

        public System.Int32? ResourceId
        {
            get
            {
                return (System.Int32?) (this.GetField("ResourceId")).fieldValue;
            }

            set
            {
                this.SetFieldValue("ResourceId", value);
            }
        }


        public System.String ClassName
        {
            get
            {
                return (System.String) (this.GetField("ClassName")).fieldValue;
            }

            set
            {
                this.SetFieldValue("ClassName", value);
            }
        }


        public System.String ResourceKey
        {
            get
            {
                return (System.String) (this.GetField("ResourceKey")).fieldValue;
            }

            set
            {
                this.SetFieldValue("ResourceKey", value);
            }
        }


        public System.String ResourceValue
        {
            get
            {
                return (System.String) (this.GetField("ResourceValue")).fieldValue;
            }

            set
            {
                this.SetFieldValue("ResourceValue", value);
            }
        }


        public System.String CultureInfo
        {
            get
            {
                return (System.String) (this.GetField("CultureInfo")).fieldValue;
            }

            set
            {
                this.SetFieldValue("CultureInfo", value);
            }
        }

    }
}
