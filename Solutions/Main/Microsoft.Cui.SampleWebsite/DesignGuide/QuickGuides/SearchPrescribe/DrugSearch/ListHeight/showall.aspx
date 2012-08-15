<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="showall.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.ListHeight.ShowAll" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="line">
            	<div class="illustration">
                	<img src="../../images/msp0470.png" alt="A search results list with a scroll bar" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0470</p>
                <p>
                When a limited list has been extended by selecting the control to display a full list, 
                extend the list by providing a scroll bar
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0480</p>
                <p>
                Keep search results &#39;flat&#39;. Do not provide expand or collapse mechanisms or tree-style 
                browsing within the search results
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0490</p>
                <p>
                When a selection has been made in a search results list that has a scroll bar, allow the 
                scroll bar to be used such that the selection can be scrolled out of view
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
