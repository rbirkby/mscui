<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsAddressLabel" Title="Untitled Page" CodeBehind="AddressLabel.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="defaultContent" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The AddressLabel control displays an address in an in-form (vertical) or in-line
            (horizontal) format. In the in-form format, each line contains a single left-justified
            address element; no separator characters should display. In the in-line format,
            multiple elements display on a single line, with address elements separated by a
            single comma and a single space. Individual address elements should not split across
            multiple lines.
        </p>
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlightControl" HeaderText="<a id='addressLabelSilverlightTab' href=javascript:TabClick('addressLabelSilverlightTab'); title='Silverlight Tab'>Silverlight</a>">
            <ContentTemplate>
                <br />               
                Example Silverlight control (embedded):               
                <br />
                <object data="data:application/x-silverlight," type="application/x-silverlight-2"
                    width="100%" height="170px">
                    <param name="source" value="../ClientBin/Microsoft.Cui.SamplePages.xap" />
                    <param name="initParams" value="StartPage=AddressLabel,AllowResize=False" />
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
                            <p>
                                The AddressLabel control is initialized with the following code:
                            </p>
                            <pre>&lt;sl:AddressLabel Address1="98 Andover Place" Town="Reading" County="Berkshire" Postcode="rg3 5ap" 
                        AddressDisplayFormat="InForm" Width="200" FontFamily="Verdana" FontSize="14" 
                        FontWeight="Bold" HorizontalAlignment="Left"/> 
                        </pre>
                            <ul>
                                <li><strong>Address1</strong> &ndash; gets or sets the first line of an address</li>
                                <li><strong>Address2</strong> &ndash; gets or sets the second line of an address</li>
                                <li><strong>Address3</strong> &ndash; gets or sets the third line of an address</li>
                                <li><strong>Town</strong> &ndash; gets or sets the town in an address</li>
                                <li><strong>County</strong> &ndash; gets or sets the county in an address</li>
                                <li><strong>Postcode</strong> &ndash; gets or sets the postcode</li>
                                <li><strong>Country</strong> &ndash; gets or sets the country in an address</li>
                                <li><strong>AddressDisplayFormat</strong> &ndash; gets or sets the address layout to
                                    be either in-form (vertical) or in-line (horizontal)</li>
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
                                <li>All stored address data should display. However, if an address element is empty,
                                    it will not display. For example, in the code sample above, the Country property
                                    is not defined and so is not displayed in the AddressLabel</li>
                                <li>The Postcode property should always display in capitalized form and as the final
                                    element before the Country property</li>
                            </ul>
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWPF" HeaderText="<a id='addressLabelWPFTab' href=javascript:TabClick('addressLabelWPFTab'); title='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <br />
                Example WPF control (screenshot):
                <br />
                <br />
                <table>
                    <tr>
                        <td>
                            <img id="Img1" class="controls_border" title="AddressLabel WPF control screenshot"
                                runat="server" src="~/Components/Images/addresslabelA.gif" />
                        </td>
                        <td>
                            The AddressLabel using the in-form (vertical) layout format.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img id="Img2" class="controls_border" title="AddressLabel WPF control screenshot"
                                runat="server" src="~/Components/Images/addresslabelB.gif" />
                        </td>
                        <td>
                            The AddressLabel using the in-line (horizontal) layout format.
                        </td>
                    </tr>
                </table>
                <p>
                    Further information on this control can be found on the Silverlight tab above.
                    The full source code can be found in the Microsoft Health Common User Interface Toolkit,
                    which can be downloaded from our
                    <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx" target="_blank"
                        title="Link to releases page on the CodePlex site (New Window)">CodePlex</a>
                    site.
                </p>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='addressLabelASPNETTab' href=javascript:TabClick('addressLabelASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Panel CssClass="demoCCarea" ID="demoPanel1" runat="server" Width="450px">
                                <NhsCui:AddressLabel Style="margin: 0px; padding: 0px; margin-left: auto; margin-right: auto;
                                    width: 130px" ID="AddressLabel1" Address1="98 Andover Place" Town="Reading" County="Berkshire"
                                    Postcode="RG3 5AP" AddressDisplayFormat="InForm" runat="server" CssClass="AddressLabel"
                                    AddressTypeStyle="Label" Font-Bold="true" />
                            </asp:Panel>
                        </td>
                        <td>
                            The AddressLabel using the in-form (vertical) layout format.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel CssClass="demoCCarea" ID="demoPanel2" runat="server" Width="450px">
                                <NhsCui:AddressLabel ID="AddressLabel2" Address1="98 Andover Place" Town="Reading"
                                    County="Berkshire" Postcode="RG3 5AP" AddressDisplayFormat="InLine" runat="server"
                                    CssClass="AddressLabel" AddressTypeStyle="Label" Font-Bold="true" />
                            </asp:Panel>
                        </td>
                        <td>
                            The AddressLabel using the in-line (horizontal) layout format.
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
                        <p>
                            The AddressLabel control is initialized with the following code:
                        </p>
                        <pre>&lt;NhsCui:AddressLabel ID="AddressLabel1" runat="server"
                Address1="98 Andover Place" 
                Town="Reading" County="Berkshire" Postcode="RG3 5AP" 
                AddressDisplayFormat="InForm" CssClass="AddressLabel" AddressTypeStyle="Label" 
                Font-Bold="true" /&gt;</pre>
                        <ul>
                            <li><strong>Address1</strong> &ndash; gets or sets the first line of an address</li>
                            <li><strong>Address2</strong> &ndash; gets or sets the second line of an address</li>
                            <li><strong>Address3</strong> &ndash; gets or sets the third line of an address</li>
                            <li><strong>Town</strong> &ndash; gets or sets the town in an address</li>
                            <li><strong>County</strong> &ndash; gets or sets the county in an address</li>
                            <li><strong>Postcode</strong> &ndash; gets or sets the postcode</li>
                            <li><strong>Country</strong> &ndash; gets or sets the country in an address</li>
                            <li><strong>AddressDisplayFormat</strong> &ndash; gets or sets the address layout to
                                be either in-form (vertical) or in-line (horizontal)</li>
                            <li><strong>AddressType</strong> &ndash; gets or sets the address type; defaults to
                                &ldquo;Usual address&rdquo;</li>
                            <li><strong>AddressTypeStyle</strong> &ndash; gets the CSS that is to be applied to
                                the address label</li>
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
                            <li>All stored address data should display. However, if an address element is empty,
                                it will not display. For example, in the code sample above, the Country property
                                is not defined and so is not displayed in the AddressLabel</li>
                            <li>The Postcode property should always display in capitalized form and as the final
                                element before the Country property</li>
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='addressLabelWinFormsTab' href=javascript:TabClick('addressLabelWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <br />
                Example WinForms control (screenshot):
                <br />
                <br />
                <table>
                    <tr>
                        <td>
                            <img class="controls_border" alt="AddressLabel WinForms Control screenshot" title="AddressLabel WinForms control screenshot"
                                runat="server" src="~/Components/Images/addresslabelA.gif" />
                        </td>
                        <td>
                            The AddressLabel using the in-form (vertical) layout format.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img class="controls_border" alt="AddressLabel WinForms Control screenshot" title="AddressLabel WinForms control screenshot"
                                runat="server" src="~/Components/Images/addresslabelB.gif" />
                        </td>
                        <td>
                            The AddressLabel using the in-line (horizontal) layout format.
                        </td>
                    </tr>
                </table>
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
                        <div class="last section">
                        <ul>
                            <li><strong>Address1</strong> &ndash; gets or sets the first line of an address</li>
                            <li><strong>Address2</strong> &ndash; gets or sets the second line of an address</li>
                            <li><strong>Address3</strong> &ndash; gets or sets the third line of an address</li>
                            <li><strong>Town</strong> &ndash; gets or sets the town in an address</li>
                            <li><strong>County</strong> &ndash; gets or sets the county in an address</li>
                            <li><strong>Postcode</strong> &ndash; gets or sets the postcode</li>
                            <li><strong>Country</strong> &ndash; gets or sets the country in an address</li>
                            <li><strong>AddressDisplayFormat</strong> &ndash; gets or sets the address layout to
                                be either in-form (vertical) or in-line (horizontal)</li>
                            <li><strong>AddressType</strong> &ndash; gets or sets the address type; defaults to
                                &ldquo;Usual address&rdquo;</li>
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
                                All stored address data should display. However, if an address element is empty,
                                it will not display. For example, in the code sample above, the Country property
                                is not defined and so is not displayed in the AddressLabel.
                            </p>
                            <p>
                                The Postcode should always display in capitalized form and as the final element
                                before the Country.
                            </p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WinformsAddInfo_Extender" runat="Server" TargetControlID="WinformsAddInfo_ContentPanel"
                        ExpandControlID="WinformsAddInfo_HeaderPanel" CollapseControlID="WinformsAddInfo_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="WinformsAddInfo_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>  
</asp:Content>
