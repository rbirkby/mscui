<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="drugsearch.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearchPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            A drug search tool supports text entry of search criteria and the display 
            of matches in a search results list. Guidance recommends the provision of a 
            Progressive matching control that updates the search results displayed as 
            characters are entered into the search input box. 
            </p>
            <img src="images/drugsearch.png" alt="Diagram illustrating input controls and search results lists for a Progressive search and a Static search" />
        </div>
    </div>      
</asp:Content>
