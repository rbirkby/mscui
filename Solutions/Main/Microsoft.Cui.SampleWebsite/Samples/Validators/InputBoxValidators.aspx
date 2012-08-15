<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Title="InputBox Validators (Standard)" Codebehind="InputBoxValidators.aspx.cs"
    Inherits="SamplesValidatorsInputBoxValidators" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="leafPageSpecificHeadTags" runat="server">
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">

    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>    
        <asp:Panel ID="DemoPanel1" runat="server" CssClass="demoCCarea" Width="650px">
            &nbsp;
            <fieldset>
                <legend>Validation Configuration</legend>
                <div>
                    <asp:CheckBox ID="ToggleValidationCheckBox" runat="server" Text="Switch On Validation"
                        AutoPostBack="true" Checked="true" />
                    <fieldset>
                        <legend>Validation Options</legend>
                        <div>
                            <span style="display:block; width:160px;text-align:right;float:left;clear:left;">Validation Summary:</span>
                            <asp:RadioButton AutoPostBack="true" GroupName="ValidationSummaryOnOff" ID="ValidationSummaryOnRadioButton"
                                runat="server" Text="On" Checked="true" style="display:block; width:140px;float:left;"/>
                            <asp:RadioButton AutoPostBack="true" GroupName="ValidationSummaryOnOff" ID="ValidationSummaryOffRadioButton"
                                runat="server" Text="Off"/>
                        </div>
                        <div>
                           <span style="display:block; width:160px;text-align:right;float:left;clear:left;">Initiate Validation:</span>
                           <asp:RadioButton AutoPostBack="true" GroupName="InitiateValidation" ID="ValidateOnBlurRadioButton"
                                runat="server" Text="On Exit" Checked="true" style="display:block; width:140px;float:left;"/>
                           <asp:RadioButton AutoPostBack="true" GroupName="InitiateValidation" ID="ValidateOnSubmitRadioButton"
                                runat="server" Text="On Page Submit"/>
                        </div>
                        <div>
                            <span style="display:block; width:160px;text-align:right;float:left;clear:left;">Focus After Validation: </span>
                            <asp:RadioButton AutoPostBack="true" GroupName="FocusAfterValidation" ID="FocusOnErrorTrueRadioButton"
                                runat="server" Text="Same Control" Checked="true" style="display:block; width:140px;float:left;"/>
                            <asp:RadioButton AutoPostBack="true" GroupName="FocusAfterValidation" ID="FocusOnErrorFalseRadioButton"
                                runat="server" Text="Next Control"/>
                        </div>
                    </fieldset>
                </div>
            </fieldset>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <table>
                <tr>
                    <td>
                        DateInputBox
                    </td>
                    <td>
                        <NhsCui:DateInputBox ID="dateInputBox1" runat="server" Functionality="simple">
                        </NhsCui:DateInputBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <NhsCui:DateInputBoxValidator ID="dateInputBoxValidator1" runat="server" ControlToValidate="dateInputBox1"
                            ErrorMessage="Invalid date">
                        </NhsCui:DateInputBoxValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        TimeInputBox
                    </td>
                    <td>
                        <NhsCui:TimeInputBox ID="timeInputBox1" Functionality="Simple" runat="server">
                        </NhsCui:TimeInputBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <NhsCui:TimeInputBoxValidator ID="timeInputBoxValidator2" runat="server" ControlToValidate="timeInputBox1"
                            ErrorMessage="Invalid time">
                        </NhsCui:TimeInputBoxValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        TimeSpanInputBox
                    </td>
                    <td>
                        <NhsCui:TimeSpanInputBox ID="timeSpanInputBox1" runat="server"></NhsCui:TimeSpanInputBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <NhsCui:TimeSpanInputBoxValidator ID="timeSpanInputBoxValidator1" runat="server"
                            ControlToValidate="timeSpanInputBox1" ErrorMessage="Invalid timespan">
                        </NhsCui:TimeSpanInputBoxValidator>
                    </td>
                </tr>
            </table>
            <asp:Button ID="submitPageButton" Text="Submit" runat="server" />
        </asp:Panel>
        <p class="resetFloatAfterdemoCCArea"></p>
        
    </div>      
    <!-- Area for Usage Hints -->
    <asp:Panel ID="UsageHints_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <input type="image" ID="usageHints_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
            Usage Hints
        </div>
    </asp:Panel>
    <asp:Panel ID="UsageHints_ContentPanel" runat="server" Style="overflow: hidden;height:0px">
        <div class="section">
            <ul>
                <li>Select the Switch On Validation checkbox to turn on validation checking.</li>
                <li>Select the Validation Options checkboxes according to how and when you want the validation checking to be performed.</li>
                <li>Enter values in the input boxes and click Submit to view how the different validation options work.</li>
            </ul>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeUsageHints" runat="Server" TargetControlID="UsageHints_ContentPanel"
        ExpandControlID="UsageHints_HeaderPanel" CollapseControlID="UsageHints_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="usageHints_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Usage Hints section"
        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Usage Hints section"
        SuppressPostBack="true" />
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
                The input box validators are:
            </p>
            <ul>
                <li>the DateInputBoxValidator</li>
                <li>the TimeInputBoxValidator</li>
                <li>the TimeSpanInputBoxValidator</li>
            </ul>
            <p>
                These validators are all inherited from the ASP.NET BaseValidator class and are
                specifically designed to check the validity of the user input; an error message
                is displayed if the input is deemed to be invalid.
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
                These validators were designed because:
            </p>
            <ul>
                <li>the DateInputBox, TimeInputBox and TimeSpanInputBox controls require a method to
                    assess whether user input is valid</li>
                <li>it is best practice to inform the user where any errors have occurred and preferably
                    give guidance on how to correct these errors</li>
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
function pageLoad()  {
    // get control refrences
    dateInput = $get("<%=dateInputBox1.ClientID%>" + '_dateInputBox1_TextBox').DateInputBox;
    timeInput = $get("<%=timeInputBox1.ClientID%>" + '_timeInputBox1_TextBox').TimeInputBox;
}
    </script>

</asp:Content>
