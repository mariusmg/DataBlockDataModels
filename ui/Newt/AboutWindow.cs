using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace voidsoft.Newt
{
    public partial class AboutWindow : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutWindow"/> class.
        /// </summary>
        public AboutWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the labelLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void labelLink_Click(object sender, EventArgs e)
        {
            try
            {
                Process pc = new Process();

                ProcessStartInfo info = new ProcessStartInfo();
                info.UseShellExecute = true;
                info.FileName = "http://www.voidsoft.ro";

                pc.StartInfo = info;

                pc.Start();
            }
            catch
            {
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}