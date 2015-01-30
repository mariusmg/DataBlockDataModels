<%@ Page Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true"
    Codebehind="SettingsUserInterface.aspx.cs" ValidateRequest="false" Inherits="voidsoft.Shopkeeper.Administration.SettingsUserInterface"
    Title="Site settings" Theme="admin" %>

<%@ MasterType VirtualPath="~/admin/AdminMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">
                <h3>
                    Settings</h3>
                <asp:DropDownList runat="server" ID="dropDownSettings" AutoPostBack="true" OnSelectedIndexChanged="dropDownSettings_OnSelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <br />
                <br />
                <asp:Panel runat="server" ID="panelTextBox" Visible="false">
                    <asp:TextBox runat="server" ID="textBoxText" Height="200px" Width="400px" TextMode="MultiLine">
                    </asp:TextBox>
                </asp:Panel>
                <asp:Panel runat="server" ID="panelInteger" Visible="false">
                    <asp:TextBox runat="server" Width="200px" ID="textBoxInteger">
                    </asp:TextBox>
                </asp:Panel>
                <asp:Panel runat="server" ID="panelBoolean" Visible="false">
                    <asp:RadioButton runat="server" Checked="false" ID="radioButtonTrue" Text="true"
                        GroupName="bools" />
                    <asp:RadioButton runat="server" Checked="false" ID="radioButtonFalse" Text="false"
                        GroupName="bools" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <br />
                <br />
                <asp:Button runat="server" CssClass="button" ID="buttonSend" OnClick="buttonSend_OnClick" Text="Salveaza" />
            </td>
        </tr>
    </table>
</asp:Content>
