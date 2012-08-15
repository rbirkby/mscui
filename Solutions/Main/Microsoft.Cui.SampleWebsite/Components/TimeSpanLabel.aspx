<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsTimeSpanLabel" Title="Untitled Page" CodeBehind="TimeSpanLabel.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The TimeSpanLabel control lets you display a label representing a span between two
            times such as an age. The label may include units of years, months, weeks, days,
            hours, minutes and seconds. The units may be expressed in long or short units, with
            use of plural or singular units determined automatically. The following table provides
            information on the long and short unit versions and their singular and plural forms.
        </p>
        <table class="desc">
            <tr>
                <th align="left" width="35%">
                    Long units (singular)
                </th>
                <td>
                    Year
                </td>
                <td>
                    Month
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
                <th align="left" width="35%">
                    Long units (plural)
                </th>
                <td>
                    Years
                </td>
                <td>
                    Months
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
                <th align="left" width="35%">
                    Short units (singular)
                </th>
                <td>
                    Y
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
                <th align="left" width="35%">
                    Short units (plural)
                </th>
                <td>
                    Y
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
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='timeSpanLabelASPNETTab' href=javascript:TabClick('timeSpanLabelASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <table summary="Table showing examples of various TimeSpanLabel configurations">
                    <col width="50%" />
                    <col width="50%" />
                    <tr>
                        <td>
                            <asp:Panel CssClass="demoCCarea" ID="demoPanel1" runat="server">
                                <NhsCui:TimeSpanLabel ID="TimeSpanLabel" runat="server" To="13 Sep 2010" UnitLength="Short">
                                </NhsCui:TimeSpanLabel>
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel CssClass="demoCCarea" ID="demoPanel2" runat="server">
                                <NhsCui:TimeSpanLabel ID="TimeSpanLabel1" runat="server" To="13 Sep 2010" UnitLength="Long">
                                </NhsCui:TimeSpanLabel>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            A TimeSpanLabel displaying a time span calculated from a To value of the 13th of
                            September 2010 and using short units.
                        </td>
                        <td valign="top">
                            A TimeSpanLabel displaying a time span calculated from a To value of the 13th of
                            September 2010 and using long units.
                        </td>
                    </tr>
                </table>
                <br />
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
                        The TimeSpanLabel control is initialized with the following code:
                        <pre>&lt;NhsCui:TimeSpanLabel ID="TimeSpanLabel1" runat="server"
                To="13 Sep 2010" UnitLength="Short" /&gt;
            </pre>
                        <ul>
                            <li><strong>From</strong> &ndash; gets or sets the date and time which mark the start
                                of the time span</li>
                            <li><strong>IsAge</strong> &ndash; specifies whether the time span represents an age</li>
                            <li><strong>Granularity</strong> &ndash; specifies the largest units to be displayed;
                                its value depends on the status of IsAge and may be seconds, minutes, hours, days,
                                weeks, months or years</li>
                            <li><strong>Threshold</strong> &ndash; specifies the smallest units to be displayed;
                                its value depends on the status of IsAge and may be seconds, minutes, hours, days,
                                weeks, months or years </li>
                            <li><strong>To</strong> &ndash; gets or sets the date and time which mark the end of
                                the time span</li>
                            <li><strong>UnitLength</strong> &ndash; indicates whether short or long units should
                                be used</li>
                            <li><strong>Value</strong> &ndash; gets or sets the value to be displayed by the label</li>
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
                        <p>
                            If IsAge is:
                        </p>
                        <ul>
                            <li>&ldquo;True&rdquo;, the Granularity and Threshold property values are set from the
                                time span</li>
                            <li>&ldquo;False&rdquo;, the Granularity and Threshold property values are set from
                                the assigned Granularity and Threshold property values</li>
                        </ul>
                        The following table provides guidance on how the Granularity and Threshold values
                        are set from the time span. &nbsp
                        <table class="desc">
                            <thead align="left">
                                <tr>
                                    <th width="35%">
                                        Time span
                                    </th>
                                    <th width="35%">
                                        Granularity
                                    </th>
                                    <th width="35%">
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='timeSpanLabelWinFormsTab' href=javascript:TabClick('timeSpanLabelWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
               <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />                    
                    <table summary="Table showing examples of various TimeSpanLabel configurations">
                    <col width="50%" />
                    <col width="50%" />
                    <tr>
                        <td>
                            <img class="controls_border" alt="TimeSpanLabel WinForms control screenshot" title="TimeSpanLabel WinForms control screenshot" runat="server" src="~/Components/Images/timespanlabelA.GIF" />
                        </td>
                        <td>
                            <img class="controls_border" alt="TimeSpanLabel WinForms control screenshot" title="TimeSpanLabel WinForms control screenshot" runat="server" src="~/Components/Images/timespanlabelB.GIF" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            A TimeSpanLabel displaying a time span calculated from a To value of the 13th of
                            September 2010 and using short units.
                        </td>
                        <td valign="top">
                            A TimeSpanLabel displaying a time span calculated from a To value of the 13th of
                            September 2010 and using long units.
                        </td>
                    </tr>
                </table>
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
                            <li><strong>From</strong> &ndash; gets or sets the date and time which mark the start
                                of the time span</li>
                            <li><strong>IsAge</strong> &ndash; specifies whether the time span represents an age</li>
                            <li><strong>Granularity</strong> &ndash; specifies the largest units to be displayed;
                                its value depends on the status of IsAge and may be seconds, minutes, hours, days,
                                weeks, months or years</li>
                            <li><strong>Threshold</strong> &ndash; specifies the smallest units to be displayed;
                                its value depends on the status of IsAge and may be seconds, minutes, hours, days,
                                weeks, months or years </li>
                            <li><strong>To</strong> &ndash; gets or sets the date and time which mark the end of
                                the time span</li>
                            <li><strong>UnitLength</strong> &ndash; indicates whether short or long units should
                                be used</li>
                            <li><strong>Value</strong> &ndash; gets or sets the value to be displayed by the label</li>
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
                                    If IsAge is:
                                    <ul>
                                        <li>true, the Granularity and Threshold property values are set from the time span</li>
                                        <li>false, the Granularity and Threshold property values are set from the assigned Granularity
                                            and Threshold property values</li>
                                    </ul>
                                </p>
                                <p>
                                    The following table provides guidance on how the Granularity and Threshold values
                                    are set from the time span.</p>
                                <p>
                                    <table>
                                        <thead>
                                            <tr>
                                                <th>
                                                    Time Span
                                                </th>
                                                <th>
                                                    Granularity
                                                </th>
                                                <th>
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
</asp:Content>
