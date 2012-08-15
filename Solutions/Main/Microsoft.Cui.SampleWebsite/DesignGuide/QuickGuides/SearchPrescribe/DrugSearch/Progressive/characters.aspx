<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="characters.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.Progressive.Characters" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../../images/msp0390.png" alt="A search text input box containing the text &#39;p&#39; with a search results list displaying the message &#39;Please type in at least 2 characters&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0390</p>
                <p>
                Require a minimum of two characters before displaying search results
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0400</p>
                <p>
                When only one character has been entered, display a message that 
                explains why there are no results and reports the two-character 
                minimum
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                	<img src="../../images/msp0410.png" alt="A search text input box containing the text &#39;pax&#39; with a search results list displaying the message &#39;No matches were found&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0410</p>
                <p>
                When two or more characters have been entered and no matches were 
                found, display a message that clearly indicates a search has been 
                performed and no matches were found
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            

        </div>
    </div>
</asp:Content>
