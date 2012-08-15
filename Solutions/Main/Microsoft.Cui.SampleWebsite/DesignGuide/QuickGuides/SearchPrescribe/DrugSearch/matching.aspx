<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="matching.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.MatchingPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0500.png" alt="A search text input box in which the text &#39;can&#39; is displayed and part of a search results list in which the first two entries are &#39;candesartan&#39; and &#39;clotrimazole &#45; CANESTEN (CLOTRIMAZOLE)&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0500</p>
                <p>
                Match the text in the search text input box to generic drug 
                names and brand names respectively
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0510.png" alt="Diagram of a search text input box and part of a search results list. Circles indicate each instance of the letters &#39;can&#39; at the beginning of the matched generic drug names and brand names" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0510</p>
                <p>
                Match text entered into the search text input box to beginning of any 
                word (and not to substrings or endings of words)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0520.png" alt="A search text input box in which the text &#39;sal flu&#39; has been entered and a search results list with a single match(fluticasone &#43; salmeterol) is displayed" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0520</p>
                <p>
                Support multiple word searching by allowing the entry of letters separated 
                with a space and matching those against multiple words
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
