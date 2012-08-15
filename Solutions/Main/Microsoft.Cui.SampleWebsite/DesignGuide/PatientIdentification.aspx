<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="PatientIdentification.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.PatientIdentification"
    Title="Guidance - Patient ID Number Input and Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Patient Banner.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Patient Banner documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Patient Banner (PDF&nbsp;format)</span>
            </asp:HyperLink>
            
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <p>This Design Guidance is one of nine documents relating to patient demographic data which have been adopted by the 
            Information Standards Board for Health and Social Care (ISB) 
            as a full standard for implementation in healthcare systems. 
            Further information can be found at 
            <a target="_blank" href="http://www.isb.nhs.uk/docs/cui" title="Link to the Information Standards Board website (New Window)">Information Standards Board</a>.</p>
        <h2>
            	Introduction</h2>
        <p>
            	The <i>Design Guidance &ndash; Patient Banner</i>
            	document provides you with guidance and recommendations for implementing patient banners in clinical applications. A patient banner
		is an area within a clinical user interface (UI) that provides key information in a consistent and unambiguous manner so that
		patients can be accurately identified and matched with their associated records. It enables patient safety and clinical application
		usability by:
        </p>
        <ul>
            	<li>Reducing the likelihood of errors when matching patients with their care</li>
            	<li>Facilitating rapid and accurate patient identification</li>
		<li>Increasing clinical effectiveness by providing support for related tasks and links to useful information
        </ul>
        </div>
        <div class="last section">
        <h2>
            Summary
        </h2>
	<div>        
	<p>
            The guidance focuses on patient banner design and includes recommendations for:
        </p>
        <ul>
            <li>Application context, including where to position the patient banner in the application</li>
            <li>Identifying what information to display in the patient banner, including the use of minimum data sets and information groupings</li>
            <li>identifying how to display information within the patient banner, including positioning and the use of labels</li>
        </ul>
    </div>
    
    <div class="relatedResources">
        <div>
            <h2>Associated Quick Reference Guides</h2>
            <p>Quick Implementation Guides (QIG) present Design Guidance content in a more consumable, visual manner. Each QIG contains all design guidance points (mandatory and recommended) accompanied by example diagrams to illustrate their application.</p>  
            <p>A set of Crib Sheets is provided that also supports Design Guidance interpretation. Crib Sheets are single page visual summaries of the key guidance points. Each crib sheet focuses on a specific area of guidance, so some design guidance documents have more than one. Hence, you can download the crib sheet that is pertinent to the particular topic you are interested in.</p>
            <p>These documents are offered by way of quick reference to already familiar guidance. However, remember that the full and definitive versions of the Design Guidance include further usage examples (showing both correct and incorrect application) and detail the rationale behind the guidance.</p>
        </div>
        <table>
            <colgroup>
                <col width="50%" />
                <col width="50%" />
            </colgroup>
            <thead>
                <tr>
                    <th>
                        Quick Implementation Guides
                    </th>
                    <th>
                        Crib Sheets
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <a href="QuickGuides/PatientBanner" title="Link to Patient Banner Quick Implementation Guide">Patient Banner</a>
                    </td>
                    <td>
                        <a href="CribSheets/Crib Sheet for Patient Banner Default View.pdf" target="_blank" title="Link to Patient Banner Default View Crib Sheet">Patient Banner Default View (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td />
                    <td>
                        <a href="CribSheets/Crib Sheet for Patient Banner Expanded.pdf" target="_blank" title="Link to Patient Banner Expanded Crib Sheet">Patient Banner Expanded (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td />
                    <td>
                        <a href="CribSheets/Crib Sheet for Patient Banner for Deceased Patients.pdf" target="_blank" title="Link to Patient Banner for Deceased Patients Crib Sheet">Patient Banner for Deceased Patients (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td />
                    <td>
                        <a href="CribSheets/Crib Sheet for Patient Banner Format.pdf" target="_blank" title="Link to Patient Banner Format Crib Sheet">Patient Banner Format (PDF Format)</a>
                    </td>
                </tr>
            </tbody>            
        </table>
    </div>
    
</asp:Content>
