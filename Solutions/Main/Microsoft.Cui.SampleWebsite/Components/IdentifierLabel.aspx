<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsIdentifierLabel" Title="Untitled Page" CodeBehind="IdentifierLabel.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The IdentifierLabel control displays a unique patient identification number.
        </p>
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlightControl" HeaderText="<a id='identifierLabelSilverlightTab' href=javascript:TabClick('identifierLabelSilverlightTab'); title='Silverlight Tab'>Silverlight</a>">
            <ContentTemplate>
                <br />
                Example Silverlight control (embedded):
                <br />
                <br />
                <object data="data:application/x-silverlight," type="application/x-silverlight-2"
                    width="100%" height="50px">
                    <param name="source" value="../ClientBin/Microsoft.Cui.SamplePages.xap" />
                    <param name="initParams" value="StartPage=IdentifierLabel,AllowResize=False" />
                    <param name="minRuntimeVersion" value="3.0.40818.0" />
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none;">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                            style="border-style: none" />
                    </a>
                </object>
                <iframe style='visibility: hidden; height: 0; width: 0; border: 0px'></iframe>
                <p class="resetFloatAfterdemoCCArea">
                    The IdentifierLabel displaying a patient identifier.
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
                        <div class="section">
                            The IdentifierLabel control is initialized with the following code:
                            <pre>&lt;sl:IdentifierLabel x:Name="IdentifierLabelControl" IdentifierType="NhsNumber" Text="4372623623" 
                            FontFamily="Verdana" FontSize="12" FontWeight="Bold" 
                            VerticalAlignment="Center" HorizontalAlignment="Center" /&gt;
        </pre>
                            <ul>
                                <li><strong>IdentifierType</strong> &ndash; gets or sets whether to process the identifier
                                    with the &ldquo;NhsNumber&rdquo; validation checksum </li>
                                <li><strong>Text</strong> &ndash; gets or sets a unique patient identifier; this is
                                    mandatory if the IdentifierType is set to &ldquo;NhsNumber&rdquo;</li>
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
                                <li><strong>IdentifierType</strong> has a default value of &ldquo;Other&rdquo;</li>
                                <li><strong>Text</strong> has a default value of &ldquo;XXX XXX XXXX&rdquo;</li>
                            </ul>
                            <p>
                                If IdentifierType is set to &ldquo;Other&rdquo;, no validation is performed. If
                                IdentiferType is set to &ldquo;NhsNumber&rdquo;, an algorithm checks whether the
                                number conforms to the NHS Number format. This involves ensuring:
                            </p>
                            <ul>
                                <li>No alphabetic characters are included</li>
                                <li>The string length is equal to 10 characters</li>
                                <li>All characters are not the same</li>
                                <li>The NhsNumber string is not empty or a null value</li>
                            </ul>
                            <p>
                                If the algorithm finds that the number is not correctly formatted as an NHS Number,
                                no data is displayed in the IdentifierLabel.</p>
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWPF" HeaderText="<a id='identifierLabelWPFTab' href=javascript:TabClick('identifierLabelWPFTab'); title='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WPF control (screenshot):
                    <br />
                    <br />
                    <img id="Img1" class="controls_border" alt="IdentifierLabel WPF control screenshot"
                        title="IdentifierLabel WPF control screenshot" runat="server" src="~/Components/Images/identifierlabel.GIF" />
                    <p class="resetFloatAfterdemoCCArea">
                        The IdentifierLabel displaying a patient identifier.
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
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='identifierLabelASPNETTab' href=javascript:TabClick('identifierLabelASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Panel CssClass="demoControlarea" ID="demoPanel1" runat="server">
                                <NhsCui:IdentifierLabel ID="IdentifierLabel1" runat="server" IdentifierType="NhsNumber"
                                    Text="4372623623"></NhsCui:IdentifierLabel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <p class="resetFloatAfterdemoCCArea">
                    The IdentifierLabel displaying a patient identifier.
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
                        The IdentifierLabel control is initialized with the following code:
                        <pre>&lt;NhsCui:IdentifierLabel ID="IdentifierLabel1" runat="server" IdentifierType="NhsNumber"
            Text="4372623623" /&gt;
        </pre>
                        <ul>
                            <li><strong>IdentifierType</strong> &ndash; gets or sets whether to process the identifier
                                with the &ldquo;NhsNumber&rdquo; validation checksum </li>
                            <li><strong>Text</strong> &ndash; gets or sets a unique patient identifier; this is
                                mandatory if the IdentifierType is set to &ldquo;NhsNumber&rdquo;</li>
                            <li><strong>Value</strong> &ndash; gets or sets the value of the unique identifier,
                                for example, an NHS Number</li>
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
                            <li><strong>IdentifierType</strong> has a default value of &ldquo;Other&rdquo;</li>
                            <li><strong>Text</strong> has a default value of &ldquo;XXX XXX XXXX&rdquo;</li>
                        </ul>
                        <p>
                            If IdentifierType is set to &ldquo;Other&rdquo;, no validation is performed. If
                            IdentiferType is set to &ldquo;NhsNumber&rdquo;, an algorithm checks whether the
                            number conforms to the NHS Number format. This involves ensuring:
                        </p>
                        <ul>
                            <li>No alphabetic characters are included</li>
                            <li>The string length is equal to 10 characters</li>
                            <li>All characters are not the same</li>
                            <li>The NhsNumber string is not empty or a null value</li>
                        </ul>
                        <p>
                            If the algorithm finds that the number is not correctly formatted as an NHS Number,
                            no data is displayed in the IdentifierLabel.</p>
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='identifierLabelWinFormsTab' href=javascript:TabClick('identifierLabelWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />
                    <img class="controls_border" alt="IdentifierLabel WinForms control screenshot" title="IdentifierLabel WinForms control screenshot"
                        runat="server" src="~/Components/Images/identifierlabel.GIF" />
                    <p class="resetFloatAfterdemoCCArea">
                        The IdentifierLabel displaying a patient identifier.
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
                            <li><strong>IdentifierType</strong> &ndash; gets or sets whether to process the identifier
                                with the &ldquo;NhsNumber&rdquo; validation checksum </li>
                            <li><strong>Text</strong> &ndash; gets or sets a unique patient identifier; this is
                                mandatory if the IdentifierType is set to &ldquo;NhsNumber&rdquo;</li>
                            <li><strong>Value</strong> &ndash; gets or sets the value of the unique identifier,
                                for example, an NHS Number</li>
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
                                IdentifierType defaults to "Other" and no validation is performed. If IdentiferType
                                is set to "NhsNumber", an algorithm checks whether the NhsNumber format is correct.
                                This involves ensuring:
                                <ul>
                                    <li>no alphabetic characters are included</li>
                                    <li>the string length is equal to 10 characters</li>
                                    <li>all characters are not the same</li>
                                    <li>the NhsNumber string is not empty or a null value</li>
                                </ul>
                            </p>
                            <p>
                                If the algorithm finds the NhsNumber format to be incorrect, no data is displayed
                                in the IdentifierLabel.
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
