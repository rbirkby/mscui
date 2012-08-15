<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="controls.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DetailedForms.Controls" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1600.png" alt="Detailed prescription form shown within a tabbed view" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1610</p>
                <p>
                Provide controls such as tabs or buttons for navigating 
                between areas of the detailed prescription form
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1620</p>
                <p>
                Provide controls for accessing all areas of the detailed 
                prescription such that there is no area that can only be 
                accessed by selecting an item (such as edit administration 
                times) from a selection list
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-1630</p>
                <p>
                When displaying the input controls in a detailed 
                prescription form, include an appropriate set of 
                controls for accessing optional fields
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>   
</asp:Content>
