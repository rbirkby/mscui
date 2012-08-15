<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="order.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.Order" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1830.png" alt="Illustration of the right hand side of a prescription form in which a callout indicates the frequency field and another callout, labelled &#39;fields whose values depend on the frequency&#39; indicates the two fields below" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1830</p>
                <p>
                When determining the order in which to display input 
                controls, prioritise the placement of fields whose 
                values determine which other fields may be displayed 
                in the form
                </p>
                <p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1840</p>
                <p>
                When determining the order in which to display input 
                controls, prioritise the grouping together of controls 
                whose values affect the options available in other controls
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>      
</asp:Content>
