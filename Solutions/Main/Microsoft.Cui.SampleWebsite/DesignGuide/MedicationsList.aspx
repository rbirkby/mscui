<%@ Page Title="" Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true" CodeBehind="MedicationsList.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.MedicationsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageSpecificHeadTags" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Guidance -- Medications List.pdf"
            	Target="_blank" ToolTip="Links to Design Guidance - Medications List documentation">
            
               	<img src="../images/SFTheme/pdf.png" alt="PDF Download" />
               	<span>Design Guidance &ndash; Medications List (PDF format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <h2>Introduction</h2>
	<p>
	   The <i>Design Guidance &ndash; Medications List</i> document provides you with guidance and recommendations for displaying a list of medications for a single patient in a hospital ward environment.
	   The guidance applies to the display of information about medications in a list that is organized using columns and rows, and specifically to the way that the information is organized, and can be manipulated, by the user.
	   It enhances patient safety and clinical application usability by providing guidance for:	</p>
	<ul>
	   
	    <li>Maximizing the number of medications that can be displayed whilst preserving the integrity of the information given</li> 
	    <li>Making the most of screen real estate by managing column widths and list lengths</li> 
 	    <li>Using formatting to draw attention to information and improve readability by reducing unnecessary clutter</li>	    
	    <li>Mitigating risks associated with lists that are too long to be displayed at once</li> 
	    <li>Adhering to models used in current practice, such as &#39;current&#39; and &#39;past&#39; medications </li> 
	    <li>Organizing the list by supporting sorting, grouping and filtering</li> 
	</ul>
    </div>
<div class="last section">
        <h2>Summary</h2>
        <p>
            The guidance describes: 
        </p>    
        <ul>
	    <li>Layout, gridlines and formatting</li>
	    <li>Constraining dimensions for rows, columns and the whole list</li>
	    <li>Displaying dates and status values</li>
	    <li>Displaying Current and Past medications and the recent past notification</li>
	    <li>Filtering Past medications</li>
	    <li>Managing long lists using the Look-ahead Scroll Bar</li>
	    <li>Sorting and Grouping</li>


        </ul>    
    </div>
    
    <div class="relatedResources">
        <div>
            <h2>Associated Quick Reference Guides</h2>
            <p>Quick Implementation Guides (QIG) present Design Guidance content in a more consumable, visual manner. Each QIG contains all design guidance points (mandatory and recommended) accompanied by example diagrams to illustrate their application.</p>  
            <p>These documents are offered by way of quick reference to already familiar guidance. However, remember that the full and definitive versions of the Design Guidance include further usage examples (showing both correct and incorrect application) and detail the rationale behind the guidance.</p>
        </div>
        <table>
            <colgroup>
                <col width="100%" />
            </colgroup>
            <thead>
                <tr>
                    <th>
                        Quick Implementation Guides
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <a href="QuickGuides/MedicationsList" title="Link to Medications List Quick Implementation Guide">Medications List</a>
                    </td>
                </tr>                
            </tbody>            
        </table>
    </div>
    
</asp:Content>
