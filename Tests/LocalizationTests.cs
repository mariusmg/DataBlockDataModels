using System.Globalization;
using NUnit.Framework;
using voidsoft.DataBlock;
using voidsoft.DataModels;

namespace Tests
{
    [TestFixture]
    public class LocalizationTests
    {
        private Localization localization = null;

        public const string CONNECTION_STRING = @"server=xxxx\sqlexpress;user id=xxx;password=xxxx;database=modeltests";
        private DatabaseServer server = DatabaseServer.SqlServer;

        /// <summary>
        /// Creates the everything.
        /// </summary>
        [SetUp]
        public void CreateEverything()
        {
            this.localization = new Localization(server, CONNECTION_STRING);
        }

        [Test]
        public void GetResourceValue()
        {
            string var = localization.GetResourceValue("default.aspx", "labelText.Text", "en-US");
        }

        [Test]
        public void GetResourceValueWithCultureInfo()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            string var = localization.GetResourceValue("default.aspx", "labelText.Text", System.Threading.Thread.CurrentThread.CurrentUICulture);
        }


        [Test]
        public void LoadAllForACulture()
        {
            localization.LoadResourcesForCultureInfo("en-US");
        }

        [Test]
        public void GetClassNames()
        {
            string[] stx = this.localization.GetCultures();
            string[] st = this.localization.GetClassNames(stx[0]);
        }

        [Test]
        public void GetCultures()
        {
            string[] st = this.localization.GetCultures();
        }

        [Test]
        public void TestIndexer()
        {
            string g = this.localization["a", "b"];
        }



    }
}