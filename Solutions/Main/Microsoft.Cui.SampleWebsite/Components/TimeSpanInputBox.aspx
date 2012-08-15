<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsTimeSpanInputBox" Title="Untitled Page" CodeBehind="TimeSpanInputBox.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
         <p>
                The TimeSpanInputBox lets you enter a time duration in a clear and unambiguous manner.
                The control comprises a freetext entry area where you can enter a single span of
                time in a variety of defined formats.
            </p>
            <p>
                &nbsp;</p>
            <table class="desc">
                <tr>
                    <th align="left" width="30%">
                        Long units (singular)
                    </th>
                    <td>
                        year
                    </td>
                    <td>
                        month
                    </td>
                    <td>
                        week
                    </td>
                    <td>
                        day
                    </td>
                    <td>
                        hour
                    </td>
                    <td>
                        minute
                    </td>
                    <td>
                        second
                    </td>
                </tr>
                <tr>
                    <th align="left" width="30%">
                        Long units (plural)
                    </th>
                    <td>
                        years
                    </td>
                    <td>
                        months
                    </td>
                    <td>
                        weeks
                    </td>
                    <td>
                        days
                    </td>
                    <td>
                        hours
                    </td>
                    <td>
                        minutes
                    </td>
                    <td>
                        seconds
                    </td>
                </tr>
                <tr>
                    <th align="left" width="30%">
                        Short units (singular)
                    </th>
                    <td>
                        y
                    </td>
                    <td>
                        m
                    </td>
                    <td>
                        w
                    </td>
                    <td>
                        d
                    </td>
                    <td>
                        hr
                    </td>
                    <td>
                        min
                    </td>
                    <td>
                        sec
                    </td>
                </tr>
                <tr>
                    <th align="left" width="30%">
                        Short units (plural)
                    </th>
                    <td>
                        y
                    </td>
                    <td>
                        m
                    </td>
                    <td>
                        w
                    </td>
                    <td>
                        d
                    </td>
                    <td>
                        hrs
                    </td>
                    <td>
                        min
                    </td>
                    <td>
                        sec
                    </td>
                </tr>
            </table>
            <p>
                &nbsp;</p>
            <p>
                TimeSpanInputBox then displays the resolved time span according to predefined rules.
                You can enter different durations either independently or using combinations. You
                can also edit the individual time elements in the entered durations.</p>
        </div>
        <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
            <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='timeSpanInputBoxASPNETTab' href=javascript:TabClick('timeSpanInputBoxASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
                <ContentTemplate>
                    <br />
                    <div style="height:20px;width:400px;">Example ASP.NET control (embedded):</div>
                    <asp:Panel CssClass="demoControlarea" ID="demoPanel1" runat="server">
                       <NhsCui:TimeSpanInputBox runat="server" ID="TimeSpanInputBox1" />
                    </asp:Panel>
                    <p class="resetFloatAfterdemoCCArea">
                        <input onclick="javascript:ToggleIsAge();" type="checkbox" id="isAgeCheckBox" /><label
                            for="isAgeCheckBox">Treat input value as age</label>
                    </p>
                     <!-- Area for Usage Hints -->
        <asp:Panel ID="UsageHints_HeaderPanel" runat="server" Style="cursor: pointer;">
            <div class="heading">
                <input type="image" id="usageHints_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                Usage Hints
            </div>
        </asp:Panel>
        <asp:Panel ID="UsageHints_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
            <div class="section">
                <p>
                    You can enter a time span directly in the TimeSpanInputBox using:
                </p>
                <ul>
                    <li>'y' for years</li>
                    <li>'m' for months</li>
                    <li>'w' for weeks</li>
                    <li>'d' for days</li>
                    <li>'hrs' for hours</li>
                    <li>'min' for minutes</li>
                    <li>'sec' for seconds</li>
                    <li>A combination of these units, with the units listed in the order given</li>
                </ul>
                <p>
                    You can optionally include spaces between the different time elements. For example,
                    you could enter:
                </p>
                <ul>
                    <li>2y23d14hrs7min, or</li>
                    <li>2y 23d 14hrs 7min</li>
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
                    The TimeSpanInputBox is initialized with the following code:</p>
                <pre>&lt;NhsCui:TimeSpanInputBox ID="TimeSpanInputBox1" runat="server" /&gt;
            </pre>
                <ul>
                    <li><strong>From</strong> &ndash; the wrapper for Value.From </li>
                    <li><strong>Granularity</strong> &ndash; the wrapper for Value.Granularity </li>
                    <li><strong>IsAge</strong> &ndash; the wrapper for Value.IsAge </li>
                    <li><strong>Threshold</strong> &ndash; the wrapper for Value.Threshold </li>
                    <li><strong>To</strong> &ndash; the wrapper for Value.To </li>
                    <li><strong>Value</strong> &ndash; gets or sets the time span entered in the input box
                    </li>
                    <li><strong>UnitLength</strong> &ndash; specifies whether long or short units are used</li>
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
            If a duration control is combined with either date and/or time controls, the values
            expressed within the duration control will be dependent on the date/time controls
            combined with the duration control, as follows:</p>
            <ul>
                <li>Duration and Time Control = hrs, min and sec</li>
                <li>Duration and Date Control = y, m and d</li>
                <li>Duration and Date and Time = y, m, d, hrs, min and sec</li>
            </ul>
            <p>
                The clinical application needs to allow:</p>
            <ul>
                <li>The entry of different durations either independently or using combinations
                </li>
                <li>Editing of the individual elements of the durations entered into the control
                </li>
                <li>The duration to be automatically calculated when a start date/time and an end date/time
                    are entered</li>
                <li>The end date/time to be automatically calculated when a start date/time and duration
                    (or vice versa) are entered </li>
                <li>The use of y for years, m for months, d for days, hrs for hours, min for minutes
                    and sec for seconds </li>
            </ul>
            <p>
                If IsAge is:</p>
            <ul>
                <li>&ldquo;True&rdquo;, the Granularity and Threshold property values are set from the
                    time span </li>
                <li>&ldquo;False&rdquo;, the Granularity and Threshold property values are set from
                    the assigned Granularity and Threshold property values </li>
            </ul>
            The following table provides guidance on how the Granularity and Threshold
            values are set from the time span. &nbsp
            <table class="desc">
                <thead align="left">
                    <tr>
                        <th width="33%">
                            Time span
                        </th>
                        <th width="33%">
                            Granularity
                        </th>
                        <th width="33%">
                            Threshold
                        </th>
                    </tr>
                </thead>
                <tr>
                    <td>
                        Less than 2 hours
                    </td>
                    <td>
                        Minutes (TimeSpanUnits.Minutes)
                    </td>
                    <td>
                        Minutes (TimeSpanUnits.Minutes)
                    </td>
                </tr>
                <tr>
                    <td>
                        Less than 2 days
                    </td>
                    <td>
                        Hours (TimeSpanUnits.Hours)
                    </td>
                    <td>
                        Hours (TimeSpanUnits.Hours)
                    </td>
                </tr>
                <tr>
                    <td>
                        Less than 4 weeks
                    </td>
                    <td>
                        Days (TimeSpanUnits.Days)
                    </td>
                    <td>
                        Days (TimeSpanUnits.Days)
                    </td>
                </tr>
                <tr>
                    <td>
                        Less than 1 year
                    </td>
                    <td>
                        Weeks (TimeSpanUnits.Weeks)
                    </td>
                    <td>
                        Days (TimeSpanUnits.Days)
                    </td>
                </tr>
                <tr>
                    <td>
                        Less than 2 years
                    </td>
                    <td>
                        Months (TimeSpanUnits.Months)
                    </td>
                    <td>
                        Days (TimeSpanUnits.Days)
                    </td>
                </tr>
                <tr>
                    <td>
                        Less than 18 years
                    </td>
                    <td>
                        Years (TimeSpanUnits.Years)
                    </td>
                    <td>
                        Months (TimeSpanUnits.Months)
                    </td>
                </tr>
                <tr>
                    <td>
                        Greater than or equal to 18 years
                    </td>
                    <td>
                        Years (TimeSpanUnits.Years)
                    </td>
                    <td>
                        Years (TimeSpanUnits.Years)
                    </td>
                </tr>
            </table>
            <ul>
                <li>UnitLength has a default value of "Short". If long units are specified using "Long",
                    the entire unit name is included. If short units are specified, an abbreviated form
                    of the unit is included. Singular or plural versions of the units are included automatically.
                </li>
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='timeSpanInputBoxWinFormsTab' href=javascript:TabClick('timeSpanInputBoxWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />
                    <img class="controls_border" alt="TimeSpanInputBox WinForms control screenshot" title="TimeSpanInputBox WinForms control screenshot" runat="server" src="~/Components/Images/timespaninputbox.png" />
                    <br />
                    <br />
                    <p>
                        The full source code for this control can be found in the
                        Microsoft Health Common User Interface Toolkit, which can be downloaded from our
                        <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx" target="_blank"
                            title="Link to releases page on the CodePlex site (New Window)">CodePlex</a>
                        site.
                    </p>
                    <!-- Area for Winforms Usage Section -->
                    <asp:Panel ID="WinformsUsage_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WinformsUsage_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WinformsUsage_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                        <p>
                            You can enter a time span directly in the TimeSpanInputBox using:
                        </p>
                        <ul>
                            <li>'y' for years</li>
                            <li>'m' for months</li>
                            <li>'w' for weeks</li>
                            <li>'d' for days</li>
                            <li>'hrs' for hours</li>
                            <li>'min' for minutes</li>
                            <li>'sec' for seconds</li>
                            <li>A combination of these units, with the units listed in the order given</li>
                        </ul>
                        <p>
                            You can optionally include spaces between the different time elements. For example,
                            you could enter:
                        </p>
                        <ul>
                            <li>2y23d14hrs7min, or</li>
                            <li>2y 23d 14hrs 7min</li>
                        </ul>
                    </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WinformsUsage_Extender" runat="Server" TargetControlID="WinformsUsage_ContentPanel"
                        ExpandControlID="WinformsUsage_HeaderPanel" CollapseControlID="WinformsUsage_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="WinformsUsage_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                        SuppressPostBack="true" />
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
                            <li><strong>From</strong> &ndash; the wrapper for Value.From </li>
                            <li><strong>Granularity</strong> &ndash; the wrapper for Value.Granularity </li>
                            <li><strong>IsAge</strong> &ndash; the wrapper for Value.IsAge </li>
                            <li><strong>Threshold</strong> &ndash; the wrapper for Value.Threshold </li>
                            <li><strong>To</strong> &ndash; the wrapper for Value.To </li>
                            <li><strong>Value</strong> &ndash; gets or sets the time span entered in the input box
                            </li>
                            <li><strong>UnitLength</strong> &ndash; specifies whether long or short units are used</li>
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
                                        If a duration control is combined with either date and/or time controls, the values
                                        expressed within the duration control will be dependent on the date/time controls
                                        combined with the duration control, as follows:</p>
                                    <ul>
                                        <li>Duration and Time Control = hrs, min and sec</li>
                                        <li>Duration and Date Control = y, m and d</li>
                                        <li>Duration and Date and Time = y, m, d, hrs, min and sec</li>
                                    </ul>
                                    <p>
                                        &nbsp</p>
                                    <p>
                                        The clinical application:</p>
                                    <ul>
                                        <li>Must allow for the entry of different durations either independently or by using
                                            combinations</li>
                                        <li>Must allow for the editing of the individual elements of the durations entered into
                                            the control</li>
                                        <li>Must automatically calculate the duration when a start date/time and an end date/time
                                            are entered</li>
                                        <li>Must automatically calculate the end date/time when a start date/time and duration
                                            (or vice versa) are entered</li>
                                        <li>Must use <strong>y</strong> for years, <strong>m</strong> for months, <strong>d</strong>
                                            for days, <strong>hrs</strong> for hours, <strong>min</strong> for minutes and <strong>
                                                sec</strong> for seconds</li>
                                    </ul>      
                                     <p>
                                        &nbsp</p>
                                    <p>
                                        If IsAge is:
                                    </p>
                                    <ul>
                                        <li><strong>true</strong>, the Granularity and Threshold property values are set from the time span</li>
                                        <li><strong>false</strong>, the Granularity and Threshold property values are set from the assigned Granularity and Threshold property values</li>
                                    </ul>
                                     <p>
                                        &nbsp</p>
                                    <p>
                                        The following table provides guidance on how the Granularity and Threshold values are set from the time span.
                                    </p>
                                        <p>&nbsp</p> 
                                        <table class="desc">
                                            <thead align="left">
                                                <th width="35%">
                                                    Time span</th>
                                                <th width="35%">
                                                    Granularity</th>
                                                <th width="35%">
                                                    Threshold</th>
                                            </thead>
                                            <tr>
                                                <td>
                                                    Less than 2 hours</td>
                                                <td>
                                                    Minutes (TimeSpanUnits.Minutes)</td>
                                                <td>
                                                    Minutes (TimeSpanUnits.Minutes)</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Less than 2 days</td>
                                                <td>
                                                    Hours (TimeSpanUnits.Hours)</td>
                                                <td>
                                                    Hours (TimeSpanUnits.Hours)</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Less than 4 weeks</td>
                                                <td>
                                                    Days (TimeSpanUnits.Days)</td>
                                                <td>
                                                    Days (TimeSpanUnits.Days)</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Less than 1 year</td>
                                                <td>
                                                    Weeks (TimeSpanUnits.Weeks)</td>
                                                <td>
                                                    Days (TimeSpanUnits.Days)</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Less than 2 years</td>
                                                <td>
                                                    Months (TimeSpanUnits.Months)</td>
                                                <td>
                                                    Days (TimeSpanUnits.Days)</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Less than 18 years</td>
                                                <td>
                                                    Years (TimeSpanUnits.Years)</td>
                                                <td>
                                                    Months (TimeSpanUnits.Months)</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Greater than or equal to 18 years</td>
                                                <td>
                                                    Years (TimeSpanUnits.Years)</td>
                                                <td>
                                                    Years (TimeSpanUnits.Years)</td>
                                            </tr>
                                        </table>
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
            function pageLoad() {
                var theControl = $find("<%=TimeSpanInputBox1.ClientID%>" + '_TimeSpanInputBox1_timeSpanInputBoxExtender');
                var checkBox = $get('isAgeCheckBox');
                checkBox.checked = theControl.get_isAge();
            }
            
            function ToggleIsAge() {
                var theControl = $find("<%=TimeSpanInputBox1.ClientID%>" + '_TimeSpanInputBox1_timeSpanInputBoxExtender');
                theControl.set_isAge(!theControl.get_isAge());
            }
    </script>

</asp:Content>
