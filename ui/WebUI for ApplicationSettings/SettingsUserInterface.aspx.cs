using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using voidsoft.DataModels;

namespace voidsoft.Shopkeeper.Administration
{
    public partial class SettingsUserInterface : Page
    {
        private ApplicationSettingsManager manager;
        private ApplicationSettings[] settings;


        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                manager = new ApplicationSettingsManager(ApplicationContext.Server, ApplicationContext.ConnectionString);

                GetApplicationSettings();

                if (!IsPostBack)
                {
                    SetPageContext();
                }

            }
            catch (Exception ex)
            {
                Log.WriteTraceOutput(ex);

            }
        }


        private void SetPageContext()
        {
            dropDownSettings.Items.Add(new ListItem("-----", "-"));

            foreach (ApplicationSettings setting in settings)
            {
                if (setting.ShowInUserInterface)
                {
                    dropDownSettings.Items.Add(new ListItem(setting.Name, setting.SettingId.ToString()));
                }
            }
        }

        private void GetApplicationSettings()
        {
            settings = manager.GetSettings();
        }


        protected void dropDownSettings_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropDownSettings.SelectedIndex == 0)
            {

                ShowPanel(null);


                return;
            }


            string key = dropDownSettings.SelectedValue;


            ApplicationSettings find = Array.Find(settings, delegate(ApplicationSettings s)
                                                                {
                                                                    return s.SettingId.ToString() == key;
                                                                });


            if (find != null)
            {
                if (find.DataType == "text")
                {
                    ShowPanel(panelTextBox);

                    textBoxText.Text = find.SettingValue;
                }
                else if ((find.DataType == "integer") || (find.DataType == "double"))
                {
                    ShowPanel(panelInteger);
                    textBoxInteger.Text = find.SettingValue;
                }
                else if (find.DataType == "bool")
                {
                    ShowPanel(panelBoolean);

                    try
                    {
                        bool value = Boolean.Parse(find.SettingValue);

                        if (value)
                        {
                            radioButtonTrue.Checked = true;
                        }
                        else
                        {
                            radioButtonFalse.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void ShowPanel(Panel p)
        {
            Control control = Master.FindControl("ContentPlaceHolder1");

            foreach (Control c in control.Controls)
            {
                if (c is Panel)
                {
                    c.Visible = false;
                }
            }

            if (p != null)
            {

                foreach (Control c in control.Controls)
                {
                    if (c is Panel)
                    {
                        if (c.ID == p.ID)
                        {
                            c.Visible = true;
                            return;
                        }
                    }
                }
            }
        }



        protected void buttonSend_OnClick(object sender, EventArgs e)
        {

            try
            {


                if (dropDownSettings.SelectedIndex == 0)
                {
                    return;
                }


                string key = dropDownSettings.SelectedValue;


                ApplicationSettings find = Array.Find(settings, delegate(ApplicationSettings s)
                                                                    {
                                                                        return s.SettingId.ToString() == key;
                                                                    });

                if (find.DataType == "text")
                {
                    find.SettingValue = textBoxText.Text;
                }
                else if (find.DataType == "integer")
                {

                    int f;

                    bool parse = Int32.TryParse(textBoxInteger.Text, out f);


                    if (parse == false)
                    {

                        this.Master.ShowErrorMessage("Please enter a valid numeric value");

                        return;
                    }

                    find.SettingValue = textBoxInteger.Text;
                }
                else if (find.DataType == "double")
                {

                    double d;

                    bool parse = Double.TryParse(textBoxInteger.Text, out d);


                    if (parse == false)
                    {
                        this.Master.ShowErrorMessage("Please enter a valid numeric value");

                        return;

                    }

                    find.SettingValue = textBoxInteger.Text;
                }
                else if (find.DataType == "bool")
                {
                    if (radioButtonTrue.Checked)
                    {
                        find.SettingValue = "true";
                    }
                    else
                    {
                        find.SettingValue = "false";
                    }
                }

                manager.Update(find);


                ApplicationContext.UpdateOption(find);

                Response.Redirect(Request.RawUrl, true);


            }
            catch (Exception ex)
            {
                Log.WriteTraceOutput(ex);
            }

        }
    }
}