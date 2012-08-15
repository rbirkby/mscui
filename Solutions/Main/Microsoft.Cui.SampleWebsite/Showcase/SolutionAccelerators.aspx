<%@ Page Language="C#" MasterPageFile="~/Navigational.master" AutoEventWireup="true"
    Inherits="Microsoft.Cui.SampleWebsite.ShowcaseSolutionAccelerators" EnableViewState="false"
    Title="Untitled Page" CodeBehind="SolutionAccelerators.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="navigationPageContent" runat="Server">
    <div class="first section">
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
    </div>
    <div class="last section">
        <h2>
            Clinical Documentation Solution Accelerator</h2>
        <p>
            The Clinical Documentation Solution Accelerator (CDSA) for the Microsoft Office
            system is a Connected Health Platform resource that enables you or your customer
            to define and create clinically encoded documents. Documents can be created based
            upon ready-made templates (for example, for a Discharge Summary or a Referral) retrieved
            using the Microsoft Office System.
        </p>
        <p>
            Template designers can create Office Word templates that also provide for the entered
            data to be mapped to a machine-readable, clinical encoding taxonomy (such as <a href="http://www.ihtsdo.org/snomed-ct/"
                target="_blank" title="Opens the SNOMED Clinical Terms website (New Window)">SNOMED
                CT</a><sup>&reg;</sup>). Clinicians simply complete the clinical forms created
            from those CDSA-enabled templates. The Office Word (.docx) file contains both human-readable
            and machine-readable clinical information (such as a Health Level 7 Clinical Document
            Architecture (HL7 CDA) representation).
        </p>
        <p>
            You can use CDSA as the foundation for clinical document workflows (for example,
            in combination with Microsoft Office SharePoint Server 2010). Tailor those to the
            specific Electronic Medical Record (EMR) or Electronic Health Record (EHR) systems
            in your own or your customer's healthcare environment. In addition, CDSA will enable
            you to create clinical documents suitable for upload to a Personal Health Record
            (PHR) system (such as Microsoft HealthVault) or to other external storage.
        </p>
        <a href="../CDSA.htm" target="_blank" title="Link to Clinical Documentation Solution Accelerator page (New Window)">
            Go to the CDSA page to learn more, view videos and to download CDSA</a>
        <img src="../images/CDSA/CDSASolutionAccelerator.jpg" alt="'Clinical Documentation Solution Accelerator' Image"
            style="margin-top: 15px" />
    </div>
</asp:Content>
