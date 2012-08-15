<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsGenderLabel" Title="Untitled Page" CodeBehind="GenderLabel.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The GenderLabel control is used to display the patient&rsquo;s gender. If the patient&rsquo;s
            gender is not known, &ldquo;NotKnown&rdquo; may be used.
        </p>
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlightControl" HeaderText="<a id='genderLabelSilverlightTab' href=javascript:TabClick('genderLabelSilverlightTab'); title='Silverlight Tab'>Silverlight</a>">
            <ContentTemplate>
                <br />
                Example Silverlight control (embedded):
                <br />
                <br />
                <object data="data:application/x-silverlight," type="application/x-silverlight-2"
                    width="100%" height="50px">
                    <param name="source" value="../ClientBin/Microsoft.Cui.SamplePages.xap" />
                    <param name="initParams" value="StartPage=GenderLabel,AllowResize=False" />
                    <param name="minRuntimeVersion" value="3.0.40818.0" />
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none;">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                            style="border-style: none" />
                    </a>
                </object>
                <iframe style='visibility: hidden; height: 0; width: 0; border: 0px'></iframe>
                <p class="resetFloatAfterdemoCCArea">
                    A GenderLabel displaying a patient&rsquo;s gender.
                </p>
                <div>
                    <!-- Area for Section 1 -->
                    <asp:Panel ID="Section1_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="Section1_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Section1_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="last section">
                            The GenderLabel control is initialized with the following code:
                            <pre>&lt;sl:GenderLabel x:Name="GenderLabelControl" Value="Female" /></pre>
                            <ul>
                                <li><strong>Value</strong> &ndash; gets or sets the patient&rsquo;s gender; this may
                                    be set to &ldquo;Female&rdquo;, &ldquo;Male&rdquo;, &ldquo;NotSpecified&rdquo; or
                                    &ldquo;NotKnown&rdquo;</li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeSection1" runat="Server" TargetControlID="Section1_ContentPanel"
                        ExpandControlID="Section1_HeaderPanel" CollapseControlID="Section1_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="Section1_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                        SuppressPostBack="true" />
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWPF" HeaderText="<a id='genderLabelWPFTab' href=javascript:TabClick('genderLabelWPFTab'); title='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WPF control (screenshot):
                    <br />
                    <br />
                    <img id="Img1" class="controls_border" alt="GenderLabel WPF control screenshot" title="GenderLabel WPF control screenshot"
                        runat="server" src="~/Components/Images/genderlabel.GIF" />
                    <p class="resetFloatAfterdemoCCArea">
                        A GenderLabel displaying a patient&rsquo;s gender.
                    </p>
                    <p>
                        Further information on this control can be found on the Silverlight tab above.
                        The full source code can be found in the Microsoft Health Common User Interface Toolkit,
                        which can be downloaded from our
                        <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx" target="_blank"
                            title="Link to releases page on the CodePlex site (New Window)">CodePlex</a>
                        site.
                    </p>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='genderLabelASPNETTab' href=javascript:TabClick('genderLabelASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Panel CssClass="demoControlarea" ID="demoPanel1" runat="server">
                                <NhsCui:GenderLabel ID="FemaleGenderLabel" runat="server" Value="Female"></NhsCui:GenderLabel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <p class="resetFloatAfterdemoCCArea">
                    A GenderLabel displaying a patient&rsquo;s gender.
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
                    <div class="last section">
                        The GenderLabel control is initialized with the following code:
                        <pre>&lt;NhsCui:GenderLabel ID="FemaleGenderLabel" runat="server" Value="Female"/&gt;</pre>
                        <ul>
                            <li><strong>Value</strong> &ndash; gets or sets the patient&rsquo;s gender; this may
                                be set to &ldquo;Female&rdquo;, &ldquo;Male&rdquo;, &ldquo;NotSpecified&rdquo; or
                                &ldquo;NotKnown&rdquo;</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="properties_ContentPanel"
                    ExpandControlID="properties_HeaderPanel" CollapseControlID="properties_HeaderPanel"
                    Collapsed="True" ExpandDirection="Vertical" ImageControlID="properties_ToggleImage"
                    ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                    CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                    SuppressPostBack="true" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='genderLabelWinFormsTab' href=javascript:TabClick('genderLabelWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />
                    <img class="controls_border" alt="GenderLabel WinForms control screenshot" title="GenderLabel WinForms control screenshot"
                        runat="server" src="~/Components/Images/genderlabel.GIF" />
                    <p class="resetFloatAfterdemoCCArea">
                        A GenderLabel displaying a patient&rsquo;s gender.
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
                            <li><strong>Value</strong> &ndash; gets or sets the patient&rsquo;s gender; this may
                                be set to &ldquo;Female&rdquo;, &ldquo;Male&rdquo;, &ldquo;NotSpecified&rdquo; or
                                &ldquo;NotKnown&rdquo;</li>
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
                                    <li>Value -- gets or sets the patient's gender; this may be set to Female, Male, NotSpecified or NotKnown</li>
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
