<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="spellsynonyms.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.Matching.SpellSynonyms" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../../images/msp0570.png" alt="Illustration of the top part of a search results list for the text &#39;asprin&#39; with the letter &#39;i&#39; highlighted in each instance of the word &#39;aspirin&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0570</p>
                <p>
                Support spelling matches
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-0590</p>
                <p>
                When there are spelling matches or synonyms to display, list them 
                along with other results in the search results list
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        <div class="midline">
            <div class="illustration">
                <img src="../../images/msp0580.png" alt="Illustration of the top part of a search results list for the text &#39;fru&#39;. The second entry is &#39;furosemide&#39; and is followed by text in grey italics: previously named frusemide" />
             </div>
             <div class="guidetext">
                <p class="number">MSP-0580</p>
                <p>
                Support synonyms such that a drug name for which a synonym has been 
                defined is displayed when the synonym is matched
                </p>
                <p class="recommended">Recommended</p>
             </div>
        </div>
            
        <div class="line">
            <div class="illustration">
                <img src="../../images/msp0770.png" alt="Diagram including a search results list with a single entry: &#39;furosemide&#39; with text in grey italics on the same line &#39;previously named frusemide&#39;. A callout points to the word &#39;frusemide&#39;" />
             </div>
             <div class="guidetext">
                <p class="number">MSP-0770</p>
                <p>
                For drug names that are displayed when matched on a synonym, supplement 
                the drug name with a message that includes the synonym
                </p>
                <p class="mandatory">Mandatory</p>
             </div>
        </div>
                    
        </div>
    </div>
</asp:Content>
