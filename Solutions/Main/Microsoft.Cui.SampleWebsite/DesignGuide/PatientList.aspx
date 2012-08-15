<%@ Page Title="" Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="PatientList.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.PatientList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Guidance -- Patient List View.pdf" Target="_blank" ToolTip="Links to Design Guidance - Patient List View documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Patient List View (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <h2>
            Introduction
        </h2>
        <p>
            The <i>Design Guidance &ndash; Patient List View</i> document provides you with guidance
            and recommendations for the safe display of information for multiple patients in
            Patient List format. The guidance relates primarily to hospital and acute care scenarios
            and considers consistent access, display and management of the patient information.
            The guidance is focused on three key considerations:
            <ul>
                <li>Users must be able to easily access the Patient List information they need</li>
                <li>Users must be able to quickly and easily familiarize themselves with the Patient
                    List layout on first use</li>
                <li>Users must be able to quickly understand how to manage the display of Patient List
                    information</li>
            </ul>
        </p>
    </div>    
    <div class="last section">
        <h2>
            Summary</h2>
        <p>
            The document provides guidance on:
            <ul>
                <li>Patient List header </li>
                <li>Column headers</li>
                <li>Columns (containing sets of information within the Patient List)</li>
                <li>Row and cell content </li>
                <li>Sublists (located within cells) </li>
                <li>The display of patient identification information</li>
                <li>Row key identifiers</li>
                <li>Managing columns of displayed data</li>
                <li>Notification of updates to data</li>
                <li>Display of historical patient information as a 'snapshot'</li>
                <li>Display of further information opened by the user</li>
            </ul>
        </p>
    </div>
</asp:Content>
