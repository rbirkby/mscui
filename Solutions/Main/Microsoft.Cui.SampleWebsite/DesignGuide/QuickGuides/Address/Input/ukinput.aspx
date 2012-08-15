<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="ukinput.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.UKInputPage" %>
 
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
   <div id="page">
        <div id="overview">            
            <p>
            The minimum number of data elements required for input of a UK address 
            can vary. Many addresses will only require three elements:
            </p>
            <ul>
            	<li>House number and street</li>
                <li>Town and city</li>
                <li>Postcode</li>
            </ul>
            <img src="../images/ukinput.png" alt="A form with labels and input controls for Line 1, Line 2, Town &#047; City, County and Postcode. Postcode is followed by a &#39;Find Postcode&#39; button." />
            <p>
            However, addresses outside of London will require a &#39;County&#39; and all 
            addresses might need the name of a locality or suburb.
            </p>
        </div>
    </div>
</asp:Content>