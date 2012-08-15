<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="TimelineView.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.TimelineView"
    Title="Guidance - Timeline View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Guidance -- Timeline View.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Timeline View documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Timeline View (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <h2>
            Introduction
        </h2>
        <p>
            The <i>Design Guidance &ndash; Timeline View</i> document provides you with guidance
            and recommendations for the design and functionality required to graphically portray
            patient clinical data over (potentially long) time periods.
        </p>
        <p>
            This guidance shows how best to visualize clinical events and durations to provide
            clinicians with a richer context for understanding a patient&rsquo;s condition. Its purpose
            is to define the best approaches to take when presenting information on a timescale
            &ndash; in isolation or in combination with other, quantitative data &ndash; to assist clinicians
            in identifying meaningful and diagnostic patterns.</p>
    </div>
    <div class="last section">
        <h2>
            Summary</h2>
        <p>
            The guidance describes how to layout and display a Timeline View and how to provide clinicians with the ability to adjust and navigate through this view. It includes recommendations for:</p>
        <ul>
            <li>Laying out the Timeline View</li>
            <li>Displaying the Timeline View area</li>
            <li>Displaying individual timeline entries</li>
            <li>Navigating through entries in a specific time period</li>
            <li>Representing sets of related events</li>
            <li>Navigating between time periods</li>
        </ul>
    </div>
</asp:Content>
