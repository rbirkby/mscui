<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="AddressDisplay.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.AddressDisplay"
    Title="Guidance - Address Information Input and Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Address Input and Display.pdf"
            	Target="_blank" ToolTip="Links to Design Guidance - Address Input and Display documentation">
            
               	<img src="../images/SFTheme/pdf.png" alt="PDF Download" />
               	<span>Design Guidance &ndash; Address Input and Display (PDF format)</span>
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
        	The <i>Design Guidance &ndash; Address Input and Display</i>
        	document describes guidance and recommendations for the input and display of UK and 
		non-UK postal addresses in clinical applications. It enables flexible address input and display, and 
		enhances clinical application usability by:
        </p>
        <ul>
        	<li>Providing a predictable and easily recognizable format for the display of address information</li>
        	<li>Providing efficient controls for the input of UK and non-UK patient address</li>
        	<li>Providing a means of locating UK postcodes and addresses to improve data quality</li>
		<li>Ensuring consistency with formats commonly used in public applications and websites, 
		and conforming to international standards</li>
        </ul>
    </div>
    <div class="last section">
        <h2>
            	Summary</h2>
        <p>
            	Guidance is given on the input and display of addresses and focuses on:
        </p>
        <ul>
            	<li>The use of In-Form (vertical) and In-Line (horizontal) style controls to enter UK and non-UK addresses</li>
            	<li>The display of UK and non-UK addresses which, depending on the application&rsquo;s requirements, may 
		comprise of the house number and street, town or city, postcode, county and country</li>
		<li>The structure, formatting, size and permissible characters for each address element field</li>
		<li>The provision of instructional text for address elements, including the use of field labels, prompts and tooltips</li>
		<li>The presentation of a UK postcode finder</li>
		<li>The presentation of a UK address finder</li>
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
                        <a href="QuickGuides/Address" title="Link to Address Input and Display Quick Implementation Guide">Address Input and Display</a>
                    </td>
                    <td>
                        <a href="CribSheets/Crib Sheet for Address Display.pdf" target="_blank" title="Link to Address Display Crib Sheet">Address Display (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td />
                    <td>
                        <a href="CribSheets/Crib Sheet for UK Address Input.pdf" target="_blank" title="Link to UK Address Input Crib Sheet">UK Address Input (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td />
                    <td>
                        <a href="CribSheets/Crib Sheet for Non UK Address Input.pdf" target="_blank" title="Link to Non-UK Address Input Crib Sheet">Non-UK Address Input (PDF Format)</a>
                    </td>
                </tr>
            </tbody>            
        </table>
    </div>  
    
</asp:Content>
