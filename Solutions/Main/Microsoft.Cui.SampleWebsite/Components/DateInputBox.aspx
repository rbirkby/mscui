<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsDateInputBox" Title="Untitled Page" CodeBehind="DateInputBox.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="headTagContent" ContentPlaceHolderID="leafPageSpecificHeadTags" runat="server">
    <link runat="server" rel="stylesheet" href="../CSS/DateInput.css" type="text/css" />
</asp:Content>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The DateInputBox allows you to enter a date. The date entered may be:
        </p>
        <ul>
            <li>An exact date including the day of the week</li>
            <li>An exact date excluding the day of the week</li>
            <li>An approximate date</li>
            <li>A month and a year</li>
            <li>A year</li>
            <li>A null index date type which can be defined by the Independent Software Vendor</li>
            <li>A null date</li>
        </ul>
        <p>
            If the date entered is ambiguous, it will be interpreted as having format dd-MM-yyyy.
            For example, if 01/02/2007 is entered, this will be interpreted as representing
            the 1st of February 2007.
        </p>      
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='dataInputASPNETTab' href=javascript:TabClick('dataInputASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <asp:Panel CssClass="demoControlarea" ID="demoPanel1" runat="server">
                    <NhsCui:DateInputBox runat="server" ID="DateInputBox1" WatermarkCssClass="DateInputWatermark"
                        NullStrings="Null,Nothing,Unknown" Functionality="Complex"></NhsCui:DateInputBox>
                </asp:Panel>
                <p class="resetFloatAfterdemoCCArea">
                    Attributes (select to view above):</p>
               
                <input onclick="ToggleApproximate();" type="checkbox" id="chkApprox" /><label for="chkApprox">Allow Approximate Value</label>
                <br />
                <input onclick="ToggleDisplayDayOfWeek();" type="checkbox" id="chkDayOfWeek" /><label for="chkDayOfWeek">Set DisplayDayOfWeek</label>
                <br />
                <input onclick="ToggleDisplayDateAsText();" type="checkbox" id="chkDateAsText" /><label for="chkDateAsText">Set DisplayDateAsText</label>
                <br />
                <br />
                <!-- Area for Usage Hints -->
                <asp:Panel ID="UsageHints_HeaderPanel" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="usageHints_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                        Usage Hints
                    </div>
                </asp:Panel>
                <asp:Panel ID="UsageHints_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                    <div class="section">
                        <p class="heading">
                            Entering a Date Directly</p>
                        <p>
                            You can enter a date directly in the DateInputBox by performing one of the following:</p>
                        <ul>
                            <li>Entering the date from your keyboard</li>
                            <li>Entering arithmetic, such as +2d to add two days to the existing date</li>
                            <li>Clicking the part of the date you want to change in the DateInputBox and using your
                                up and down arrow keys to increment and decrement the values</li>
                        </ul>
                        <p>
                            <strong>Note:</strong> You can use your left and right arrow keys to move between
                            different parts of the date in the DateInputBox.
                        </p>
                        <p class="heading">
                            Entering a Date Using the Calendar</p>
                        <p>
                            Click the Calendar icon to view the calendar. You can interact with the calendar
                            by:
                        </p>
                        <ul>
                            <li>Clicking on a day to set the date</li>
                            <li>Clicking Today to set the current date</li>
                            <li>clicking the left and right arrows to navigate through months and years</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeUsageHints" runat="Server" TargetControlID="UsageHints_ContentPanel"
                    ExpandControlID="UsageHints_HeaderPanel" CollapseControlID="UsageHints_HeaderPanel"
                    Collapsed="True" ExpandDirection="Vertical" ImageControlID="usageHints_ToggleImage"
                    ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Usage Hints section"
                    CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Usage Hints section"
                    SuppressPostBack="true" />
                <!-- Area for Properties -->
                <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="properties_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                        Properties
                    </div>
                </asp:Panel>
                <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;"
                    Height="0px">
                    <div class="section">
                        <p>
                            The DateInputBox control is initialized with the following code:</p>
                        <pre>&lt;NhsCui:DateInputBox ID="DateInputBox1" runat="server" WatermarkCssClass="DateInputWatermark"
                            NullStrings="Null,Nothing,Unknown" Functionality="Complex"/&gt;
                    </pre>
                        <ul>
                            <li><strong>AllowApproximate</strong> &ndash; specifies whether to display a checkbox
                                for the approximate flag</li>
                            <li><strong>DateType</strong> &ndash; specifies the DateType </li>
                            <li><strong>DateValue</strong> &ndash; specifies the DateValue </li>
                            <li><strong>DisplayDateAsText</strong> &ndash; specifies whether the date should be
                                displayed as Today, Tomorrow or Yesterday </li>
                            <li><strong>DisplayDayOfWeek</strong> &ndash; specifies whether the day of the week
                                should be displayed </li>
                            <li><strong>Functionality</strong> &ndash; specifies the functionality exposed by the
                                DateInputBox </li>
                            <li><strong>Month</strong> &ndash; specifies the month </li>
                            <li><strong>NullStrings</strong> &ndash; lists localized strings that identify different
                                types of null index dates </li>
                            <li><strong>Value</strong> &ndash; the entered date </li>
                            <li><strong>WatermarkCssClass</strong> &ndash; gets or sets the CSS class used for the
                                WatermarkText property </li>
                            <li><strong>WatermarkText</strong> &ndash; gets or sets the watermark text </li>
                            <li><strong>Year</strong> &ndash; specifies the year </li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="properties_ContentPanel"
                    ExpandControlID="properties_HeaderPanel" CollapseControlID="properties_HeaderPanel"
                    Collapsed="True" ExpandDirection="Vertical" ImageControlID="properties_ToggleImage"
                    ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                    CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                    SuppressPostBack="true" />
                <!-- Area for Additional Info -->
                <asp:Panel ID="AdditionalInfo_HeaderPanel" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="AdditionalInfo_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                        Additional Information
                    </div>
                </asp:Panel>
                <asp:Panel ID="AdditionalInfo_ContentPanel" runat="server" Style="overflow: hidden;
                    height: 0px">
                    <div class="last section">
                        <ul>
                            <li><strong>DisplayDateAsText</strong> has a default value of &ldquo;False&rdquo;
                            </li>
                            <li><strong>DisplayDayOfWeek</strong> has a default value of &ldquo;False&rdquo;
                            </li>
                            <li><strong>Functionality</strong> has a default value of &ldquo;Simple&rdquo; </li>
                            <li><strong>NullStrings</strong> defaults to an empty list </li>
                            <li><strong>WatermarkCssClass</strong> defaults to an empty string</li>
                            <li><strong>WatermarkText</strong> defaults to &ldquo;dd-MMM-yyyy&rdquo;</li>
                        </ul>
                        <p>
                            &nbsp;</p>
                        <p>
                            The entry modes for the DateInputBox are:
                        </p>
                        <ul>
                            <li>Using the left and right arrow keys on your keyboard</li>
                            <li>Entering freeform text</li>
                            <li>Using arithmetic where y, m, w and d represent years, months, weeks and days respectively;
                                a positive or negative operand may be used</li>
                            <li>Using the dropdown calendar as illustrated above</li>
                            <li>Using the space bar to cycle through null types, if they exist</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeAdditionalInfo" runat="Server" TargetControlID="AdditionalInfo_ContentPanel"
                    ExpandControlID="AdditionalInfo_HeaderPanel" CollapseControlID="AdditionalInfo_HeaderPanel"
                    Collapsed="True" ExpandDirection="Vertical" ImageControlID="AdditionalInfo_ToggleImage"
                    ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                    CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                    SuppressPostBack="true" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='dataInputWinFormsTab' href=javascript:TabClick('dataInputWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />                    
                    <img class="controls_border" alt="DateInputBox WinForms control screenshot" title="DateInputBox WinForms control screenshot" runat="server" src="~/Components/Images/dateinputbox.GIF" />
                    <br />
                    <br />
                <p>
                    The full source code for this control can be found in the
                    Microsoft Health Common User Interface Toolkit, which can be downloaded from our
                    <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx" target="_blank"
                        title="Link to releases page on the CodePlex site (New Window)">CodePlex</a>
                    site.
                </p>
                    <!-- Area for Winforms Properties Section -->
                    <asp:Panel ID="WinformsProps_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WinformsProps_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WinformsProps_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
			             <ul>
                            <li><strong>AllowApproximate</strong> &ndash; specifies whether to display a checkbox
                                for the approximate flag</li>
                            <li><strong>DateType</strong> &ndash; specifies the DateType </li>
                            <li><strong>DateValue</strong> &ndash; specifies the DateValue </li>
                            <li><strong>DisplayDateAsText</strong> &ndash; specifies whether the date should be
                                displayed as Today, Tomorrow or Yesterday </li>
                            <li><strong>DisplayDayOfWeek</strong> &ndash; specifies whether the day of the week
                                should be displayed </li>
                            <li><strong>Functionality</strong> &ndash; specifies the functionality exposed by the
                                DateInputBox </li>
                            <li><strong>Month</strong> &ndash; specifies the month </li>
                            <li><strong>NullStrings</strong> &ndash; lists localized strings that identify different
                                types of null index dates </li>
                            <li><strong>Value</strong> &ndash; the entered date </li>
                            <li><strong>WatermarkText</strong> &ndash; gets or sets the watermark text </li>
                            <li><strong>Year</strong> &ndash; specifies the year </li>
                        </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WinformsProps_Extender" runat="Server" TargetControlID="WinformsProps_ContentPanel"
                        ExpandControlID="WinformsProps_HeaderPanel" CollapseControlID="WinformsProps_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="WinformsProps_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                        SuppressPostBack="true" />                
                    <!-- Area for Winforms Additional Information Section -->
                    <asp:Panel ID="WinformsAddInfo_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WinformsAddInfo_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Additional Information
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WinformsAddInfo_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="last section">
                            <p>
                                <ul>
                                    <li>DisplayDateAsText has a default of False</li>
                                    <li>DisplayDayOfWeek has a default of False</li>
                                    <li>Functionality has a default of Simple</li>
                                    <li>NullStrings defaults to an empty list</li>
                                    <li>WatermarkText defaults to dd-MMM-yyyy</li>
                                </ul>
                            </p>
                            <p>
                                The entry modes for the DateInputBox are:
                                <ul>
                                    <li>using the left and right arrow keys on your keyboard</li>
                                    <li>entering freeform text</li>
                                    <li>using arithmetic where y, m , w and d represent years, months, weeks and days respectively;
                                        a positive or negative operand may be used</li>               
                                    <li>using the dropdown calendar</li>
                                    <li>using the space bar to cycle through null types, if they exist</li>
                                </ul>
                            </p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WinformsAddInfo_Extender" runat="Server" TargetControlID="WinformsAddInfo_ContentPanel"
                        ExpandControlID="WinformsAddInfo_HeaderPanel" CollapseControlID="WinformsAddInfo_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="WinformsAddInfo_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />
                </div>    
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>


    <script type="text/javascript">
            function ToggleApproximate() 
            {
                var obj = $find("<%=DateInputBox1.ClientID%>" + '_DateInputBox1_DateInputBoxExtender');
                obj.set_allowApproximate(!obj.get_allowApproximate());
            }
            function ToggleDisplayDayOfWeek() 
            {
                var obj = $find("<%=DateInputBox1.ClientID%>" + '_DateInputBox1_DateInputBoxExtender');
                obj.set_displayDayOfWeek(!obj.get_displayDayOfWeek());
            }
            function ToggleDisplayDateAsText() 
            {
                var obj = $find("<%=DateInputBox1.ClientID%>" + '_DateInputBox1_DateInputBoxExtender');
                obj.set_displayDateAsText(!obj.get_displayDateAsText());
            }
    </script>

</asp:Content>
