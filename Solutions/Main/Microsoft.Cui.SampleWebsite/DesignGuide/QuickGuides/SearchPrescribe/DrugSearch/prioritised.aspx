<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="prioritised.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.PrioritisedPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0620.png" alt="Diagram of a search results list with two sections labelled &#39;Commonly prescribed matches&#39; and &#39;Standard matches&#39; respectively. The two sections are separated with a horizontal line and the first section has a callout labelled &#39;prioritised results&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0620</p>
                <p>
                Display prioritised results in a separate section that appears 
                above other results in the list
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-0630</p>
                <p>
                Separate the prioritised results from standard matches with a horizontal line
                </p>
                <p class="recommended">Recommended</p>
            </div>
            
        </div>
    </div>
</asp:Content>
