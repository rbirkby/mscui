<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="listheight.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.ListHeightPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
            
           <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0430.png" alt="Diagram of a search text input box with a search results list that is only two rows high and contains two matches" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0430</p>
                <p>
                Display search results in a list that is only as high as needed to show the successful 
                matches or up to a defined maximum height
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0460</p>
                <p>
                Allow the height of the search results list to grow to an upper limit to 
                accommodate the number of results matched
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0440.png" alt="Diagram of a search results list with a section at the bottom containing the text &#39;Show 10 of 40 matches&#39; and a button labelled &#39;Show All&#39;. One callout indicates the two counts (10 and 40) and the other indicates that the button is the &#39;control for displaying the full list&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0440</p>
                <p>
                When the number of matches is too large to be displayed in the maximum list height, 
                display a message at the end of the search results list that contains counts of the 
                displayed results and total matches
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0450</p>
                <p>
                When the number of matches is too large to be displayed in the maximum list height, 
                place a control for displaying the full list at the end of the search results list
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
                        
        </div>
    </div>
</asp:Content>
