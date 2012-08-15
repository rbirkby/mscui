<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="format.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.SentenceLayout.Format" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
     <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1470.png" alt="A series of input boxes with two callouts both labelled &#39;label&#39;. The first points to &#39;DOSE&#39; in an input box containing &#39;DOSE 250 mg&#39; and the second points to &#39;first dose&#39; in an input box containing &#39;first dose Today 20:00&#39;" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1470</p>
                <p>
                When using sentence layout, for fields that 
                have labels, incorporate labels into the fields
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1520.png" alt="A series of input boxes with two callouts both labelled &#39;label&#39;. The first points to &#39;at these times&#39; in an input box containing &#39;at these times 08:00, 20:00&#39; and the second points to &#39;first dose&#39; in an input box containing &#39;first dose Today 20:00&#39;" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1520</p>
                <p>
                Provide labels for controls whose contents could 
                be interpreted as belonging to a different control
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1450.png" alt="Diagram showing a series of boxes (drug, route, dose, frequency, give when..., administration times, first dose, duration) displayed as a sentence and wrapping onto a new line at two points. Where the boxes wrap onto a new line, arrows join the end of one line to the beginning of the next" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1480</p>
                <p>
                When using sentence layout, wrap 
                fields onto a new line as necessary
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1490</p>
                <p>
                When grouping fields in sentence layout, 
                start a new line after each group
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>        
</asp:Content>
