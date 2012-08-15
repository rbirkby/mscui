<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="interaction.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DetailedForms.Interaction" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp1580.png" alt="Diagram of a prescription form in which only a drug name and cascading list of routes is displayed" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1580</p>
                <p>
                In a detailed prescription form, require the selection of drug 
                name and route (or drug name and attributes that allow the type 
                of medication to be determined) before fields are displayed in 
                the rest of the detailed prescription form
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1660.png" alt="Diagram of a prescription form in which two input boxes (drug and form) contain values and the remaining five input boxes contain prompts in grey italic text" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1660</p>
                <p>
                Do not rely on disabling fields (or controls for accessing 
                optional fields) to impose an order
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1670</p>
                <p>
                After selections from cascading lists have been completed, 
                do not automatically open a selection list for a control in 
                the detailed prescription form unless a change to a field 
                has triggered the need to confirm or 
                <span class="nowrap">re-enter</span> values in related 
                fields
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
        </div>
    </div>   
</asp:Content>
