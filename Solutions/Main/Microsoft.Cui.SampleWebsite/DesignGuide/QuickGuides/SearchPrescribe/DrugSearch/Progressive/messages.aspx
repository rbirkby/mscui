<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="messages.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.Progressive.Messages" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                               
            <div class="line">
            	<div class="illustration">
                	<img src="../../images/msp0420a.png" alt="Diagram of a search text input box and search results list with the message &#39;Please type in at least 2 characters&#39;. Arrows indicate the height of the search results list" />
                	<img src="../../images/msp0420b.png" alt="Diagram of a search text input box and search results list with an arrow indicating the width of the search text input box. The arrow is labelled &#39;minimum message width&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0420</p>
                <p>
                When a message is displayed, maintain a minimum height equivalent to at least three 
                rows of results and a width that is at least as wide as the search text input box
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
