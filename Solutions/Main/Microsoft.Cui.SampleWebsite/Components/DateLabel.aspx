<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsDateLabel" Title="Untitled Page" CodeBehind="DateLabel.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The DateLabel control lets you display a date in a clear and unambiguous way. The
            DateLabel control may include:
        </p>
        <ul>
            <li>An exact date including the day of the week</li>
            <li>An exact date</li>
            <li>A date marked as approximate</li>
            <li>A month and year</li>
            <li>A year only</li>
            <li>A null index date where you can define the exact text displayed</li>
            <li>A null date</li>
        </ul>
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlight" HeaderText="<a id='dateLabelSilverlightTab' href=javascript:TabClick('dateLabelSilverlightTab'); title='Silverlight Tab' alt='Silverlight Tab'>Silverlight</a>">
            <ContentTemplate>
                <br />
                Example Silverlight control (embedded):
                <br />
                <br />
                <object data="data:application/x-silverlight," type="application/x-silverlight-2"
                    width="100%" height="50px">
                    <param name="source" value="../ClientBin/Microsoft.Cui.SamplePages.xap" />
                    <param name="initParams" value="StartPage=DateLabel,AllowResize=False" />
                    <param name="minRuntimeVersion" value="3.0.40818.0" />
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none;">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                            style="border-style: none" />
                    </a>
                </object>
                <p class="resetFloatAfterdemoCCArea">
                    A DateLabel displaying an exact date.
                </p>
                <!-- Area for Properties -->
                <asp:Panel ID="Properties_HeaderPanel_Silverlight" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="properties_ToggleImage_Silverlight" runat="server" src="~/images/SFTheme/acc_h.png" />
                        Properties
                    </div>
                </asp:Panel>
                <asp:Panel ID="Properties_ContentPanel_Silverlight" runat="server" Style="overflow: hidden;"
                    Height="0px">
                    <div class="section">
                        The DateLabel control is initialized with the following code:
                        <pre>&lt;sl:DateLabel Name="DateLabel1" ToolTip="A DateLabel displaying an exact date" /&gt;</pre>
                        <ul>
                            <li><strong>DateType</strong> &ndash; the date type that represents the date</li>
                            <li><strong>DateValue</strong> &ndash; the exact or approximate date. It is recommended that
                            the property DateValue be used when specifying values in XAML. Example: DateValue="11/11/2008".
                            The Value property cannot be initialized in this manner due to the absence of a custom type 
                            converter that can convert a string to a valid instance of CuiDate</li>
                            <li><strong>DisplayDateAsText</strong> &ndash; specifies whether the text &ldquo;Today&rdquo;,
                                &ldquo;Tomorrow&rdquo; or &ldquo;Yesterday&rdquo; should be displayed in place of
                                the date</li>
                            <li><strong>DisplayDayOfWeek</strong> &ndash; specifies whether the name of the day
                                is displayed with the date</li>
                            <li><strong>Month</strong> &ndash; the month; this is not a reflection of the month
                                of the DateValue and is only relevant if DateType is DateType.YearMonth </li>
                            <li><strong>NullIndex</strong> &ndash; a number identifying a null type where the index
                                has no meaning in and of itself; the meaning is implied by the NullStrings property</li>
                            <li><strong>NullStrings</strong> &ndash; a list of localized strings that identify different
                                types of null index dates</li>
                            <li><strong>Value</strong> &ndash; gets or sets the date to be displayed in the label. The
                            Value property cannot be initialized in XAML (as with the DateValue property) due to the 
                            absence of a custom type converter that can convert a string to a valid instance of CuiDate </li>
                            <li><strong>Year</strong> &ndash; the year; this is not a reflection of the year of
                                the DateValue and is only relevant if DateType is DateType.Year</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpePropertiesSilverlight" runat="Server" TargetControlID="properties_ContentPanel_Silverlight"
                    ExpandControlID="properties_HeaderPanel_Silverlight" CollapseControlID="properties_HeaderPanel_Silverlight"
                    Collapsed="True" ExpandDirection="Vertical" ImageControlID="properties_ToggleImage_Silverlight"
                    ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                    CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                    SuppressPostBack="true" />
                <!-- Area for Additional Info -->
                <asp:Panel ID="AdditionalInfo_HeaderPanel_Silverlight" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="AdditionalInfo_ToggleImage_Silverlight" runat="server" src="~/images/SFTheme/acc_v.png" />
                        Additional Information
                    </div>
                </asp:Panel>
                <asp:Panel ID="AdditionalInfo_ContentPanel_Silverlight" runat="server" Style="overflow: hidden;
                    height: 0px">
                    <div class="last section">
                        <ul>
                            <li><strong>DateType </strong>has a default value of &ldquo;Exact&rdquo;</li>
                            <li><strong>DateValue</strong> has a default value of &ldquo;Now&rdquo;</li>
                            <li><strong>DisplayDateAsText</strong> has a default value of &ldquo;False&rdquo;</li>
                            <li><strong>DisplayDayOfWeek</strong> has a default value of &ldquo;False&rdquo;</li>
                            <li><strong>Month</strong> has a default value of &ldquo;1&rdquo;</li>
                            <li><strong>NullIndex</strong> has a default value of &ldquo;-1&rdquo;</li>
                            <li><strong>NullStrings</strong> has a default value of an empty list of strings</li>
                            <li><strong>Year</strong> has a default value of &ldquo;0&rdquo;</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeAdditionalInfo_Silverlight" runat="Server" TargetControlID="AdditionalInfo_ContentPanel_Silverlight"
                    ExpandControlID="AdditionalInfo_HeaderPanel_Silverlight" CollapseControlID="AdditionalInfo_HeaderPanel_Silverlight"
                    Collapsed="True" ExpandDirection="Vertical" ImageControlID="AdditionalInfo_ToggleImage_Silverlight"
                    ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                    CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                    SuppressPostBack="true" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWPFControl" HeaderText="<a id='dateLabelWPFTab' href=javascript:TabClick('dateLabelWPFTab'); title='WPF Tab' alt='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WPF control (screenshot):
                    <br />
                    <br />                   
                    <img id="Img1" class="controls_border" alt="DateLabel WPF control screenshot" title="DateLabel WPF control screenshot" runat="server"
                        src="~/Components/Images/datelabel.GIF" />
                    <p class="resetFloatAfterdemoCCArea">
                        A DateLabel displaying an exact date.
                    </p>
                <p>
                    The full source code for this control can be found in the
                    Microsoft Health Common User Interface Toolkit, which can be downloaded from our
                    <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx" target="_blank"
                        title="Link to releases page on the CodePlex site (New Window)">CodePlex</a>
                    site.
                </p>
                    <!-- Area for WPF Properties Section -->
                    <asp:Panel ID="WPFProps_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WPFProps_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WPFProps_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                        <ul>
                            <li><strong>DateType</strong> &ndash; the date type that represents the date</li>
                            <li><strong>DateValue</strong> &ndash; the exact or approximate date. It is recommended that
                            the property DateValue be used when specifying values in XAML. Example: DateValue="11/11/2008".
                            The Value property cannot be initialized in this manner due to the absence of a custom type converter
                            that can convert a string to a valid instance of CuiDate</li>
                            <li><strong>DisplayDateAsText</strong> &ndash; specifies whether the text &ldquo;Today&rdquo;,
                                &ldquo;Tomorrow&rdquo; or &ldquo;Yesterday&rdquo; should be displayed in place of
                                the date</li>
                            <li><strong>DisplayDayOfWeek</strong> &ndash; specifies whether the name of the day
                                is displayed with the date</li>
                            <li><strong>Month</strong> &ndash; the month; this is not a reflection of the month
                                of the DateValue and is only relevant if DateType is DateType.YearMonth </li>
                            <li><strong>NullIndex</strong> &ndash; a number identifying a null type where the index
                                has no meaning in and of itself; the meaning is implied by the NullStrings property</li>
                            <li><strong>NullStrings</strong> &ndash; a list of localized strings that identify different
                                types of null index dates</li>
                            <li><strong>Value</strong> &ndash; gets or sets the date to be displayed in the label. The
                            Value property cannot be initialized in XAML (as with the DateValue property) due to the 
                            absence of a custom type converter that can convert a string to a valid instance of CuiDate </li>
                            <li><strong>Year</strong> &ndash; the year; this is not a reflection of the year of
                                the DateValue and is only relevant if DateType is DateType.Year</li>
                        </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WPFProps_Extender" runat="Server" TargetControlID="WPFProps_ContentPanel"
                        ExpandControlID="WPFProps_HeaderPanel" CollapseControlID="WPFProps_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="WPFProps_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                        SuppressPostBack="true" />                
                    <!-- Area for WPF Additional Information Section -->
                    <asp:Panel ID="WPFAddInfo_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WPFAddInfo_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Additional Information
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WPFAddInfo_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="last section">
                            <p>
                                <ul>
                                    <li><strong>DateType </strong>has a default value of &ldquo;Exact&rdquo;</li>
                                    <li><strong>DateValue</strong> has a default value of &ldquo;Now&rdquo;</li>
                                    <li><strong>DisplayDateAsText</strong> has a default value of &ldquo;False&rdquo;</li>
                                    <li><strong>DisplayDayOfWeek</strong> has a default value of &ldquo;False&rdquo;</li>
                                    <li><strong>Month</strong> has a default value of &ldquo;1&rdquo;</li>
                                    <li><strong>NullIndex</strong> has a default value of &ldquo;-1&rdquo;</li>
                                    <li><strong>NullStrings</strong> has a default value of an empty list of strings</li>
                                    <li><strong>Year</strong> has a default value of &ldquo;0&rdquo;</li>
                                </ul>
                            </p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WPFAddInfo_Extender" runat="Server" TargetControlID="WPFAddInfo_ContentPanel"
                        ExpandControlID="WPFAddInfo_HeaderPanel" CollapseControlID="WPFAddInfo_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="WPFAddInfo_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='dateLabelASPNETTab' href=javascript:TabClick('dateLabelASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <asp:Panel CssClass="demoControlarea" ID="demoPanel1" runat="server">
                    <NhsCui:DateLabel ID="DateLabel" runat="server"></NhsCui:DateLabel>
                </asp:Panel>
                <p class="resetFloatAfterdemoCCArea">
                    A DateLabel displaying an exact date.
                </p>
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
                        The DateLabel control is initialized with the following code:
                        <pre>&lt;NhsCui:DateLabel ID="DateLabel1" runat="server"/&gt;</pre>
                        <ul>
                            <li><strong>DateType</strong> &ndash; the date type that represents the date</li>
                            <li><strong>DateValue</strong> &ndash; the exact or approximate date</li>
                            <li><strong>DisplayDateAsText</strong> &ndash; specifies whether the text &ldquo;Today&rdquo;,
                                &ldquo;Tomorrow&rdquo; or &ldquo;Yesterday&rdquo; should be displayed in place of
                                the date</li>
                            <li><strong>DisplayDayOfWeek</strong> &ndash; specifies whether the name of the day
                                is displayed with the date</li>
                            <li><strong>Month</strong> &ndash; the month; this is not a reflection of the month
                                of the DateValue and is only relevant if DateType is DateType.YearMonth </li>
                            <li><strong>NullIndex</strong> &ndash; a number identifying a null type where the index
                                has no meaning in and of itself; the meaning is implied by the NullStrings property</li>
                            <li><strong>NullStrings</strong> &ndash; a list of localized strings that identify different
                                types of null index dates</li>
                            <li><strong>Value</strong> &ndash; gets or sets the date to be displayed in the label</li>
                            <li><strong>Year</strong> &ndash; the year; this is not a reflection of the year of
                                the DateValue and is only relevant if DateType is DateType.Year</li>
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
                            <li><strong>DateType </strong>has a default value of &ldquo;Exact&rdquo;</li>
                            <li><strong>DateValue</strong> has a default value of &ldquo;Now&rdquo;</li>
                            <li><strong>DisplayDateAsText</strong> has a default value of &ldquo;False&rdquo;</li>
                            <li><strong>DisplayDayOfWeek</strong> has a default value of &ldquo;False&rdquo;</li>
                            <li><strong>Month</strong> has a default value of &ldquo;1&rdquo;</li>
                            <li><strong>NullIndex</strong> has a default value of &ldquo;-1&rdquo;</li>
                            <li><strong>NullStrings</strong> has a default value of an empty list of strings</li>
                            <li><strong>Year</strong> has a default value of &ldquo;1&rdquo;</li>
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='dateLabelWinFormsTab' href=javascript:TabClick('dateLabelWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />                   
                    <img class="controls_border" alt="DateLabel WinForms control screenshot" title="DateLabel WinForms control screenshot" runat="server"
                        src="~/Components/Images/datelabel.GIF" />
                    <p class="resetFloatAfterdemoCCArea">
                        A DateLabel displaying an exact date.
                    </p>
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
                            <li><strong>DateType</strong> &ndash; the date type that represents the date</li>
                            <li><strong>DateValue</strong> &ndash; the exact or approximate date</li>
                            <li><strong>DisplayDateAsText</strong> &ndash; specifies whether the text &ldquo;Today&rdquo;,
                                &ldquo;Tomorrow&rdquo; or &ldquo;Yesterday&rdquo; should be displayed in place of
                                the date</li>
                            <li><strong>DisplayDayOfWeek</strong> &ndash; specifies whether the name of the day
                                is displayed with the date</li>
                            <li><strong>Month</strong> &ndash; the month; this is not a reflection of the month
                                of the DateValue and is only relevant if DateType is DateType.YearMonth </li>
                            <li><strong>NullIndex</strong> &ndash; a number identifying a null type where the index
                                has no meaning in and of itself; the meaning is implied by the NullStrings property</li>
                            <li><strong>NullStrings</strong> &ndash; a list of localized strings that identify different
                                types of null index dates</li>
                            <li><strong>Value</strong> &ndash; gets or sets the date to be displayed in the label</li>
                            <li><strong>Year</strong> &ndash; the year; this is not a reflection of the year of
                                the DateValue and is only relevant if DateType is DateType.Year</li>
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
                                    <li>DateType has a default value of Exact</li>
                                    <li>DateValue has a default value of Now</li>
                                    <li>DisplayDateAsText has a default value of False</li>
                                    <li>DisplayDayOfWeek -- has a default value of True</li>
                                    <li>Month has a default value of 1</li>                
                                    <li>Year has a default value of 1</li>
                                    <li>NullStrings has a default value of an empty list of strings</li>
                                    <li>NullIndex has a default value of -1</li>
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
</asp:Content>
