<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="columns.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.ColumnsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">
        <p>
        A Medications List View can display various columns containing a variety of 
        medications information. Guidance is provided for key aspects of the way in 
        which the view is structured, including:
        </p>
        <ul>
            <li>Which columns must be displayed</li>
            <li>The order in which some columns are displayed</li>
            <li>Column widths</li>
            <li>Column headings</li>
            <li>Combining more than one attribute in a single column</li>
        </ul>
        <p>
        This diagram illustrates key guidance points:
        </p>
            <img src="images/columns.png" alt="Diagram of the structure of a medications list area with callouts indicating key guidance points" />
        <p>
        The specific information to display in the date columns is not part of 
        guidance because different tasks and contexts have different requirements. 
        Therefore, this guidance only provides for the relative placement of the 
        date columns.
        </p>
        <p>Illustrations on these pages use column headings with the following labels:</p>
        <ul>
            <li><strong>Start Date</strong> for the start or initiation of a medication</li>
            <li><strong>End Date</strong> for the end or completion of a medication</li>
        </ul>
        </div>
    </div>
</asp:Content>