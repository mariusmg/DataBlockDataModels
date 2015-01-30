using System;
using System.ComponentModel;
using System.Windows.Forms;
using voidsoft.DataBlock;
using voidsoft.DataModels;

namespace voidsoft.Newt
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow : Form
    {
        private delegate void Add();

        private string currentCulture;
        private Localization localization = null;
        private bool isConnected = false;
        private string currentClassName;


        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }


        #region toolbar event handlers

        private void toolStripButtonOpenConnection_Click(object sender, EventArgs e)
        {
            InputBox ibox = null;

            try
            {
                ibox = new InputBox();
                DialogResult result = ibox.ShowDialog();

                if (result == DialogResult.OK)
                {
                    localization = new Localization(DatabaseServer.SqlServer, ibox.EnteredText);

                    this.backgroundWorker.RunWorkerAsync();
                }
            }
            catch
            {
            }
            finally
            {
                if (ibox == null)
                {
                    ibox.Dispose();
                }
            }
        }

        #endregion

        #region data loader

        private void LoadCultureData()
        {
            this.LoadCultures();
            this.LoadClasses();
        }

        private void LoadClasses()
        {
            string[] classes = this.localization.GetClassNames(this.currentCulture);

            Add d = delegate()
                        {
                            this.listBox.Items.Clear();

                            foreach (string className in classes)
                            {
                                this.listBox.Items.Add(className);
                            }
                        };


            this.Invoke(d);
        }

        private void LoadCultures()
        {
            string[] cultures = this.localization.GetCultures();

            Add d = delegate()
                        {
                            this.toolStripComboBoxCultures.Items.Clear();

                            foreach (string culture in cultures)
                            {
                                this.toolStripComboBoxCultures.Items.Add(culture);
                            }
                            if (this.toolStripComboBoxCultures.Items.Count > 0)
                            {
                                this.toolStripComboBoxCultures.SelectedIndex = 0;
                            }
                        };

            this.Invoke(d);
        }











        /// <summary>
        /// Handles the DoWork event of the backgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.LoadCultureData();
        }


        /// <summary>
        /// Handles the RunWorkerCompleted event of the backgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }


            this.isConnected = true;

            if (this.toolStripComboBoxCultures.Items.Count == 0)
            {
                MessageBox.Show("No cultures found", "Newt");
                this.toolStripButtonCreate.Enabled = true;
            }
            else
            {
                this.EnableControls();
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshClass();
        }

        private void RefreshClass()
        {
            if (this.listBox.SelectedItem != null)
            {
                this.dataGridView.DataSource =
                    this.localization.GetResources(this.listBox.SelectedItem.ToString(),
                                                   this.toolStripComboBoxCultures.SelectedItem.ToString());

                this.dataGridView.Columns[0].Width = 140;
                this.dataGridView.Columns[1].Width = 490;
                

                this.currentClassName = this.listBox.SelectedItem.ToString();

                if (this.dataGridView.RowCount > 0)
                {
                    this.buttonDelete.Enabled = true;
                    this.buttonEdit.Enabled = true;
                }
            }
        }

        #endregion

        /// <summary>
        /// Enables the controls.
        /// </summary>
        private void EnableControls()
        {
            this.toolStripComboBoxCultures.Enabled = true;
            this.toolStripButtonCreate.Enabled = true;
            this.toolStripButtonClone.Enabled = true;
            this.toolStripButtonDeleteCulture.Enabled = true;
            this.toolStripButtonEditCulture.Enabled = true;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the toolStripMenuItemNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripMenuItemNew_Click(object sender, EventArgs e)
        {
            ResourceEditWindow editWindow = null;

            try
            {
                editWindow = new ResourceEditWindow();
                editWindow.textBoxCultureInfo.Enabled = false;
                editWindow.textBoxCultureInfo.Text = this.currentCulture;

                editWindow.textBoxResourceKey.Enabled = false;
                editWindow.textBoxResourceValue.Enabled = false;

                DialogResult result = editWindow.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.localization.Operations.CreateNewResource(editWindow.ClassName, editWindow.ResourceKey,
                                                                   editWindow.ResourceValue, this.currentCulture);
                    this.listBox.Items.Add(editWindow.ClassName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.Message);
            }
            finally
            {
                if (editWindow == null)
                {
                    editWindow.Dispose();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the toolStripMenuItemDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listBox.SelectedItems.Count > 0)
                {

                    if ((MessageBox.Show("Are you sure you want to delete this class ?", "Newt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        this.localization.Operations.DeleteClass(this.listBox.SelectedItem.ToString(), this.currentCulture);
                        this.dataGridView.DataSource = null;
                        this.LoadClasses();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.Message, "Newt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Handles the Click event of the toolStripMenuItemEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            ResourceEditWindow editWindow = null;

            try
            {
                if (this.listBox.SelectedItems.Count > 0)
                {
                    string oldClassName = this.listBox.SelectedItem.ToString();
                    int index = this.listBox.SelectedIndex;

                    editWindow = new ResourceEditWindow();
                    editWindow.textBoxResourceKey.Enabled = false;
                    editWindow.textBoxResourceValue.Enabled = false;

                    editWindow.textBoxCultureInfo.Enabled = false;
                    editWindow.textBoxCultureInfo.Text = this.currentCulture;

                    editWindow.textBoxClassName.SelectedText = this.currentClassName;

                    DialogResult result = editWindow.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        this.localization.Operations.UpdateClass(editWindow.textBoxClassName.Text, oldClassName, this.currentCulture);

                        this.LoadClasses();
                        
                        //this.listBox.SelectedItems[index] = editWindow.textBoxClassName.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.Message, "Newt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (editWindow != null)
                {
                    editWindow.Dispose();
                }
            }
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event of the toolStripComboBoxCultures control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripComboBoxCultures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.toolStripComboBoxCultures.SelectedIndex > -1)
            {
                this.currentCulture = this.toolStripComboBoxCultures.Items[this.toolStripComboBoxCultures.SelectedIndex].ToString();
                this.dataGridView.DataSource = null;
                this.dataGridView.Refresh();
                this.LoadClasses();
            }
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonClone control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonClone_Click(object sender, EventArgs e)
        {
            InputBox box = null;

            try
            {

                box = new InputBox();
                box.Text = "Please enter the name of the new culture info";

                if (box.ShowDialog() == DialogResult.OK)
                {
                    this.localization.Operations.CloneCultureInfo(this.currentCulture, box.EnteredText);

                    this.dataGridView.DataSource = null;
                    this.backgroundWorker.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.Message, "Newt");
            }
            finally
            {

            }
        }


        /// <summary>
        /// Handles the Click event of the buttonNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonNew_Click(object sender, EventArgs e)
        {
            ResourceEditWindow editWindow = null;

            try
            {
                //check if we have any classes
                if (this.listBox.Items.Count == 0 || this.currentClassName == string.Empty)
                {
                    MessageBox.Show("No classes found. Please enter a class first", "Newt");
                    return;
                }
                
                
                editWindow = new ResourceEditWindow();
                editWindow.textBoxClassName.Text = this.currentClassName;
                editWindow.textBoxClassName.Enabled = false;
                editWindow.textBoxCultureInfo.Text = this.currentCulture;
                editWindow.textBoxCultureInfo.Enabled = false;

                if (editWindow.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.localization.Operations.CreateNewResource(this.currentClassName, editWindow.textBoxResourceKey.Text, editWindow.textBoxResourceValue.Text, this.currentCulture);
                    this.RefreshClass();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.Message);
            }
            finally
            {
                if (editWindow != null)
                {
                    editWindow.Dispose();
                }
            }

        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonCreate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonCreate_Click(object sender, EventArgs e)
        {
            ResourceEditWindow editWindow = null;

            try
            {
                editWindow = new ResourceEditWindow();
                DialogResult result = editWindow.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.localization.Operations.CreateNewResource(editWindow.ClassName, editWindow.ResourceKey, editWindow.ResourceValue, editWindow.CultureInfo);
                    // this.LoadCultureData();


                    this.backgroundWorker.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.Message);
            }
            finally
            {
                if (editWindow != null)
                {
                    editWindow.Dispose();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRowIndex = -1;

                if (this.dataGridView.SelectedCells.Count > 0)
                {
                    selectedRowIndex = this.dataGridView.SelectedCells[0].RowIndex;
                }

                if (this.dataGridView.SelectedRows.Count > 0)
                {
                    selectedRowIndex = this.dataGridView.SelectedRows[0].Index;
                }

                if (selectedRowIndex > -1)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this ?", "Newt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        this.localization.Operations.DeleteResourceKey(this.currentClassName, this.dataGridView.Rows[selectedRowIndex].Cells[0].Value.ToString(), this.currentCulture);
                        this.dataGridView.Rows.RemoveAt(selectedRowIndex);
                    }
                }
                else
                {
                    MessageBox.Show("Please select the resource to delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.Message, "Newt");
            }
            finally
            {

            }
        }


        private void buttonEdit_Click(object sender, EventArgs e)
        {
            ResourceEditWindow window = null;
            try
            {
                int selectedRowIndex = -1;

                if (this.dataGridView.SelectedCells.Count > 0)
                {
                    selectedRowIndex = this.dataGridView.SelectedCells[0].RowIndex;
                }

                if (this.dataGridView.SelectedRows.Count > 0)
                {
                    selectedRowIndex = this.dataGridView.SelectedRows[0].Index;
                }

                if (selectedRowIndex > -1)
                {
                    window = new ResourceEditWindow();

                    window.textBoxClassName.Text = this.currentClassName;
                    window.textBoxClassName.Enabled = false;

                    window.textBoxCultureInfo.Enabled = false;
                    window.textBoxCultureInfo.Text = this.currentCulture;

                    string oldResourceKey = this.dataGridView.Rows[selectedRowIndex].Cells[0].Value.ToString();
                    string oldResourceValue = this.dataGridView.Rows[selectedRowIndex].Cells[1].Value.ToString();

                    window.textBoxResourceKey.SelectedText = oldResourceKey;
                    window.textBoxResourceValue.SelectedText = oldResourceValue;


                    if (window.ShowDialog() == DialogResult.OK)
                    {
                        if (oldResourceKey != window.textBoxResourceKey.Text || oldResourceValue != window.textBoxResourceValue.Text)
                        {
                            this.localization.Operations.UpdateResource(this.currentCulture, this.currentClassName, window.textBoxResourceKey.Text, oldResourceKey, window.textBoxResourceValue.Text, oldResourceValue);

                            this.RefreshClass();

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select the resource to delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.Message, "Newt");
            }
            finally
            {
                if (window != null)
                {
                    window.Dispose();
                }
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the listBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void listBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                toolStripMenuItemDelete_Click(null, null);
            }
        }

        private void toolStripButtonDeleteCulture_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure you want to delete the selected culture ?", "Newt", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes))
                {
                    this.localization.Operations.DeleteCultureInfo(this.currentCulture);
                    this.backgroundWorker.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.Message);
            }

            finally
            {

            }
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonAbout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            
            AboutWindow aboutWindow = null;
            
            try
            {
                
                aboutWindow = new AboutWindow();
                aboutWindow.ShowDialog();
                
            }
            catch
            {
            }
            finally
            {
                if (aboutWindow != null)
                {
                    aboutWindow.Dispose();
                }
            }
        }


        /// <summary>
        /// Handles the Click event of the toolStripButtonEditCulture control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonEditCulture_Click(object sender, EventArgs e)
        {
            InputBox box = null;

            try
            {
                box = new InputBox();
                box.Text = "Enter the new name for culture " + this.currentCulture;

                string oldName = this.currentCulture;


                if (box.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.localization.Operations.EditCultureInfo(oldName, box.EnteredText);

                    this.backgroundWorker.RunWorkerAsync();
                    //this.LoadCultureData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.Message);
            }
            finally
            {
                if (box != null)
                {
                    box.Dispose();
                }
            }
        }

    }
}