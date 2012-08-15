<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="genericequiv.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.CascadingLists.GenericEquiv" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp1010.png" alt="Diagram of a cascading list containing three parts. The first two parts are labelled &#39;two parts&#39; and contain: &#39;atenolol &#45; oral&#39; and &#39;TENORMIN &#45; oral&#39;. The final part contains &#39;other...&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1010</p>
                <p>
                When a cascading list includes options for the selected brand and for 
                generic equivalents, divide the list into two parts
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp1020.png" alt="Diagram of a cascading list containing three parts. The first part (atenolol &#45; oral) has a callout labelled &#39;generic equivalent&#39; and the second part (TENORMIN &#45; oral) has a callout labelled &#39;selected brand&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1020</p>
                <p>
                Display generic equivalent options above specific brand options in cascading lists
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
