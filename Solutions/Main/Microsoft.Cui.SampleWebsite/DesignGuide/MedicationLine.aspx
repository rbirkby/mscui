<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="MedicationLine.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.MedicationLinePage"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Medication Line.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Medication Line documentation">
            
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Medication Line (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
            Introduction</h2>
        <p>
           The <i>Design Guidance &ndash; Medication Line</i>
           document provides you with both guidance and recommendations for the display of, and interaction with, information used to express a single medication. It enhances patient safety and clinical application usability by:
        </p>
        <ul>
            <li>   
	            Ensuring a consistent visual representation of medication information for all contexts, while providing the flexibility needed for specific contexts
	        </li>
	        <li>
	            Providing clarity and simplicity for displaying medication information
	        </li>    
	        <li>
	            Mitigating risks of misinterpretation, and incorrect selection, of medication information
	        </li>	       
	     </ul>
        </div>
    <div class="update section">
       <label>Note:</label>
	 <P>In February 2009, the document <i>Design Guidance &ndash; Medication Line</i> was updated. This version replaces all previous versions of the guidance for a medication line.</P>
    </div>              
        <div class="last section">
        <h2>
            Summary
        </h2>
        <p>
           The guidance focuses on:
        </p> 
        <ul>    
          <li>
               Formatting generic drug and brand names
          </li>
          <li>         
               Displaying dose and strength
          </li>
          <li>         
               Displaying volume, rate and duration
          </li>
          <li>         
               Attribute order
          </li>
          <li>         
               Truncation, abbreviation, numbers and symbols
          </li>
          <li>         
               Wrapping, line spacing, line breaks and separators
          </li>
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
                        <a href="QuickGuides/MedicationLine" title="Link to Medication Line Quick Implementation Guide">Medication Line</a>
                    </td>
                    <td>
                        <a href="CribSheets/Crib Sheet for Medication Line Basics.pdf" title="Link to Medication Line Basics Crib Sheet" target="_blank">Medication Line Basics (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td />
                    <td>
                        <a href="CribSheets/Crib Sheet for Medication Line Details.pdf" target="_blank" title="Link to Medication Line Details Crib Sheet">Medication Line Details (PDF Format)</a>
                    </td>
                </tr>
            </tbody>            
        </table>
    </div>
    
</asp:Content>
