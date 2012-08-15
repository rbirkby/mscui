<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="finder.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.FinderPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
   <div id="page">
        <div id="overview">            
            <p>
            A UK address finder should only be provided if the clinical application 
            supports a postcode-based address lookup service. Typically, all the 
            matching addresses found in the database are displayed in an address 
            picker. The user then selects one of these addresses and it is stored 
            as if the user had input the whole address.
            </p>
            <img src="../images/ukfinder.png" alt="A form with labels and input controls for House &#47; Building Number, House &#47; Building Name and Postcode. Postcode is followed by a &#39;Find Address&#39; button." />
            <p>
            Though the guidance does not define it, the finder control may provide 
            additional text by using hints, prompts or tooltips.
            </p>
        </div>
    </div>
</asp:Content>