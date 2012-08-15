<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="TerminologyCodedInformation.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.TerminologyCodedInformation"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Terminology -- Display Standards for Coded Information.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Terminology Display Standards for Coded Information documentation">
            
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Terminology Display Standards for Coded Information (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
            Introduction</h2>
        <p>
            The <i>Design Guidance &ndash; Terminology Display Standards for Coded Information</i> document
            provides recommendations on how coded information used in a medical note-taking
            environment can be displayed at a user interface level.
        </p>
        </div><div class="last section">
        <h2>
            Summary
        </h2>
        <p>
            Guidance is given on display standards for coded information and focuses on:
        </p>
        <ul>
            <li>The entry and selection of text, including display standards for communicating encodable
                text</li>
            <li>Fully-qualified concepts, including display standards for encoded text</li>
            <li>Concepts which are not encoded but are displayed in the clinical notes</li>
        </ul>
    </div>
</asp:Content>
