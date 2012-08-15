<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="AlertsReminders.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.AlertsReminders"
    Title="Microsoft Health CUI – Guidance – Alerts and Reminders Framework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Guidance Exploration -- Decision Support.pdf"
                Target="_blank" ToolTip="Links to Design Guidance Exploration - Decision Support documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance Exploration &ndash; Decision Support (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <h1>
            Groundwork &ndash; Alerts and Reminders Framework</h1>
        <h2>
            Introduction</h2>
        <p>
            The design groundwork exploration in the <i>Design Guidance Exploration &ndash; Decision
                Support</i> document provides you with design exploration and recommendations
            for the notification of alerts and reminders within a patient record that have either
            occurred or should occur. It enhances patient safety and clinical application usability
            by providing guidance for:
        </p>
        <ul>
            <li>Displaying alerts and reminders in a clinical application</li>
            <li>Editing and managing alerts</li>
            <li>Recommending standard components of an alert</li>
        </ul>
        <p>
            The first part of this design groundwork exploration to be made publically available
            is the early design exploration for decision support alerts. This focuses on areas
            of decision support design, including the structure of the decision support information
            window and interacting with system generated alerts.</p>
    </div>
    <div class="update section">
        <label>
            Note:</label>
        <p>
            The ideas presented in this document are for community preview and consultation
            only. Further design and patient safety assessments are required to finalize the
            content as CUI Design Guidance.
        </p>
    </div>

    
    <div class="last section">
        <h2>
            Summary
        </h2>
        <p>
            The Alerts and Reminders groundwork focusses on decision support within the context
            of an individual patient record. The areas of focus for this work are:
        </p>
        <ul>
            <li>Communication of decision support capability</li>
            <li>Display of choice lists with preferences and display</li>
            <li>Interaction with unprompted notifications</li>
        </ul>
    </div>
</asp:Content>
