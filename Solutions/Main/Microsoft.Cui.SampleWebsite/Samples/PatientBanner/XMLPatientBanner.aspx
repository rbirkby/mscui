<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="SamplesPatientBannerXmlPatientBanner" Title="Untitled Page" Codebehind="XMLPatientBanner.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <asp:UpdatePanel ID="CCUpdatePanel" runat="server">
            <ContentTemplate>    
        <asp:Panel ID="DemoPanel1" runat="server" CssClass="demoCCareaPB">
            <div style="margin-bottom:5px">
                <asp:Label ID="patientSelectText" runat="server" Text="Please select a patient:">
                </asp:Label>
            </div>
            <asp:Panel ID="matchOptions" runat="server">
                <asp:ListBox ID="patientListBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PatientListBox_SelectedIndexChanged">
                </asp:ListBox>
            </asp:Panel>
            <table>
                <tr>
                    <td>
                        <NhsCui:PatientBanner ID="patientBanner" runat="server" Width="750px" 
                                OnViewAllAddressesClick="PatientBanner_ViewAllAddressesClick" 
                                OnViewAllergyRecordClick="PatientBanner_ViewAllergyRecordClick"                                 
                                OnViewAllContactDetailsClick="PatientBanner_ViewAllContactDetailsClick" 
                                OnGenderValueClick="PatientBanner_GenderValueClick" OnIdentifierClick="PatientBanner_IdentifierClick"
                            >
                        </NhsCui:PatientBanner>
                    </td>
                </tr>
            </table>
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
                Select a name from the list of fictitious patients and the corresponding data record
                will be displayed in the PatientBanner control.
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
                This sample takes the PatientBanner control and offers a choice of fictitious data
                records from an XML document. This is an example of how the PatientBanner control
                can be easily bound to a data source. The PatientBanner control could just as easily
                be bound to a more practical data source such as a database.
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
