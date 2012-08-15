<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="sorting.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.SortingPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">
        <p>
        Column headings are used to indicate which column the list is sorted by and can be 
        clicked to reverse the sort order.
        </p>
        <img src="images/sorting.png" alt="Diagram of a list of current medications with callouts for key elements" />
        <p>
        When a user clicks a column to change the sort order, retain the default sort 
        order as a secondary sort. That will define the display order for medications 
        that have the same attribute value as the new sort. For example, suppose the 
        default display for a list of current medications is with the most recent 
        start date at the top. If the user then sorts that list by &#39;started&#39;, 
        the &#39;started&#39; medications will be listed with the most recent start 
        date at the top.
        </p>
    </div>    
</asp:Content>