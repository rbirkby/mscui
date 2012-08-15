<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="currentpast.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.CurrentPastPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">
        <p>
        Each medication is displayed in either the current medications list 
        or the past medications list. A control is provided that allows the user 
        to navigate between the two lists and filter the past medications list:
        </p>
        <img src="images/medv066.png" alt="Current and Past controls with a drop-down list associated with the Past button" />
        <p>
        When the status of a medication changes, it may move from the current 
        medications list to the past medications list. A notification is displayed 
        in the current medications list that includes a count of medications that 
        have recently moved into the past medications list.
        </p>
        <img src="images/recentpast.png" alt="A list of current medications with a &#39;Recent Past&#39; notification displayed at the bottom" />
        </div>
    </div>
</asp:Content>