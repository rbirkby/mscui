<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="TablesGraphsDisplay.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.TablesGraphsDisplay"
    Title="Guidance - Displaying Graphs and Tables" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Displaying Graphs and Tables.pdf"
            	Target="_blank" ToolTip="Links to Design Guidance - Displaying Graphs and Tables documentation">
            
               	<img src="../images/SFTheme/pdf.png" alt="PDF Download" />
               	<span>Design Guidance &ndash; Displaying Graphs and Tables (PDF format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
        	Introduction</h2>
        <p>
        	The <i>Design Guidance &ndash; Displaying Graphs and Tables</i>
        	document provides you with guidance and recommendations for identifying 'vital signs' data values and patterns over time, 
		both as single and multiple data series, within both graphs and tables. It enhances patient safety and clinical application 
		usability by:
        </p>
        <ul>
        	<li>Ensuring specific data values and critical patterns, such as trends and significant interactions, are interpreted correctly</li>
        	<li>Ensuring a consistent visual presentation of data values and data series within graphs and tables</li>
        	<li>Reducing the chance of incorrect or delayed diagnosis and treatment due to incorrect analysis of the data</li>
	    </ul>
    </div>
    <div class="update section">
       <label>Note:</label>
	 <P>In February 2009, the document <i>Design Guidance &ndash; Displaying Graphs and Tables</i> was updated. This version replaces all previous versions of the guidance for displaying graphs and tables.</P>
    </div>   
    <div class="last section">
        <h2>
            	Summary</h2>
        <p>
            	Guidance is given on displaying tables and graphs and focuses on:
        </p>
        <ul>
            <li>Providing appropriate layout and formatting of graphs and tables for data values and data series</li>
            <li>Providing consistent scaling and appropiate axes</li>
            <li>Analyzing data values against normal ranges, and in a series of data values taken over time, 
		including the use of interpolation</li>
		    <li>Representing the same data values within both graphs and tables</li>
		    <li>Demonstrating where access to supporting contextual data will be necessary</li>
	    </ul>
    </div>
</asp:Content>
