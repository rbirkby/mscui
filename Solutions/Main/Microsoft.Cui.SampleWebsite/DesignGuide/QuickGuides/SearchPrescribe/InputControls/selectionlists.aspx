<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="selectionlists.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.SelectionListsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp1900.png" alt="Two step diagram for the input box &#39;Give when...&#39;: 1. A list in which the last option is &#39;other...&#39; 2. The text &#39;Other...&#39; is replaced by a text cursor and prompt text: &#39;Enter a description of when this medication should be given&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1900</p>
                <p>
                Where possible, encourage the selection of an item 
                from a selection list before allowing free text to be entered
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp1930.png" alt="A list of administration times in which the second option contains &#39;06:00, 22:00&#39, followed by &#39;Non-standard times&#39; in grey italics. A callout labelled &#39;supplementary information&#39; points to the text in grey italics" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1910</p>
                <p>
                When there is supplementary information to display for 
                an entry in a selection list, display the information 
                in grey italics
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>      
</asp:Content>
