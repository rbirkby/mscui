<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="layout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.Layout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0280.png" alt="Outline diagram of a search text input box, a small gap and then search results displayed below" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0280</p>
                <p>
                Do not allow the search results list to be positioned such that it is 
                separated from the search text input box by other controls or by a 
                significant space
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0290.png" alt="Diagram of a search text input box (with a magnifying glass icon in the right hand side) and a callout labelled &#39;In-field prompt&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0290</p>
                <p>
                Clearly describe the scope of the search
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0300</p>
                <p>
                Use an in-field prompt in the search text input box to describe 
                the scope of the search
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            

        </div>
    </div>
</asp:Content>
