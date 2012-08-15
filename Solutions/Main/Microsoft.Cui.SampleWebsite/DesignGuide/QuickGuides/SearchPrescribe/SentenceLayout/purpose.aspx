<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="purpose.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.SentenceLayout.Purpose" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
     <div id="page">
        <div id="guidance">
                        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1450.png" alt="Diagram showing a series of boxes (drug, route, dose, frequency, give when..., administration times, first dose, duration) displayed as a sentence and wrapping onto a new line at two points. Where the boxes wrap onto a new line, arrows join the end of one line to the beginning of the next" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1450</p>
                <p>
                Use sentence layout when fields are displayed 
                in an area with much greater width than height 
                (a thin horizontal strip)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1460</p>
                <p>
                Use sentence layout for cascading lists and 
                whenever selection lists are presented step by step
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>      
</asp:Content>
