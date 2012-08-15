<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="selection.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.SelectionPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">
        <p>
        The user can select more than one medication and initiate the same action 
        on them all. After the user selects the medications, access to the available 
        actions is by a context menu (typically opened with a right-click).
        </p>
        <img src="images/medv128.png" alt="Diagram of a medications list with three medications selected and a context menu displayed" />
    </div>     
</asp:Content>