<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="MedicationsAdmin.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.MedicationsAdmin"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Drug Administration.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Drug Administration documentation">
            
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Drug Administration (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
            Introduction</h2>
        <p>
            The <i>Design Guidance &ndash; Drug Administration</i> document provides you with guidance and
            recommendations for displaying a patient&rsquo;s medication administration information
            in clinical applications. It enables unambiguous display of administration information,
            while enhancing patient safety and clinical application usability by:             
        </p>
        <ul>
            <li>Displaying rich medication information effectively, such as administration history, administration plan and medication item types </li>
            <li>Allowing quick identification and location of items that require immediate attention or may be useful to monitor (reducing the risk of them going unnoticed)</li>
            <li>Supporting efficient and accurate recording of the administration results, such as timing, administrator details, deviations and exceptions </li>
            <li>Electronically mirroring the format of current drug charts </li>
            <li>Displaying complex schedules effectively</li>
        </ul>
    </div>
    <div class="last section">
        <h2>
            Summary
        </h2>
        <p>
            The guidance describes the interface for supporting the administration of medications for a single patient:
        </p>
        <ul>
            <li>The composition and ordering of items that appear within the administration interface </li>
            <li>The framework controls, such as the Status Bar, Grouping Control and Look-Ahead Scroll Bar</li>
            <li>Basic display properties, such as icons, symbols and event information </li>
            <li>Examples of detailed displays and complex administrations for Regular, Once Only and As Required items and administrations of Significant Duration</li>
            <li>The recording of administration events</li>
        </ul>
    </div>
</asp:Content>
