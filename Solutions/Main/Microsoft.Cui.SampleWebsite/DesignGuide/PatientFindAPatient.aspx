<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="PatientFindAPatient.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.PatientFindAPatient"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Find a Patient.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Find a Patient documentation">
            
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Find a Patient (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
            Introduction</h2>
        <p>
            The <i>Design Guidance &ndash; Find a Patient</i> document provides you with guidance
            and recommendations for implementing patient search functionality in clinical applications.
            It enables reliable and robust searching of patient records, while enhancing patient
            safety and providing an effective user experience, by:
        </p>
        <ul>
            <li>Facilitating rapid and effective searching of patient records</li>
            <li>Reducing the likelihood of failed or erroneous searches</li>
            <li>Supporting relevant information governance rules and policies within a usable and
                clear workflow</li>
            <li>Facilitating consistency with the design of other interface components</li>
        </ul>
        </div><div class="last section">
        <h2>
            Summary
        </h2>
        <p>
            The guidance focuses on the dynamics of searching national patient databases, but
            also identifies higher level principles that can be applied across a broader range
            of scenarios. It includes recommendations for:
        </p>
        <ul>
            <li>Determining application context of patient search</li>
            <li>Eliciting search criteria</li>
            <li>Supporting relevant information governance rules and policies</li>
            <li>Presenting search results</li>
            <li>Providing repair strategies for failed searches</li>
            <li>Displaying query refinement forms</li>
        </ul>
    </div>
</asp:Content>
