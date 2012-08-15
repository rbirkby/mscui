<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="SamplesPatientBannerExtendedPatientBanner" Title="Untitled Page"
    Codebehind="ExtendedPatientBanner.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <asp:Panel ID="DemoPanel1" runat="server" CssClass="demoCCareaPB">
            <%-- Tel Nos from: http://www.ofcom.org.uk/telecoms/ioi/numbers/num_drama --%>
            <NhsCui:PatientBanner ID="patientBanner1" PatientImage="~/Images/PatientSnaps/SChandraseknar.gif"
                FamilyName="Chandrasekhar" GivenName="Subramanyan" Title="Mr" DateOfBirth="14-Jul-1945"
                Identifier="434 382 8352" Gender="Male" HomePhoneNumber="(0163) 296 0564" WorkPhoneNumber="(0163) 296 0555"
                MobilePhoneNumber="(07971) 184701" EmailLabelText="Email" EmailAddress="ChSubram@abc.xyz" Address1="56 Mississippi Close"
                Town="Eastleigh" County="Hampshire" PostCode="SO50 7TH"
                AccessKey="E" ZoneTwoTooltip="Click to expand" SubsectionThreeTitle="Other Information"
                SubsectionFourTitle="Considerations"
                SubsectionOneWidth="161px"
                SubsectionTwoWidth="160px"
                SubsectionThreeWidth="150px"
                SubsectionFourWidth="100px"
                SubsectionFiveWidth="140px"
                 runat="server" 
                OnViewAllAddressesClick="PatientBanner_ViewAllAddressesClick" 
                OnViewAllContactDetailsClick="PatientBanner_ViewAllContactDetailsClick"
                OnGenderValueClick="PatientBanner_GenderValueClick" OnIdentifierClick="PatientBanner_IdentifierClick">
                        <SubsectionThreeTemplate>
                            <table class="otherInfo">
                                <tr><th class="nhscui_pb_zone2_label">Ethnicity</th><td>Asian - British</td></tr>
                                <tr><th class="nhscui_pb_zone2_label">Dispensing Patient</th><td>Yes</td></tr>
                                <tr><th class="nhscui_pb_zone2_label">Patient Type</th><td>Full NHS</td></tr>
                                <tr><th class="nhscui_pb_zone2_label">Current Clinician</th><td>Dr Clift</td></tr>
                                <tr><th class="nhscui_pb_zone2_label">Registered GP</th><td>Dr Hunter</td></tr>
                            </table>
                        </SubsectionThreeTemplate>
                        <SubsectionFourTemplate>    
                            <div style="width:95px;">
                           - Requires Hindi Interpreter
                           </div>
                        </SubsectionFourTemplate>
            </NhsCui:PatientBanner>
        </asp:Panel>
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
                The sample shows how you can extend the PatientBanner control to include additional
                information in the expandable panel.
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
                The PatientBanner control exposes functionality that allows additional ASP.NET controls
                to be included on the control. This additional data can inherit the same CSS styling
                as the other areas of the expandable panel to maintain a consistent look and feel.
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
