<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="NameDisplayInput.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.NameDisplayInput"
    Title="Guidance - Patient Name Input and Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Patient Name Input and Display.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Patient Name Input and Display documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Patient Name Input and Display (PDF format)</span>
            </asp:HyperLink>
            <br />           
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <p>This Design Guidance is one of nine documents relating to patient demographic data which have been adopted by the 
            Information Standards Board for Health and Social Care (ISB) 
            as a full standard for implementation in healthcare systems. 
            Further information can be found at 
            <a target="_blank" href="http://www.isb.nhs.uk/docs/cui" title="Link to the Information Standards Board website (New Window)">Information Standards Board</a>.</p>
        <h2>
            Introduction
        </h2>
	<p>The <i>Design Guidance &ndash; Patient Name Input and Display</i> document describes  guidance and recommendations for the input and display of patient name data in clinical 
	applications. It enables unambiguous input and display of patient name data, while enhancing patient safety and clinical application 
	usability by:
	<ul>
		<li>Providing efficient input controls for entering patient names in a fast and easy manner</li>
		<li>Ensuring a consistent, clear and easily recognizable visual 
		representation of patient name elements, individually and when combined</li>
		<li>Specifying the &lsquo;Title&rsquo;, &lsquo;Family Name&rsquo; and &lsquo;Given Name&rsquo; as the minimum data set required to safely identify a patient</li>
		<li>Specifying the &lsquo;Middle Name&rsquo;, &lsquo;Suffix&rsquo; and &lsquo;Preferred Name&rsquo; as optional data elements to 
		improve data quality</li>

	</ul>

	</p>
    </div>
    <div class="first section">
        <h2>
            Summary</h2>
	<p>
	Guidance is given on the input and display of patient name and focuses on:
	</p>   
	<ul>
		<li>The use of InForm (vertical) and InLine (horizontal) controls to enter and display all elements of the patient name </li>
		<li>The structure, format, size and entry requirements for each element of the patient name </li>
		<li>The provision of instructional text for patient name data elements, including field labels, prompts and tooltips</li>
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
                        <a href="QuickGuides/PatientName" title="Link to Patient Name Input and Display Quick Implementation Guide">Patient Name Input and Display</a>
                    </td>
                    <td>
                        <a href="CribSheets/Crib Sheet for Patient Name Display.pdf" target="_blank" title="Link to Patient Name Display Crib Sheet">Patient Name Display (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td />
                    <td>
                        <a href="CribSheets/Crib Sheet for Patient Name Input.pdf" target="_blank" title="Link to Patient Name Input Crib Sheet">Patient Name Input (PDF Format)</a>
                    </td>
                </tr>
            </tbody>            
        </table>
    </div>  
    
</asp:Content>
