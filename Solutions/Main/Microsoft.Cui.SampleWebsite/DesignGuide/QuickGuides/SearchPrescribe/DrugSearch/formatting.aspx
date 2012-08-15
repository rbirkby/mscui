<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="formatting.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.FormattingPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0780.png" alt="Diagram of a search text input box and search results list with a callout labelled &#39;shaded row&#39; pointing to the background of one of the alternate rows that have a pale grey background" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0780</p>
                <p>
                Use subtle alternate shading of matches in the search results list
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0790</p>
                <p>
                Avoid the use of strong horizontal lines to separate individual list results
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0800.png" alt="Diagram of a search results list with one list item in the first section and four in the second section. Callouts labelled &#39;unshaded&#39; point to the first line of each section" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0800</p>
                <p>
                Re-start alternate shading at the beginning of a new 
                section in a search results list
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
