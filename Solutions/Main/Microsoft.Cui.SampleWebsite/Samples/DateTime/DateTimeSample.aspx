<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="SamplesDateTimeDateTimeSample" Title="Untitled Page" Codebehind="DateTimeSample.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="headTagContent" ContentPlaceHolderID="leafPageSpecificHeadTags" runat="server">
    <link id="dateInputCssLink" runat="server" rel="stylesheet" href="../../CSS/DateInput.css"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <asp:Panel ID="DemoPanel1" runat="server" CssClass="demoCCarea">
            <table>
                <tr>
                    <td>
                        <NhsCui:DateInputBox ID="dateInputBox1" runat="server" Functionality="simple">
                        </NhsCui:DateInputBox>
                    </td>
                    <td>
                        <NhsCui:TimeInputBox ID="timeInputBox1" Functionality="Simple" runat="server">
                        </NhsCui:TimeInputBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="dateLabelText" runat="server" Text="The date input was:">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="dateLabelValue" runat="server" Text="-">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="timeLabelText" runat="server" Text="The time input was:">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="timeLabelValue" runat="server" Text="-">
                        </asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="resetFloatAfterdemoCCArea">
        </div>
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
                This sample allows you to use the basic forms of the DateInputBox and TimeInputBox
                controls in combination. You can input exact dates using freetext, the calendar,
                or the up and down arrow keys.
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
                You can enter an exact date and/or time only and the ToolTip reflects only the allowed
                behaviors.
            </p>
            <p>
                The following properties have been set:</p>
            <ul>
                <li>AllowApproximate has been set to &ldquo;False&rdquo; for time </li>
                <li>Default has been set to the current date and time </li>
                <li>DisplayDayOfWeek has been set to not include the day of the week </li>
                <li>Functionality is set to &ldquo;Simple&rdquo;; </li>
                <li>Month/Years, Years, NullIndexes and Nulls are not allowed</li>
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

// refreshing labels on property change
function onPropertyChange(sender, args) 
{
    refreshDateTimeLabels();
}
   	
// on app load function adds property change handles
function pageLoad() 
{
    // get control refrences
    dateInput = $get("<%=dateInputBox1.ClientID%>"+ '_dateInputBox1_TextBox').DateInputBox;
    timeInput = $get("<%=timeInputBox1.ClientID%>"+ '_timeInputBox1_TextBox').TimeInputBox;
    dateLabel = $get("<%=dateLabelValue.ClientID%>");
    timeLabel = $get("<%=timeLabelValue.ClientID%>");
     
    // date time change handlers
    dateInput.add_propertyChanged(onPropertyChange);            
    timeInput.add_propertyChanged(onPropertyChange);

    // refresh date time labels
    refreshDateTimeLabels();
}

function refreshDateTimeLabels() 
{
   dateLabel.replaceChild(document.createTextNode(dateInput.get_value().toString()), dateLabel.firstChild);
   timeLabel.replaceChild(document.createTextNode(timeInput.get_value().toString()), timeLabel.firstChild);
}

    </script>

</asp:Content>
