using System;
using System.Data;
using voidsoft.DataBlock;

namespace voidsoft.DataModels
{

    [Serializable()]
    public class KeyValue : TableMetadata
    {

        public enum KeyValueFields
        {
            EntityId,
            EntityName,
            EntityValue
        }


        private DatabaseField[] myFields;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValue"/> class
        /// </summary>
        public KeyValue()
        {
            myFields = new DatabaseField[3];
            myFields[0] = new DatabaseField(DbType.Int32, "EntityId", true, true, null);
            myFields[1] = new DatabaseField(DbType.String, "EntityName", false, false, null);
            myFields[2] = new DatabaseField(DbType.String, "EntityValue", false, false, null);

            this.currentTableName = "EntityValue";


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
        public KeyValue Clone()
        {
            return this.Clone<KeyValue>();
        }

        public System.Int32? EntityId
        {
            get
            {
                return (System.Int32?)(this.GetField("EntityId")).fieldValue;
            }

            set
            {
                this.SetFieldValue("EntityId", value);
            }
        }


        public System.String EntityName
        {
            get
            {
                return (System.String)(this.GetField("EntityName")).fieldValue;
            }

            set
            {
                this.SetFieldValue("EntityName", value);
            }
        }


        public System.String EntityValue
        {
            get
            {
                return (System.String)(this.GetField("EntityValue")).fieldValue;
            }

            set
            {
                this.SetFieldValue("EntityValue", value);
            }
        }

    }
}
