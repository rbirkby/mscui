<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsContactLabel" CodeBehind="ContactLabel.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="defaultContent" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The ContactLabel control displays the following details for a contact:
        </p>
        <ul>
            <li>Home telephone number</li>
            <li>Work telephone number</li>
            <li>Mobile number</li>
            <li>Email address</li>
        </ul>
        <p>
            If the telephone or email data is not entered, the label will be displayed with
            a blank space next to it.
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
                    width="100%" height="100px">
                    <param name="source" value="../ClientBin/Microsoft.Cui.SamplePages.xap" />
                    <param name="initParams" value="StartPage=ContactLabel,AllowResize=False" />
                    <param name="minRuntimeVersion" value="3.0.40818.0" />
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none;">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                            style="border-style: none" />
                    </a>
                </object>
                <iframe style='visibility: hidden; height: 0; width: 0; border: 0px'></iframe>
                <p class="resetFloatAfterdemoCCArea">
                    The ContactLabel displaying a contact's home, work and mobile phone numbers, and their email address.
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
                            The ContactLabel control is initialized with the following code:
                            <pre>&lt;sl:ContactLabel HomePhoneNumber="(0118) 496 0337" WorkPhoneNumber="(0118) 496 0338" 
                        MobilePhoneNumber="(07700) 900555" EmailAddress="jane.evans@abc.xyz" />
                        </pre>
                            <ul>
                                <li><strong>HomePhoneLabelText</strong> &ndash; gets or sets the caption associated
                                    with the contact's home phone number</li>
                                <li><strong>HomePhoneNumber</strong> &ndash; gets or sets the contact's home phone number</li>
                                <li><strong>WorkPhoneLabelText</strong> &ndash; gets or sets the caption associated
                                    with the contact's work phone number</li>
                                <li><strong>WorkPhoneNumber</strong> &ndash; gets or sets the contact's work phone number</li>
                                <li><strong>MobilePhoneLabelText</strong> &ndash; gets or sets the caption associated
                                    with the contact's mobile phone number</li>
                                <li><strong>MobilePhoneNumber</strong> &ndash; gets or sets the contact's mobile phone
                                    number</li>
                                <li><strong>EmailLabelText</strong> &ndash; gets or sets the caption associated with
                                    the contact's email address</li>
                                <li><strong>EmailAddress</strong> &ndash; gets or sets the contact's email address</li>
                                <li><strong>LabelStyle</strong> &ndash; gets or sets the style for the labels</li>
                                <li><strong>DataStyle</strong> &ndash; gets or sets the style for the data</li>
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
                                <li><strong>HomePhoneLabelText</strong> has a default value of &ldquo;Home&rdquo;</li>
                                <li><strong>HomePhoneNumber</strong> has a default value of &ldquo;&rdquo;</li>
                                <li><strong>WorkPhoneLabelText</strong> has a default value of &ldquo;Work&rdquo;</li>
                                <li><strong>WorkPhoneNumber</strong> has a default value of &ldquo;&rdquo;</li>
                                <li><strong>MobilePhoneLabelText</strong> has a default value of &ldquo;Mobile&rdquo;</li>
                                <li><strong>MobilePhoneNumber</strong> has a default value of &ldquo;&rdquo;</li>
                                <li><strong>EmailLabelText</strong> has a default value of &ldquo;Email&rdquo;</li>
                                <li><strong>EmailAddress</strong> has a default value of &ldquo;&rdquo;</li>
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWPF" HeaderText="<a id='contactLabelWPFTab' href=javascript:TabClick('contactLabelWPFTab'); title='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WPF control (screenshot):
                    <br />
                    <br />
                    <img id="Img1" class="controls_border" alt="ContactLabel WPF control screenshot"
                        title="ContactLabel WPF control screenshot" runat="server" src="~/Components/Images/contactlabel.GIF" />
                    <p class="resetFloatAfterdemoCCArea">
                        The ContactLabel displaying a contact's home, work and mobile phone numbers, and their email address.
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
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='contactLabelASPNETTab' href=javascript:TabClick('contactLabelASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Panel CssClass="demoControlarea" ID="demoPanel1" runat="server">
                                <NhsCui:ContactLabel ID="ContactLabel1" EmailLabelText="Email" EmailAddress="jane.evans@abc.xyz"
                                    HomePhoneNumber="(0118) 496 0337" WorkPhoneNumber="(0118) 496 0338" MobilePhoneNumber="(07700) 900555"
                                    CssClass="ContactList" runat="server" LabelStyle="ContactLabel" DataStyle="Data">
                                </NhsCui:ContactLabel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <p class="resetFloatAfterdemoCCArea">
                    The ContactLabel displaying a contact's home, work and mobile phone numbers, and their email address.
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
                        The ContactLabel control is initialized with the following code:
                        <pre>&lt;NhsCui:ContactLabel 
                ID="ContactLabel1" EmailLabelText="Email" EmailAddress="jane.evans@abc.xyz" 
                HomePhoneNumber="(0118) 496 0337" WorkPhoneNumber="(0118) 496 0338" 
                MobilePhoneNumber="(07700) 900555" CssClass="ContactList" 
                runat="server">
        </pre>
                        <ul>
                            <li><strong>HomePhoneLabelText</strong> &ndash; gets or sets the caption associated
                                with the contact's home phone number</li>
                            <li><strong>HomePhoneNumber</strong> &ndash; gets or sets the contact's home phone number</li>
                            <li><strong>WorkPhoneLabelText</strong> &ndash; gets or sets the caption associated
                                with the contact's work phone number</li>
                            <li><strong>WorkPhoneNumber</strong> &ndash; gets or sets the contact's work phone number</li>
                            <li><strong>MobilePhoneLabelText</strong> &ndash; gets or sets the caption associated
                                with the contact's mobile phone number</li>
                            <li><strong>MobilePhoneNumber</strong> &ndash; gets or sets the contact's mobile phone
                                number</li>
                            <li><strong>EmailLabelText</strong> &ndash; gets or sets the caption associated with
                                the contact's email address</li>
                            <li><strong>EmailAddress</strong> &ndash; gets or sets the contact's email address</li>
                            <li><strong>LabelStyle</strong> &ndash; gets or sets the style for the labels</li>
                            <li><strong>DataStyle</strong> &ndash; gets or sets the style for the data</li>
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
                            <li><strong>HomePhoneLabelText</strong> has a default value of &ldquo;Home&rdquo;</li>
                            <li><strong>HomePhoneNumber</strong> has a default value of &ldquo;&rdquo;</li>
                            <li><strong>WorkPhoneLabelText</strong> has a default value of &ldquo;Work&rdquo;</li>
                            <li><strong>WorkPhoneNumber</strong> has a default value of &ldquo;&rdquo;</li>
                            <li><strong>MobilePhoneLabelText</strong> has a default value of &ldquo;Mobile&rdquo;</li>
                            <li><strong>MobilePhoneNumber</strong> has a default value of &ldquo;&rdquo;</li>
                            <li><strong>EmailLabelText</strong> has a default value of &ldquo;Email&rdquo;</li>
                            <li><strong>EmailAddress</strong> has a default value of &ldquo;&rdquo;</li>
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='contactLabelWinFormsTab' href=javascript:TabClick('contactLabelWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />
                    <img class="controls_border" alt="ContactLabel WinForms control screenshot" title="ContactLabel WinForms control screenshot"
                        runat="server" src="~/Components/Images/contactlabel.GIF" />
                    <p class="resetFloatAfterdemoCCArea">
                        The ContactLabel displaying a contact's home, work and mobile phone numbers, and their email address.
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
                            <li><strong>HomePhoneLabelText</strong> &ndash; gets or sets the caption associated
                                with the contact's home phone number</li>
                            <li><strong>HomePhoneNumber</strong> &ndash; gets or sets the contact's home phone number</li>
                            <li><strong>WorkPhoneLabelText</strong> &ndash; gets or sets the caption associated
                                with the contact's work phone number</li>
                            <li><strong>WorkPhoneNumber</strong> &ndash; gets or sets the contact's work phone number</li>
                            <li><strong>MobilePhoneLabelText</strong> &ndash; gets or sets the caption associated
                                with the contact's mobile phone number</li>
                            <li><strong>MobilePhoneNumber</strong> &ndash; gets or sets the contact's mobile phone
                                number</li>
                            <li><strong>EmailLabelText</strong> &ndash; gets or sets the caption associated with
                                the contact's email address</li>
                            <li><strong>EmailAddress</strong> &ndash; gets or sets the contact's email address</li>
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
                                    <li>HomePhoneLabelText has a default value of "Home"</li>
                                    <li>WorkPhoneLabelText has a default value of "Work"</li>
                                    <li>MobilePhoneLabelText has a default value of "Mobile"</li>
                                    <li>EmailLabelText has a default value of "Email"</li>
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
