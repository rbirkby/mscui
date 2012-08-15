<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsNameLabel" Title="Untitled Page" CodeBehind="NameLabel.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The NameLabel control displays a patient's name in a standard format and layout.
            In the NameLabel control, the FamilyName is capitalized and the Title is given within parentheses.
        </p>
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlightControl" HeaderText="<a id='contactLabelSilverlightTab' href=javascript:TabClick('contactLabelSilverlightTab'); title='Silverlight Tab'>Silverlight</a>">
            <ContentTemplate>
                <br />
                Example Silverlight control (embedded):
                <br />
                <br />
                <object data="data:application/x-silverlight," type="application/x-silverlight-2"
                    width="100%" height="120px">
                    <param name="source" value="../ClientBin/Microsoft.Cui.SamplePages.xap" />
                    <param name="initParams" value="StartPage=NameLabel,AllowResize=False" />
                    <param name="minRuntimeVersion" value="3.0.40818.0" />
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none;">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                            style="border-style: none" />
                    </a>
                </object>
                <iframe style='visibility: hidden; height: 0; width: 0; border: 0px'></iframe>
                <br />
                <br />
                <div>
                    <!-- Area for Section 1 -->
                    <asp:Panel ID="Section1_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="Section1_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Section1_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                            The NameLabel control is initialized with the following code:
                            <pre>
    &lt;sl:NameLabel x:Name="NameLabelControl" GivenName="Thelma" FamilyName="Cordero" Title="Ms" 
    FontFamily="Verdana" FontSize="12" />
        </pre>
                            <pre>
    &lt;sl:NameLabel x:Name="NameLabelControl1" FamilyName="Cordero" Title="Ms" GivenName="" 
    FontFamily="Verdana" FontSize="12" />
        </pre>
                            <ul>
                                <li><strong>DisplayValue</strong> &ndash; gets the correctly-formatted aggregate value
                                    of the patient&rsquo;s name</li>
                                <li><strong>FamilyName</strong> &ndash; gets or sets the family name </li>
                                <li><strong>GivenName</strong> &ndash; gets or sets the given name </li>
                                <li><strong>Title</strong> &ndash; gets or sets the patient's title </li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeSection1" runat="Server" TargetControlID="Section1_ContentPanel"
                        ExpandControlID="Section1_HeaderPanel" CollapseControlID="Section1_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="Section1_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                        SuppressPostBack="true" />
                    <!-- Area for Section 2 -->
                    <asp:Panel ID="Section2_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="Section2_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Additional Information
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Section2_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="last section">
                            <ul>
                                <li><strong>FamilyName</strong> has a default value of "FamilyName"</li>
                                <li><strong>GivenName</strong> has a default value of "GivenName"</li>
                                <li><strong>Title</strong> has default value of "Title"</li>
                            </ul>
                            <p>
                                The maximum displayed length of:</p>
                            <ul>
                                <li>DisplayValue is 120 characters</li>
                                <li>FamilyName is 40 characters</li>
                                <li>GivenName is 40 characters</li>
                                <li>Title is 35 characters </li>
                            </ul>
                            <p>
                                If a data element is assigned data that exceeds these limits, the data should be
                                truncated to:</p>
                            <ul>
                                <li>40 characters plus an ellipsis for FamilyName and GivenName</li>
                                <li>35 characters plus an ellipsis for Title</li>
                            </ul>
                            <p>
                                If partial data is entered, the following display formats are accepted:</p>
                            <ul>
                                <li>FamilyName, GivenName</li>
                                <li>FamilyName</li>
                                <li>FamilyName (Title), with no comma delimit between FamilyName and Title</li>
                                <li>GivenName</li>
                                <li>GivenName (Title)</li>
                            </ul>
                            <p>
                                If the first element to be provided is Title, it should only be displayed in conjunction
                                with either FamilyName or GivenName.
                            </p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeSection2" runat="Server" TargetControlID="Section2_ContentPanel"
                        ExpandControlID="Section2_HeaderPanel" CollapseControlID="Section2_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="Section2_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWPF" HeaderText="<a id='contactLabelWPFTab' href=javascript:TabClick('contactLabelWPFTab'); title='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WPF control (screenshot):
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td>
                                <img id="Img2" class="controls_border" alt="NameLabel WPF control screenshot" title="NameLabel WPF control screenshot" runat="server"
                                    src="~/Components/Images/namelabelA.GIF" />
                            </td>
                            <td>
                              &nbsp;&nbsp;A NameLabel displaying a patient's FamilyName, GivenName and Title.
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img id="Img3" class="controls_border" alt="NameLabel WPF control screenshot" title="NameLabel WPF control screenshot" runat="server"
                                    src="~/Components/Images/namelabelB.GIF" />
                            </td>
                            <td>
                                &nbsp;&nbsp;A NameLabel displaying a patient's FamilyName and Title.
                            </td>
                        </tr>
                    </table>
                    <br />
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
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='nameLabelASPNETTab' href=javascript:TabClick('nameLabelASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Panel CssClass="demoControlarea" ID="demoPanel1" runat="server">
                                &nbsp;
                                <NhsCui:NameLabel ID="NameLabel1" GivenName="Thelma" FamilyName="Cordero" Title="Ms"
                                    runat="server" />
                                &nbsp;
                            </asp:Panel>
                        </td>
                        <td>
                            A NameLabel displaying a patient's FamilyName, GivenName and Title.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel CssClass="demoCCarea" ID="demoPanel2" runat="server">
                                &nbsp;
                                <NhsCui:NameLabel ID="NameLabel2" FamilyName="Cordero" Title="Mr" runat="server" />
                                &nbsp;
                            </asp:Panel>
                        </td>
                        <td>
                            A NameLabel displaying a patient's FamilyName and Title.
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
                        The NameLabel control is initialized with the following code:
                        <pre>
    &lt;NhsCui:NameLabel ID="NameLabel1" runat="server"
    GivenName="Thelma" FamilyName="Cordero" Title="Ms" /&gt;
        </pre>
                        <pre>
    &lt;NhsCui:NameLabel ID="NameLabel2" runat="server"
    FamilyName="Cordero" Title="Mr" /&gt;
        </pre>
                        <ul>
                            <li><strong>DisplayValue</strong> &ndash; gets the correctly-formatted aggregate value
                                of the patient&rsquo;s name</li>
                            <li><strong>FamilyName</strong> &ndash; gets or sets the family name </li>
                            <li><strong>GivenName</strong> &ndash; gets or sets the given name </li>
                            <li><strong>Title</strong> &ndash; gets or sets the patient's title </li>
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
                            <li><strong>FamilyName</strong> has a default value of "FamilyName"</li>
                            <li><strong>GivenName</strong> has a default value of "GivenName"</li>
                            <li><strong>Title</strong> has default value of "Title"</li>
                        </ul>
                        <p>
                            The maximum displayed length of:</p>
                        <ul>
                            <li>DisplayValue is 120 characters</li>
                            <li>FamilyName is 40 characters</li>
                            <li>GivenName is 40 characters</li>
                            <li>Title is 35 characters </li>
                        </ul>
                        <p>
                            If a data element is assigned data that exceeds these limits, the data should be
                            truncated to:</p>
                        <ul>
                            <li>37 characters plus an ellipsis for FamilyName and GivenName</li>
                            <li>32 characters plus an ellipsis for Title</li>
                        </ul>
                        <p>
                            If partial data is entered, the following display formats are accepted:</p>
                        <ul>
                            <li>FamilyName, GivenName</li>
                            <li>FamilyName</li>
                            <li>FamilyName (Title), with no comma delimit between FamilyName and Title</li>
                            <li>GivenName</li>
                            <li>GivenName (Title)</li>
                        </ul>
                        <p>
                            If the first element to be provided is Title, it should only be displayed in conjunction
                            with either FamilyName or GivenName.
                        </p>
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='nameLabelWinFormsTab' href=javascript:TabClick('nameLabelWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td>
                                <img class="controls_border" alt="NameLabel WinForms control screenshot" title="NameLabel WinForms control screenshot" runat="server"
                                    src="~/Components/Images/namelabelA.GIF" />
                            </td>
                            <td>
                                &nbsp;&nbsp;A NameLabel displaying a patient's FamilyName, GivenName and Title.
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img id="Img1" class="controls_border" alt="NameLabel WinForms control screenshot" title="NameLabel WinForms control screenshot"
                                    runat="server" src="~/Components/Images/namelabelB.GIF" />
                            </td>
                            <td>
                                &nbsp;&nbsp;A NameLabel displaying a patient's FamilyName and Title.
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
                            <li><strong>DisplayValue</strong> &ndash; gets the correctly-formatted aggregate value
                                of the patient&rsquo;s name</li>
                            <li><strong>FamilyName</strong> &ndash; gets or sets the family name </li>
                            <li><strong>GivenName</strong> &ndash; gets or sets the given name </li>
                            <li><strong>Title</strong> &ndash; gets or sets the patient's title </li>
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
                                The maximum displayed length of:
                                <ul>
                                    <li>DisplayValue is 120 characters</li>
                                    <li>FamilyName is 40 characters</li>
                                    <li>GivenName is 40 characters</li>
                                    <li>Title is 35 characters</li>
                                </ul>
                            </p>
                            <p>
                                If a data element is assigned data that exceeds these limits, the data should be
                                truncated to:
                                <ul>
                                    <li>37 characters plus an ellipsis for FamilyName and GivenName</li>
                                    <li>32 characters plus an ellipsis for Title</li>
                                </ul>
                            </p>
                            <p>
                                If partial data is entered, the following display formats are accepted:
                                <ul>
                                    <li>FamilyName, GivenName</li>
                                    <li>FamilyName</li>
                                    <li>FamilyName (Title), with no comma delimit between FamilyName and Title</li>
                                    <li>GivenName</li>
                                    <li>GivenName (Title)</li>
                                </ul>
                            </p>
                            <p>
                                If the first element to be provided is Title, it should only be displayed in conjunction
                                with either FamilyName or GivenName.
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
