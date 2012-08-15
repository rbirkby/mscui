<%@ Page Title="" Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true" CodeBehind="FilteringSortingAndGrouping.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.FilteringSortingAndGrouping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageSpecificHeadTags" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Guidance -- Filtering Sorting and Grouping.pdf"
            	Target="_blank" ToolTip="Links to Design Guidance - Filtering, Sorting and Grouping documentation">
            
               	<img src="../images/SFTheme/pdf.png" alt="PDF Download" />
               	<span>Design Guidance &ndash; Filtering, Sorting and Grouping (PDF format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <h2>Introduction</h2>
	<p>
	    The <i>Design Guidance &ndash; Filtering, Sorting and Grouping</i> document provides you with guidance and recommendations for filtering, sorting and grouping any clinical data within a single patient record displayed in a tabular form.  
	    It enhances patient safety and clinical usability by providing guidance for:
	</p>
	<ul>
	    <li>Promoting consistent use of filtering, sorting and grouping for all users across clinical applications and care settings</li>
	    <li>Endeavouring to ensure that the filtering, sorting and grouping functionality are patient-safe</li>
	    <li>Providing functionality that will aid in the analysis and interpretation of data</li>
	    <li>Providing visual cues to inform the user that the displayed information is a filtered sub-set so that accurate clinical decisions are made</li>
	</ul>
    </div>
	<div class="last section">
        <h2>Summary</h2>
        <p>
            The guidance focuses on: 
        </p>    
        <ul>
	    <li>Specifying controls to initiate the filtering, sorting and grouping processes</li>
	    <li>Specifying single or multiple criteria to create a filter expression, including attribute selection, operator usage, value entry, and combining criteria</li>
	    <li>Displaying filtered data and notification of the filters in operation</li>
	    <li>Creating sort criteria expressions, including the specification of single and multiple sort attributes, the sort order of data values, and applying several criteria at the same time</li>
	    <li>Displaying sorted data and providing indicators to show how the results have been sorted</li>
    	    <li>Removing filter expressions and sort criteria</li>
	    <li>Sorting data into groups, including the use of titles and icons to represent groups, and expanding or collapsing groups</li>
        </ul>    
    </div>
</asp:Content>
