using NUnit.Framework;
using voidsoft.DataBlock;
using voidsoft.DataModels;

namespace Tests
{
    [TestFixture]
    public class NameValueTests
    {
        private NameValue nameValue;

        [SetUp()]
        public void SetIt()
        {
            nameValue = new NameValue(DatabaseServer.SqlServer, LocalizationTests.CONNECTION_STRING, "EntityValue");
        }

        [Test]
        public void LoadAllData()
        {
            nameValue.PreloadData();
        }

        [Test]
        public void GetValue()
        {
            nameValue.GetValue("a");
        }


        [Test]
        public void InvalidateCache()
        {
            nameValue.InvalidateCache();
        }



        [Test]
        public void SetValueByCreatingItIfnotExists()
        {
            NameValue nvv = new NameValue(DatabaseServer.SqlServer, LocalizationTests.CONNECTION_STRING, "EntityValue");
            nvv.CreateIfNotExists = true;
            nvv.SetValue("fssd", "adbahdasdasd");
        }


        [Test]
        public void SetValue()
        {
            nameValue.SetValue("a", "mmmm");

            string h = nameValue.GetValue("a");
            Assert.IsTrue(h == "mmmm");
        }

        [Test]
        public void TestIndexerGet()
        {
            string f = nameValue["a"];
        }

        [Test]
        public void TestIndexerSet()
        {
            nameValue["a"] = "naspa";
            Assert.IsTrue(nameValue["a"] == "naspa");
        }


        [Test]
        public void Create()
        {
            this.nameValue.Create("ax", "ax");
            Assert.IsTrue(nameValue["ax"] == "ax");
        }
    }
}