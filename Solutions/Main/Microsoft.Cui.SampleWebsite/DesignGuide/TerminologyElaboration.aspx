<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="TerminologyElaboration.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.TerminologyElaboration"
    Title="Terminology Elaboration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Terminology -- Elaboration.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Terminology Elaboration documentation">
            
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Terminology Elaboration (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
            Introduction</h2>
        <p>
            The <i>Design Guidance &ndash; Terminology Elaboration</i> document provides
            recommendations on how additional explanatory information may be entered at terminology
            user interfaces. The aim of the guidance is to provide clinicians with the freedom
            and flexibility to comprehensively and accurately express their notes in a note-taking
            environment utilizing a <a href="http://www.ihtsdo.org/snomed-ct/" target="_blank" title="Opens the SNOMED Clinical Terms website (New Window)">SNOMED&nbsp;CT</a><sup>&reg;</sup> database which may use:
        </p>
        <ul>
            <li>Forms, where the user selects set options rather than entering text</li>
            <li>Single concept matching, where the user enters a note for a single clinical concept
                and selects an appropriate match returned by the SNOMED&nbsp;CT database</li>
            <li>Text parser matching, where the user enters textual notes and the system matches
                words and phrases against the SNOMED&nbsp;CT database</li>
        </ul>
        </div><div class="last section">
        <h2>
            Summary
        </h2>
        <p>
            Guidance is given on how to add additional information to a SNOMED&nbsp;CT expression
            when the clinician believes the encoding on its own does not provide sufficient
            meaning. Elaboration may involve:
        </p>
        <ul>
            <li>Adding unstructured text to the expression to give the expression further meaning
            </li>
            <li>Finding a SNOMED&nbsp;CT expression and adding text to it, as in a single concept matching
                approach</li>
            <li>Matching a SNOMED&nbsp;CT expression from within a passage of text and leaving some of
                the text itself un-encoded but associated with the encoded expression</li>
            <li>Adding a qualifier to a SNOMED&nbsp;CT expression, using the qualifiers offered by the
                system, such as the severity of a condition</li>
            <li>Adding or selecting numerical or date and time values for a SNOMED&nbsp;CT expression</li>
        </ul>
    </div>
</asp:Content>
