<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="SamplesPatientBannerRestyledPatientBanner" Title="Untitled Page"
    Codebehind="RestyledPatientBanner.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="headTagContent" ContentPlaceHolderID="leafPageSpecificHeadTags" runat="server">
    <link runat="server" rel="stylesheet" href="../../CSS/RestyledPatientBanner.css"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <asp:UpdatePanel ID="CCUpdatePanel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="DemoPanel1" runat="server" CssClass="demoCCareaPB">
                    <fieldset style="display: inline; border: solid 1px black;">
                        <legend>Please select a style:</legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="monoChrome" GroupName="toggleCSSRadio" Text="MonoChrome" AutoPostBack="True"
                                        runat="server" Style="margin-left: 4px" Checked="true"></asp:RadioButton>
                                </td>
                                <td>
                                    <asp:RadioButton ID="pureAndSimple" GroupName="toggleCSSRadio" Text="Pure &amp; Simple"
                                        AutoPostBack="True" runat="server" Style="margin-left: 4px"></asp:RadioButton>
                                </td>
                                <td>
                                    <asp:RadioButton ID="blueBar" GroupName="toggleCSSRadio" Text="Blue Bar" AutoPostBack="True"
                                        runat="server" Style="margin-left: 4px"></asp:RadioButton>
                                </td>
                                <td>
                                    <asp:RadioButton ID="steel" GroupName="toggleCSSRadio" Text="Steel" AutoPostBack="True"
                                        runat="server" Style="margin-left: 4px"></asp:RadioButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="blueReverse" GroupName="toggleCSSRadio" Text="Blue Reverse"
                                        AutoPostBack="True" runat="server" Style="margin-left: 4px"></asp:RadioButton>
                                </td>
                                <td>
                                    <asp:RadioButton ID="blueDoubleBar" GroupName="toggleCSSRadio" Text="Blue Double Bar"
                                        AutoPostBack="True" runat="server" Style="margin-left: 4px"></asp:RadioButton>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                    <%-- Tel Nos from: http://www.ofcom.org.uk/telecoms/ioi/numbers/num_drama --%>
                    <NhsCui:PatientBanner ID="patientBanner" PatientImage="~/images/patientsnaps/JohnEvans.gif"
                        FamilyName="Evans" GivenName="Jonathan" Title="Mr" DateOfBirth="12-Feb-2006"
                        Identifier="606 172 4098" IdentifierType="NhsNumber" Gender="Male" HomePhoneNumber="(0118) 496 0337"
                        WorkPhoneNumber="(0118) 496 0823" MobilePhoneNumber="(07971) 118470" EmailLabelText="Email" EmailAddress="jane.evans@abc.xyz" Address1="98 Andover Place"
                        Town="Reading" County="Berkshire" PostCode="RG3 5AP"
                        AccessKey="P" runat="server" Width="750px"
                        OnViewAllAddressesClick="PatientBanner_ViewAllAddressesClick" 
                        OnViewAllContactDetailsClick="PatientBanner_ViewAllContactDetailsClick"  
                        OnGenderValueClick="PatientBanner_GenderValueClick" OnIdentifierClick="PatientBanner_IdentifierClick">
                    </NhsCui:PatientBanner>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>         
    <!-- Area for Description -->
    <asp:Panel ID="description_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <input type="image" ID="description_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
            Sample Description
        </div>
    </asp:Panel>
    <asp:Panel ID="description_ContentPanel" runat="server" Style="overflow: hidden;">
        <div class="section">
            <p>
               The sample allows you to select from a variety of CSS files, using a radio button, to change the appearance of the 
        PatientBanner. 
  
            </p>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeDescription" runat="Server" TargetControlID="description_ContentPanel"
        ExpandControlID="description_HeaderPanel" CollapseControlID="description_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="description_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Sample Description" CollapsedImage="~/images/SFTheme/acc_h.png"
        CollapsedText="Click to expand the Sample Description" SuppressPostBack="true" />
    <!-- Area for Properties -->
    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <input type="image" ID="properties_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
            Sample Details
        </div>
    </asp:Panel>
    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;"
        Height="0px">
        <div class="last section">
            <p>
                This sample illustrates how the PatientBanner exposes the CSS properties that allow
                the PatientBanner to be styled as desired by a developer. These properties were
                purposely exposed to ensure the control could blend seamlessly with the style of
                the parent application.
            </p>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="properties_ContentPanel"
        ExpandControlID="properties_HeaderPanel" CollapseControlID="properties_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="properties_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Sample Details" CollapsedImage="~/images/SFTheme/acc_h.png"
        CollapsedText="Click to expand the Sample Details" SuppressPostBack="true" />

<script type="text/javascript">
    function pageLoad()
    {        
        if(this.msg != undefined && this.msg != "")
        {
            alert(this.msg);
            this.msg="";
        }
    }    
</script>        
</asp:Content>
