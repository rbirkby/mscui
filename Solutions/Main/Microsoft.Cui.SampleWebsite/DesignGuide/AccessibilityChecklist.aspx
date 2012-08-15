<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="AccessibilityChecklist.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.AccessibilityChecklist"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Accessibility Checklist.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Accessibility Checklist documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Accessibility Checklist (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
            Introduction</h2>
        <p>
            The <i>Design Guidance &ndash; Accessibility Checklist</i> document provides you with a testing
            framework for achieving accessibility within clinical applications. It enhances
            patient safety and clinical application usability by providing accessibility checkpoints
            for testing specific interface components. 
        </p>
    </div>
    <div class="last section">
        <h2>
            Summary</h2>
        <p>
            The guidance provides checkpoints and testing methodologies for the accessibility principles introduced by the <a href="accessibilityprinciples.aspx" title="Link to Accessibility Principles Guidance page">Accessibility Principles</a>.
        </p>
    </div>
</asp:Content>
