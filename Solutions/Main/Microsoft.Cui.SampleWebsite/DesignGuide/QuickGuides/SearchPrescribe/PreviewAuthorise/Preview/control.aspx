<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="control.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.PreviewAuthorise.Preview.Control" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
         
            <div class="line">
            	<div class="landscape">
                	<img src="../../images/msp2250.png" alt="Diagram of a preview in which two buttons are displayed below the medication line. The second button has focus and is labelled &#39;Close Preview&#39;. A callout labelled &#39;control for closing the preview&#39; points to the second button" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-2250</p>
                <p>
                Provide a control for closing the preview and 
                returning to the prescription form (such that 
                the prescription can be amended)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-2260</p>
                <p>
                Set default focus to the control that closes the preview
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>  
                     
        </div>
    </div>      
</asp:Content>
