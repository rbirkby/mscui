<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="TerminologyMatching.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.TerminologyMatching"
    Title="Terminology Matching" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Terminology -- Matching.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Terminology Matching documentation">
            
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Terminology Matching (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
            Introduction</h2>
        <p>
            The <i>Design Guidance &ndash; Terminology Matching</i> document provides
            guidance and recommendations for the matching of <a href="http://www.ihtsdo.org/snomed-ct/" target="_blank" title="Opens the SNOMED Clinical Terms website (New Window)">SNOMED&nbsp;CT</a><sup>&reg;</sup> clinical terms in
            user interfaces used in a medical note-taking environment.
        </p>
        <p>
            The note-taking environment may use:
        </p>
        <ul>
            <li>Forms, where the user selects set options rather than entering text</li>
            <li>Single concept matching, where the user enters a note for a single clinical concept
                and selects an appropriate match returned by the SNOMED&nbsp;CT database</li>
            <li>Text parser matching, where the user enters textual notes and the system matches
                words and phrases against the SNOMED&nbsp;CT database</li>
        </ul>
        <p>
            The aim of the guidance is to improve patient care and safety by decreasing the
            likelihood of patient notes being miswritten or important details being missed,
            with the standardization of data reducing potential misunderstandings due to nuances
            in expression or writing style. The guidance also aims to provide a flexible and
            efficient means of note taking which will not overly interfere with the clinician-patient
            interaction.
        </p>
        </div><div class="last section">
        <h2>
            Summary
        </h2>
        <p>
            Guidance is given for each of the note-taking environments and focuses on:
        </p>
        <ul>
            <li>Contexts where the user selects an option</li>
            <li>Entering text for a single clinical concept</li>
            <li>Displaying expression matches </li>
            <li>Browsing alternative matches and refining selected match(es) using free text</li>
            <li>Leaving entered notes as unstructured text</li>
            <li>Confirming match(es)</li>
            <li>Offering the opportunity to request improvements on the matches presented by the
                system</li>
        </ul>
    </div>
</asp:Content>
