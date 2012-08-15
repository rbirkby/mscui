<%@ Page Language="C#" MasterPageFile="~/LeafPage.master" AutoEventWireup="true"
    Inherits="SamplesDateTimeRestyledDateSample" Title="Untitled Page" Codebehind="RestyledDateSample.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <link id="AlternativeStyles" rel="stylesheet" href="AltCalendar.css" type="text/css" />
   
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        
        <div class="demoCCarea">
            <div style="float:left;margin-right:10px;">
                <asp:Panel ID="monthCalendar1" runat="server">
                </asp:Panel>
                <NhsCui:MonthCalendarExtender TargetControlID="monthCalendar1" runat="server" ID="MonthCalendarExtender1"
                    Value="19-Jun-2001">
                </NhsCui:MonthCalendarExtender>
            </div>
            <NhsCui:DateInputBox ID="dateInputBox1" runat="server" Functionality="simple" ToolTip="Enter a date, e.g. 12 Aug 2005 or click on the calendar icon to select a date">
            </NhsCui:DateInputBox>
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
            This is an example of restyling the DateInputBox and MonthCalendar controls using a Cascading Style Sheet (CSS).
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeDescription" runat="Server" TargetControlID="description_ContentPanel"
        ExpandControlID="description_HeaderPanel" CollapseControlID="description_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="description_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Sample Description"
        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Sample Description"
        SuppressPostBack="true" />
    <!-- Area for Properties -->
    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <input type="image" ID="properties_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
            Sample Details
        </div>
    </asp:Panel>
    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;"
        Height="0px">
        <div class="last section">
            <p>
                The original CSS used to style the MonthCalendar control is available here:
            </p>
            <ul>
                <li><a href="MonthCalendar.css">MonthCalendar.css</a></li>
            </ul>
            <p>
                The original CSS used to style the DateInputBox control is available here:
            </p>
            <ul>
                <li><a href="Calendar.css">Calendar.css</a></li>
            </ul>
            <p>
                The two controls above have been restyled using an alternative CSS file which 
                is referenced in this page by adding the following style tag:
            </p>
            <pre>&lt;link id="AlternativeStyles" rel="stylesheet" href="AltCalendar.css" type="text/css" /&gt;</pre>             
            <p>
                The alternative CSS file is available here:
            </p>
            <ul>
                <li><a href="AltCalendar.css">AltCalendar.css</a></li>
            </ul>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="properties_ContentPanel"
        ExpandControlID="properties_HeaderPanel" CollapseControlID="properties_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="properties_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Sample Details"
        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Sample Details"
        SuppressPostBack="true" />

</asp:Content>
