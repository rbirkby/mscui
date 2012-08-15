<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="TelephoneNoDisplay.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.TelephoneNoDisplay"
    Title="Guidance - Telephone Number Input and Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Telephone Number Input and Display.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Telephone Number Input and Display documentation">
            
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Telephone Number Input and Display (PDF&nbsp;format)</span>
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
	        The <i>Design Guidance &ndash; Telephone Number Input and Display</i>
        	document describes guidance and recommendations for the unambiguous display and input 
		of telephone number data in clinical applications. Additionally, it enhances patient safety and usability of clinical applications by:
        </p>
<ul>
		<li>Providing a mechanism for entering telephone numbers in a consistent and efficient manner</li>
		<li>Logically grouping the telephone number digits into meaningful 
		categories that are easily recognizable and adhere to international standards and conventions</li>
		<li>Facilitating the optional use of international and area codes</li>

</ul>
    </div>
    <div class="last section">
        <h2>
            	Summary</h2>
        <p>
            	The guidance describes:</p>
        <ul>
            	<li>Input controls for entering the telephone number elements, including the implementation of a country selector editable field</li>
            	<li>The display of the full telephone number, including the international code, area code, local number and extension number</li>
            	<li>The structure, formatting, size and entry requirements for each telephone number element</li>
		<li>The provision of instructional text for telephone number elements, including field labels, prompts and tooltips</li>
        </ul>
    </div>
    
    <div class="relatedResources">
        <div>
            <h2>Associated Quick Reference Guides</h2>
            <p>Quick Implementation Guides (QIG) present Design Guidance content in a more consumable, visual manner. Each QIG contains all design guidance points (mandatory and recommended) accompanied by example diagrams to illustrate their application.</p>  
            <p>A Crib Sheet is provided that also supports Design Guidance interpretation. Crib Sheets are single page visual summaries of the key guidance points. Each crib sheet focuses on a specific area of guidance, so some design guidance documents have more than one. Hence, you can download the crib sheet that is pertinent to the particular topic you are interested in.</p>
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
                        <a href="QuickGuides/TelNumber" title="Link to Telephone Number Input and Display Quick Implementation Guide">Telephone Number Input and Display</a>
                    </td>
                    <td>
                        <a href="CribSheets/Crib Sheet for Telephone Number Input and Display.pdf" target="_blank" title="Link to Telephone Number Input and Display Crib Sheet">Telephone Number Input and Display (PDF Format)</a>
                    </td>
                </tr>
            </tbody>            
        </table>
    </div>  
    
</asp:Content>
