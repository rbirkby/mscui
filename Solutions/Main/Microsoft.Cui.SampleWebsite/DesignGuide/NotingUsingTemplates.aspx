<%@ Page Title="" Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="NotingUsingTemplates.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.NotingUsingTemplates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Groundwork Exploration -- Noting Using Templates.pdf" Target="_blank" ToolTip="Links to Design Groundwork Exploration - Noting Using Templates document">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Groundwork Exploration &ndash; Noting Using Templates (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <h1>
            Groundwork &ndash; Noting Using Templates</h1>
        <h2>
            Introduction
        </h2>
        <p>
            The <i>Design Groundwork Exploration &ndash; Noting Using Templates</i> document
            examines how to access and populate electronic data entry structures (&lsquo;templates&rsquo;)
            in a clinical environment.
        </p>
        <p>
            This document considers these aspects of noting using templates:
            <ul>
                <li>Searching within data fields</li>
                <li>The manifestation of templates</li>
                <li>The structure of templates in terms of sections and fields</li>
            </ul>
        </p>
        <p>
            The document focuses on the entry of data into an acute medical admissions form implied by the relevant headings from the UK Royal College of Physicians (RCP) work on <a href="http://www.rcplondon.ac.uk/clinical-standards/hiu/medical-records/Pages/Overview.aspx"
                target="_blank" title="Opens the Medical Record Keeping Standards website (New Window)">Standards for Record Keeping</a>.</p>
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
            <li>Basic searching &ndash; Layout and ordering of search results</li>
            <li>Section search &ndash; Accessing the feature, displaying results, inserting sections</li>
            <li>Additional template search &ndash; Labeling, displaying alongside internal results</li>
            <li>Search on abbreviations &ndash; Labeling, prioritization in results</li>
            <li><a href="http://www.ihtsdo.org/snomed-ct/" target="_blank" title="Opens the SNOMED Clinical Terms website (New Window)">
                SNOMED&nbsp;CT</a><sup>&reg;</sup> search trigger &ndash; Triggering, display, access</li>
            <li>Free text trigger &ndash; Tagging, display, access</li>
            <li>Browsing &ndash; Location of browse, default visibility</li>
            <li>Revisiting and adding templates &ndash; Opening an existing section, opening new fields</li>
            <li>Reordering &ndash; Standard reordering, reordering by entry</li>
        </ul>
    </div>
</asp:Content>
