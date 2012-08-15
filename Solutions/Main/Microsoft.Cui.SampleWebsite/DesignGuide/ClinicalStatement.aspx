<%@ Page Title="" Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="ClinicalStatement.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.ClinicalStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Groundwork Exploration -- Display of Clinical Statements.pdf" Target="_blank" ToolTip="Links to Design Groundwork Exploration - Display of Clinical Statements documentation">                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Groundwork Exploration &ndash; Display of Clinical Statements (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <h1>
            Groundwork &ndash; Display of Clinical Statements</h1>
        <h2>
            Introduction
        </h2>
        <p>
            The <i>Design Groundwork Exploration &ndash; Display of Clinical Statements</i>
            document addresses the problem that <a href="http://www.ihtsdo.org/snomed-ct/"
                target="_blank" title="Opens the SNOMED Clinical Terms website (New Window)">SNOMED&nbsp;CT</a><sup>&reg;</sup>
            encoded clinical statements do not have a standardized display format. This has
            led to a lack of consistency in user interfaces creating the potential for misinterpretation
            when viewing such statements. This document initiates an assessment of the patient
            safety risks of alternatives to the display of such statements to inform future
            design decisions.</p>
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
            The design exploration addresses the following scope areas:</p>
        <ul>
            <li>Groundwork for future guidance on the display of encoded clinical statements for
                use in the delivery of clinical care</li>
            <li>Subset of existing clinical statement &lsquo;patterns&rsquo;, with reference to the UK National
                Health Service (NHS) <a href="http://www.connectingforhealth.nhs.uk/systemsandservices/data/lra"
                target="_blank" title="Opens the Logical Record Architecture website (New Window)">Logical Record Architecture (LRA)</a> and standardized headings
                from the UK Royal College of Physicians (RCP) work on <a href="http://www.rcplondon.ac.uk/clinical-standards/hiu/medical-records/Pages/Overview.aspx"
                target="_blank" title="Opens the Medical Record Keeping Standards website (New Window)">Standards for Record Keeping</a></li>
            <li>Indicating ability to interact with the encoded clinical statement (for example,
                to view further associated information or access the edit mode for that statement)</li>
        </ul>
    </div>
</asp:Content>
