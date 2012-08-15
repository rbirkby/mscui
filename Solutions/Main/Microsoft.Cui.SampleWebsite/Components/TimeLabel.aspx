<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsTimeLabel" Title="Untitled Page" CodeBehind="TimeLabel.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The TimeLabel control lets you display a time in a clear and unambiguous way. The
            TimeLabel control may display:
        </p>
        <ul>
            <li>An exact time</li>
            <li>A time marked as approximate</li>
            <li>A null index time where the exact text displayed can be defined by the Independent Software Vendor. For example, a time marked as "Ongoing".</li>
        </ul>
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlight" HeaderText="<a id='timeLabelSilverlightTab' href=javascript:TabClick('timeLabelSilverlightTab'); title='Silverlight Tab' alt='Silverlight Tab'>Silverlight</a>">
            <ContentTemplate>
                <br />
                Example Silverlight control (embedded):
                <br />
                <br />
                <object data="data:application/x-silverlight," type="application/x-silverlight-2"
                    width="100%" height="200px">
                    <param name="source" value="../ClientBin/Microsoft.Cui.SamplePages.xap" />
                    <param name="initParams" value="StartPage=TimeLabel,AllowResize=False" />
                    <param name="minRuntimeVersion" value="3.0.40818.0" />
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none;">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                            style="border-style: none" />
                    </a>
                </object>
                <iframe style='visibility: hidden; height: 0; width: 0; border: 0px'></iframe>
                <br />
                <!-- Area for Properties -->
                <asp:Panel ID="properties_HeaderPanel_silverlight" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="properties_silverlight_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                        Properties
                    </div>
                </asp:Panel>
                <asp:Panel ID="properties_ContentPanel_silverlight" runat="server" Style="overflow: hidden;"
                    Height="0px">
                    <div class="section">
                        The TimeLabel control is initialized with the following code:
                        <p>
                        </p>
                        <pre>&lt;sl:TimeLabel x:Name="TimeLabelControl" DisplaySeconds="False" Display12Hour="False" 
        DisplayAMPM="False" ToolTip="A TimeLabel displaying an exact time" TimeType="Exact" 
        TimeValue="13:15" /&gt;
                        </pre>
                        <ul>
                            <li><strong>DisplayAMPM</strong> &ndash; specifies whether an AM/PM suffix should be
                                included</li>
                            <li><strong>Display12Hour</strong> &ndash; specifies whether the hours value should
                                be displayed in 12-hour or 24-hour format</li>
                            <li><strong>DisplaySeconds</strong> &ndash; specifies whether seconds should be displayed</li>
                            <li><strong>NullIndex</strong> &ndash; a number identifying a null type where the index
                                has no meaning in and of itself; the meaning is implied by the NullStrings property</li>
                            <li><strong>NullStrings</strong> &ndash; a list of localized strings that identify different
                                types of null index times</li>
                            <li><strong>TimeType</strong> &ndash; the time type that represents the time</li>
                            <li><strong>TimeValue</strong> &ndash; the exact or approximate time. It is recommended that
                            the property TimeValue be used when specifying values in XAML. Example: TimeValue="11:30:20".
                            The Value property cannot be initialized in this manner due to the absence of a custom type 
                            converter that can convert a string to a valid instance of CuiTime</li>
                            <li><strong>ToolTip</strong> &ndash; the text displayed when the mouse pointer hovers
                                over the control </li>
                            <li><strong>Value</strong> &ndash; the time to be displayed in the label. The
                            Value property cannot be initialized in XAML (as with the TimeValue property) due to the 
                            absence of a custom type converter that can convert a string to a valid instance of CuiTime</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server"
                    TargetControlID="properties_ContentPanel_silverlight" ExpandControlID="properties_HeaderPanel_silverlight"
                    CollapseControlID="properties_HeaderPanel_silverlight" Collapsed="True" ExpandDirection="Vertical"
                    ImageControlID="properties_silverlight_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                    ExpandedText="Click to collapse the Properties section" CollapsedImage="~/images/SFTheme/acc_h.png"
                    CollapsedText="Click to expand the Properties section" SuppressPostBack="true" />
                <!-- Area for Additional Info -->
                <asp:Panel ID="AdditionalInfo_HeaderPanel_silverlight" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="AdditionalInfo_silverlight_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                        Additional Information
                    </div>
                </asp:Panel>
                <asp:Panel ID="AdditionalInfo_ContentPanel_silverlight" runat="server" Style="overflow: hidden;
                    height: 0px">
                    <div class="last section">
                        <ul>
                            <li><strong>DisplayAMPM</strong> has a default value of &ldquo;False&rdquo;</li>
                            <li><strong>Display12Hour</strong> has a default value of &ldquo;False&rdquo; </li>
                            <li><strong>DisplaySeconds</strong> has a default value of &ldquo;False&rdquo; </li>
                            <li><strong>NullIndex</strong> has a default value of &ldquo;-1&rdquo;</li>
                            <li><strong>NullStrings</strong> has a default value of an empty list of strings</li>
                            <li><strong>TimeType </strong>has a default value of &ldquo;Exact&rdquo;</li>
                            <li><strong>TimeValue</strong> has a default value of &ldquo;Now&rdquo;</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="Server"
                    TargetControlID="AdditionalInfo_ContentPanel_silverlight" ExpandControlID="AdditionalInfo_HeaderPanel_silverlight"
                    CollapseControlID="AdditionalInfo_HeaderPanel_silverlight" Collapsed="True" ExpandDirection="Vertical"
                    ImageControlID="AdditionalInfo_silverlight_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                    ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                    CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWPFControl" HeaderText="<a id='timeLabelWPFTab' href=javascript:TabClick('timeLabelWPFTab'); title='WinForms Tab' alt='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WPF control (screenshot):
                    <br />
                    <br />
                    <table>
                        <col width="30%" />
                        <col width="70%" />
                        <tbody>
                            <tr>
                                <td>
                                    <img id="Img5" alt="TimeLabel WinForms control screenshot"
                                        title="TimeLabel WPF control screenshot" runat="server" src="~/Components/Images/TimeLabelA_WPF.GIF" />
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying an exact time.</p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img id="Img6" alt="TimeLabel WinForms control screenshot"
                                        title="TimeLabel WPF control screenshot" runat="server" src="~/Components/Images/TimeLabelB_WPF.GIF" />
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying an approximate time.
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img id="Img7" alt="TimeLabel WinForms control screenshot"
                                        title="TimeLabel WPF control screenshot" runat="server" src="~/Components/Images/TimeLabelC_WPF.GIF" />
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying an exact time, including seconds.
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img id="Img8" alt="TimeLabel WinForms control screenshot"
                                        title="TimeLabel WPF control screenshot" runat="server" src="~/Components/Images/TimeLabelD_WPF.GIF" />
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying a null index time.
                                    </p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <p>
                        Further information on this control can be found on the Silverlight tab above. The full source code for this control can be found in the Microsoft Health Common
                        User Interface Toolkit, which can be downloaded from our <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx"
                            target="_blank" title="Link to releases page on the CodePlex site (New Window)">
                            CodePlex</a> site.
                    </p>
                    <!-- Area for WPF Properties Section -->
                    <asp:Panel ID="WPFProps_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WPFProps_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WPFProps_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <ul>
                            <li><strong>DisplayAMPM</strong> &ndash; specifies whether an AM/PM suffix should be
                                included</li>
                            <li><strong>Display12Hour</strong> &ndash; specifies whether the hours value should
                                be displayed in 12-hour or 24-hour format</li>
                            <li><strong>DisplaySeconds</strong> &ndash; specifies whether seconds should be displayed</li>
                            <li><strong>NullIndex</strong> &ndash; a number identifying a null type where the index
                                has no meaning in and of itself; the meaning is implied by the NullStrings property</li>
                            <li><strong>NullStrings</strong> &ndash; a list of localized strings that identify different
                                types of null index times</li>
                            <li><strong>TimeType</strong> &ndash; the time type that represents the time</li>
                            <li><strong>TimeValue</strong> &ndash; the exact or approximate time. It is recommended that
                            the property TimeValue be used when specifying values in XAML. Example: TimeValue="11:30:20".
                            The Value property cannot be initialized in this manner due to the absence of a custom type 
                            converter that can convert a string to a valid instance of CuiTime</li>
                            <li><strong>ToolTip</strong> &ndash; the text displayed when the mouse pointer hovers
                                over the control </li>
                            <li><strong>Value</strong> &ndash; the time to be displayed in the label. The
                            Value property cannot be initialized in XAML (as with the TimeValue property) due to the 
                            absence of a custom type converter that can convert a string to a valid instance of CuiTime</li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WPFProps_Extender" runat="Server"
                        TargetControlID="WPFProps_ContentPanel" ExpandControlID="WPFProps_HeaderPanel"
                        CollapseControlID="WPFProps_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="WPFProps_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Properties section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Properties section" SuppressPostBack="true" />
                    <!-- Area for WPF Additional Information Section -->
                    <asp:Panel ID="WPFAddInfo_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WPFAddInfo_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Additional Information
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WPFAddInfo_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="last section">
                            <ul>
                            <li><strong>DisplayAMPM</strong> has a default value of &ldquo;False&rdquo;</li>
                            <li><strong>Display12Hour</strong> has a default value of &ldquo;False&rdquo; </li>
                            <li><strong>DisplaySeconds</strong> has a default value of &ldquo;False&rdquo; </li>
                            <li><strong>NullIndex</strong> has a default value of &ldquo;-1&rdquo;</li>
                            <li><strong>NullStrings</strong> has a default value of an empty list of strings</li>
                            <li><strong>TimeType </strong>has a default value of &ldquo;Exact&rdquo;</li>
                            <li><strong>TimeValue</strong> has a default value of &ldquo;Now&rdquo;</li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WPFAddInfo_Extender" runat="Server"
                        TargetControlID="WPFAddInfo_ContentPanel" ExpandControlID="WPFAddInfo_HeaderPanel"
                        CollapseControlID="WPFAddInfo_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="WPFAddInfo_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='timeLabelASPNETTab' href=javascript:TabClick('timeLabelASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <asp:Panel ID="demoPanel1" runat="server">
                    <table>
                        <col width="30%" />
                        <col width="70%" />
                        <tbody>
                            <tr>
                                <td>
                                    <div class="demoControlarea">
                                        <NhsCui:TimeLabel ID="TimeLabel" TimeValue="13:15" runat="server" />
                                    </div>
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying an exact time.</p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="demoControlarea">
                                        <NhsCui:TimeLabel ID="TimeLabel1" TimeType="Approximate" TimeValue="13:15" runat="server" />
                                    </div>
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying an approximate time.
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="demoControlarea">
                                        <NhsCui:TimeLabel ID="TimeLabel2" DisplaySeconds="true" Display12Hour="true" DisplayAMPM="true"
                                            TimeValue="13:15" runat="server" />
                                    </div>
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying an exact time, including seconds, in 12-hour clock format
                                        with a PM indicator.
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="demoControlarea">
                                        <NhsCui:TimeLabel ID="TimeLabel3" NullStrings="Ongoing" TimeType="NullIndex" NullIndex="0"
                                            runat="server" />
                                    </div>
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying a null index time.
                                    </p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
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
                        The TimeLabel control is initialized with the following code:
                        <p>
                        </p>
                        <pre>&lt;NhsCui:TimeLabel ID="TimeLabel1" TimeValue="13:15" runat="server"/&gt;
                        </pre>
                        <ul>
                            <li><strong>DisplayAMPM</strong> &ndash; specifies whether an AM/PM suffix should be
                                included</li>
                            <li><strong>Display12Hour</strong> &ndash; specifies whether the hours value should
                                be displayed in 12-hour or 24-hour format</li>
                            <li><strong>DisplaySeconds</strong> &ndash; specifies whether seconds should be displayed</li>
                            <li><strong>NullIndex</strong> &ndash; a number identifying a null type where the index
                                has no meaning in and of itself; the meaning is implied by the NullStrings property</li>
                            <li><strong>NullStrings</strong> &ndash; a list of localized strings that identify different
                                types of null index times</li>
                            <li><strong>ToolTip</strong> &ndash; the text displayed when the mouse pointer hovers
                                over the Web server control </li>
                            <li><strong>Value</strong> &ndash; the time to be displayed in the label</li>
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
                            <li><strong>DisplayAMPM</strong> has a default value of &ldquo;False&rdquo;</li>
                            <li><strong>Display12Hour</strong> has a default value of &ldquo;False&rdquo; </li>
                            <li><strong>DisplaySeconds</strong> has a default value of &ldquo;False&rdquo; </li>
                            <li><strong>NullStrings</strong> has a default value of an empty list of strings</li>
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='timeLabelWinFormsTab' href=javascript:TabClick('timeLabelWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />
                    <table>
                        <col width="30%" />
                        <col width="70%" />
                        <tbody>
                            <tr>
                                <td>
                                    <img id="Img1" class="controls_border" alt="TimeLabel WinForms control screenshot"
                                        title="TimeLabel WinForms control screenshot" runat="server" src="~/Components/Images/timelabelA.GIF" />
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying an exact time.</p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img id="Img2" class="controls_border" alt="TimeLabel WinForms control screenshot"
                                        title="TimeLabel WinForms control screenshot" runat="server" src="~/Components/Images/timelabelB.GIF" />
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying an approximate time.
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img id="Img3" class="controls_border" alt="TimeLabel WinForms control screenshot"
                                        title="TimeLabel WinForms control screenshot" runat="server" src="~/Components/Images/timelabelC.GIF" />
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying an exact time, including seconds.
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img id="Img4" class="controls_border" alt="TimeLabel WinForms control screenshot"
                                        title="TimeLabel WinForms control screenshot" runat="server" src="~/Components/Images/timelabelD.GIF" />
                                </td>
                                <td>
                                    <p>
                                        A TimeLabel displaying a null index time.
                                    </p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <p>
                        The full source code for this control can be found in the Microsoft Health Common
                        User Interface Toolkit, which can be downloaded from our <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx"
                            target="_blank" title="Link to releases page on the CodePlex site (New Window)">
                            CodePlex</a> site.
                    </p>
                    <!-- Area for Winforms Properties Section -->
                    <asp:Panel ID="WinformsProps_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WinformsProps_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WinformsProps_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <ul>
                                <li><strong>DisplayAMPM</strong> &ndash; specifies whether an AM/PM suffix should be
                                    included</li>
                                <li><strong>Display12Hour</strong> &ndash; specifies whether the hours value should
                                    be displayed in 12-hour or 24-hour format</li>
                                <li><strong>DisplaySeconds</strong> &ndash; specifies whether seconds should be displayed</li>
                                <li><strong>NullIndex</strong> &ndash; a number identifying a null type where the index
                                    has no meaning in and of itself; the meaning is implied by the NullStrings property</li>
                                <li><strong>NullStrings</strong> &ndash; a list of localized strings that identify different
                                    types of null index times</li>
                                <li><strong>ToolTip</strong> &ndash; the text displayed when the mouse pointer hovers
                                    over the control </li>
                                <li><strong>Value</strong> &ndash; the time to be displayed in the label</li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WinformsProps_Extender" runat="Server"
                        TargetControlID="WinformsProps_ContentPanel" ExpandControlID="WinformsProps_HeaderPanel"
                        CollapseControlID="WinformsProps_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="WinformsProps_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Properties section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Properties section" SuppressPostBack="true" />
                    <!-- Area for Winforms Additional Information Section -->
                    <asp:Panel ID="WinformsAddInfo_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WinformsAddInfo_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Additional Information
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WinformsAddInfo_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="last section">
                            <ul>
                                <li>TimeType has a default value of Exact</li>
                                <li>TimeValue has a default value of Now</li>
                                <li>NullStrings has a default value of an empty list of strings</li>
                                <li>DisplaySeconds has a default value of False</li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WinformsAddInfo_Extender" runat="Server"
                        TargetControlID="WinformsAddInfo_ContentPanel" ExpandControlID="WinformsAddInfo_HeaderPanel"
                        CollapseControlID="WinformsAddInfo_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="WinformsAddInfo_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
