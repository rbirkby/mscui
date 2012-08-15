<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="format.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.PreviewAuthorise.Preview.Format" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="midline">
            	<div class="landscape">
                	<img src="../../images/msp2190.png" alt="Illustration of a preview in which the medication line wraps onto a new line twice" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-2230</p>
                <p>
                Do not display the medication line within a preview as 
                a long line of text extending for longer than 120 
                characters without wrapping onto a new line
                </p>
                <p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-2200</p>
                <p>
                Adhere to guidance in Design Guidance &#8211; 
                <a href="../../../../MedicationLine.aspx" title="Links to Guidance - Medication Line page">
                Medication Line</a> for the display of drug details in a preview
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                	<img src="../../images/msp2240.png" alt="Illustation in which the preview area has a column on the right containing &#39;08:00&#39; aligned to the top and &#39;20:00&#39; aligned to the bottom" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-2240</p>
                <p>
                Where relevant, display some prescription attributes 
                in a preview using a format similar to that used in 
                other medications views (though different to the format 
                used for the input control)
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>       
</asp:Content>
