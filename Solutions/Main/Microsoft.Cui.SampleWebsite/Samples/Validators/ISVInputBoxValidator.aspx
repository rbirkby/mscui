<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="IsvInputBoxValidator.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.Samples.Validators.IsvInputBoxValidator" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Microsoft.Cui.SampleWebsite" Namespace="Microsoft.Cui.SampleWebsite.Samples.Validators"
    TagPrefix="SampleVal" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">

    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>    
    
        <asp:Panel ID="DemoPanel1" runat="server" CssClass="demoCCarea">
            &nbsp;
            <table>
                <tr>
                    <td>
                        Date of Birth:
                    </td>
                    <td>
                        <NhsCui:DateInputBox ID="DateOfBirthBox" runat="server" Functionality="simple">
                        </NhsCui:DateInputBox>
                    </td>
                    <td>
                        <NhsCui:TimeInputBox ID="TimeOfBirthBox" runat="server">
                        </NhsCui:TimeInputBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <NhsCui:DateInputBoxValidator ID="DateOfBirthValidator" runat="server" ControlToValidate="DateOfBirthBox"
                            ErrorMessage="DOB invalid">
                        </NhsCui:DateInputBoxValidator>
                    </td>
                    <td>
                        <NhsCui:TimeInputBoxValidator ID="TimeInputBoxValidator2" runat="server" ControlToValidate="TimeOfBirthBox"
                            ErrorMessage="TOB invalid">
                        </NhsCui:TimeInputBoxValidator>
                    </td>
                    <td>
                        <SampleVal:MyIsvDateInputBoxGreaterThanValidator ID="MyVal" runat="server" ControlToValidate="DateOfDeathBox"
                            DateBoxToCompare="DateOfBirthBox" ErrorMessage="DOD should be greater than DOB"></SampleVal:MyIsvDateInputBoxGreaterThanValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date of Death:
                    </td>
                    <td>
                        <NhsCui:DateInputBox ID="DateOfDeathBox" runat="server" Functionality="simple">
                        </NhsCui:DateInputBox>
                    </td>
                    <td>
                        <NhsCui:TimeInputBox ID="TimeOfDeathBox" runat="server">
                        </NhsCui:TimeInputBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <NhsCui:DateInputBoxValidator ID="DateOfDeathValidator" runat="server" ControlToValidate="DateOfDeathBox"
                            ErrorMessage="DOD invalid">
                        </NhsCui:DateInputBoxValidator>
                    </td>
                    <td>
                        <NhsCui:TimeInputBoxValidator ID="TimeOfDeathValidator" runat="server" ControlToValidate="TimeOfDeathBox"
                            ErrorMessage="TOD invalid">
                        </NhsCui:TimeInputBoxValidator>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:Button ID="submitPageButton" Text="Submit" runat="server" />
        </asp:Panel>
        <div class="resetFloatAfterdemoCCArea"></div>
    </div>        
    <!-- Area for Description -->
    <asp:Panel ID="description_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <input type="image" ID="description_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
            Sample Description
        </div>
    </asp:Panel>
    <asp:Panel ID="description_ContentPanel" runat="server" Style="overflow: hidden;">
        <div class="section">
            <p>
                This custom input box validation sample is a demonstration of how you can extend
                the input box validators.
            </p>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeDescription" runat="Server" TargetControlID="description_ContentPanel"
        ExpandControlID="description_HeaderPanel" CollapseControlID="description_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="description_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Sample Description" CollapsedImage="~/images/SFTheme/acc_h.png"
        CollapsedText="Click to expand the Sample Description" SuppressPostBack="true" />
    <!-- Area for Properties -->
    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <input type="image" ID="properties_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
            Sample Details
        </div>
    </asp:Panel>
    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;"
        Height="0px">
        <div class="last section">
            <p>
                This sample demonstrates the two specific enhancements to the validators that are
                supplied with the MS CUI Toolkit:
            </p>
            <ul>
                <li>the validator can be made to appear like any other validator in your application
                    in both form and format</li>
                <li>further validation logic can be applied to either a single control or a group of
                    controls in order to achieve the specific error checking required for the context
                    in which it is being used</li>
            </ul>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="properties_ContentPanel"
        ExpandControlID="properties_HeaderPanel" CollapseControlID="properties_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="properties_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Sample Details" CollapsedImage="~/images/SFTheme/acc_h.png"
        CollapsedText="Click to expand the Sample Details" SuppressPostBack="true" />

    <script type="text/javascript">
// Global variables for Date Time Controls
var dateInput;
var timeInput;
var dateLabel;
var timeLabel;
var timetextbox;

   	
// on app load function adds property change handles
function pageLoad() {
}

    </script>

</asp:Content>
