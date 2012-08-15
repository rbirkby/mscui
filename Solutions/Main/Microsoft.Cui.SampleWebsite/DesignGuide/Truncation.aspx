<%@ Page Title="" Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="Truncation.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.Truncation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig" style="width: 250px">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Groundwork Exploration -- Truncation of Clinical Terms.pdf" Target="_blank" ToolTip="Links to Design Groundwork Exploration - Truncation of Clinical Terms document">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Groundwork Exploration &ndash; Truncation of Clinical Terms (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <h1>
            Groundwork &ndash; Truncation of Clinical Terms</h1>
        <h2>
            Introduction
        </h2>
        <p>
            The <i>Design Groundwork Exploration &ndash; Truncation of Clinical Terms</i> document
            addresses the problem that existing truncation of clinical terms may be more prone
            to misinterpretation and mis-selection than terms displayed in full. The document
            initiates an assessment of the patient safety risks of alternatives to truncation
            to inform future design decisions.</p>
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
            Summary</h2>
        <p>
            The design exploration addresses the following scope areas:
        </p>
        <ul>
            <li>How to truncate coded clinical terms displayed in a selection list (exploration
                and research is limited to <a href="http://www.ihtsdo.org/snomed-ct/" target="_blank"
                    title="Opens the SNOMED Clinical Terms website (New Window)">SNOMED&nbsp;CT</a><sup>&reg;</sup>)</li>
            <li>How to truncate coded clinical terms displayed in a list or table (limited feedback
                based on evolving work presented in <a href="ClinicalStatement.aspx" title="Link to Display of Clinical Statements Guidance page">
                    Display of Clinical Statements</a>)</li>
            <li>How to access the full text of a truncated clinical term (limited feedback based
                on evolving work presented in <a href="ClinicalStatement.aspx" title="Link to Display of Clinical Statements Guidance page">
                    Display of Clinical Statements</a>)</li>
        </ul>
    </div>
</asp:Content>
