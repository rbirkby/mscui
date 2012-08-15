<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="interactions.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.SelectionLists.Interactions" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="landscape">
                	<img src="../../images/msp1890.png" alt="Two step diagram: 1. A prescription form with sentence layout of input controls 2. A drop-down list below the field containing administration times" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1890</p>
                <p>
                Do not empty other fields when 
                a selection list is reopened
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1870</p>
                <p>
                Allow the ESC key to be used 
                to close a selection list
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
        </div>
    </div>      
</asp:Content>
