using System;
using System.Windows.Forms;

namespace voidsoft.Newt
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ResourceEditWindow : Form
    {
        #region fields
        private string className;
        private string resourceKey;
        private string resourceValue;
        private string cultureInfo; 
        #endregion

        


        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceEditWindow"/> class
        /// </summary>
        public ResourceEditWindow()
        {
            InitializeComponent();
        }

        #region properties
        public string ResourceValue
        {
            get
            {
                return resourceValue;
            }
            set
            {
                resourceValue = value;
            }
        }

        public string ResourceKey
        {
            get
            {
                return resourceKey;
            }
            set
            {
                resourceKey = value;
            }
        }

        public string ClassName
        {
            get
            {
                return className;
            }
            set
            {
                className = value;
            }
        }

        public string CultureInfo
        {
            get
            {
                return cultureInfo;
            }
            set
            {
                cultureInfo = value;
            }
        } 
        #endregion


        #region event handlers
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.textBoxCultureInfo.Enabled && this.textBoxCultureInfo.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter the culture info", "Newt");
                return;
            }

            if (this.textBoxClassName.Enabled && this.textBoxClassName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter the class name", "Newt");
                return;
            }


            if (this.textBoxResourceKey.Enabled && this.textBoxResourceKey.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter the resource key", "Newt");
                return;
            }


            if (this.textBoxResourceValue.Enabled && this.textBoxResourceValue.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter the resource value", "Newt");
                return;
            }

            this.className = this.textBoxClassName.Text;
            this.cultureInfo = this.textBoxCultureInfo.Text;
            this.resourceKey = this.textBoxResourceKey.Text;
            this.resourceValue = this.textBoxResourceValue.Text;
            

            this.DialogResult = DialogResult.OK;
            this.Close();
        } 
        #endregion
    }
}