<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="compactdesign.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.CompactDesign" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0350.png" alt="Five step diagram illustrating the entry of text (dil), selection of a search result (diltiazem), presentation of a list of routes, re-selection of the drug name (diltiazem) and re-display of the search results list" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0350</p>
                <p>
                When space is limited (such that the search results lists may obscure 
                other information), support the replacement of the search text input box 
                with an input control in which the selected drug name is displayed
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-0360</p>
                <p>
                When a search results list has been replaced with a control in which the 
                selected drug name is displayed, <span class="nowrap">re-display</span> the search 
                text input box, the search criteria and the search results list when this control is selected
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
