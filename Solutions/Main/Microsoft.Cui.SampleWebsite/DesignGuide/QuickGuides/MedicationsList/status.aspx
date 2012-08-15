<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="status.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.StatusPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        <p>
        The display of a medication&#39;s status should include indication of whether 
        it is &#39;current&#39; or &#39;past&#39;. This guidance does not mandate specific 
        status names, but the recommended minimum set of values is:
        </p>
        <ul>
            <li><strong>Current</strong>: &#39;Started&#39;, &#39;Not Started&#39; and &#39;Suspended&#39;</li>
            <li><strong>Past</strong>: &#39;Completed&#39;, &#39;Discontinued&#39;</li>
        </ul>
        <img src="images/status.png" alt="Two diagrams, showing: 1. Example status values for current medications (Not Started, Started, Suspended) 2. Past medications (Discontinued, Completed) respectively" />
        </div>
    </div>
</asp:Content>