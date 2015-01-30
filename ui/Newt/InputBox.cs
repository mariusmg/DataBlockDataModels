using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace voidsoft.Newt
{
    public partial class InputBox : Form
    {

        private string enteredText;
        
        
        public InputBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the entered text.
        /// </summary>
        /// <value>The entered text.</value>
        public string EnteredText
        {
            get
            {
                return this.enteredText;
            }
            set
            {
                this.enteredText = value;
            }
        }

        /// <summary>
        /// Handles the Load event of the InputBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InputBox_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            
            if(this.textBoxOK.Text.Trim() != string.Empty)
            {
                this.enteredText = this.textBoxOK.Text;

                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the textBoxOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void textBoxOK_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                this.buttonOK_Click(null,null);
            }
        }
    }
}