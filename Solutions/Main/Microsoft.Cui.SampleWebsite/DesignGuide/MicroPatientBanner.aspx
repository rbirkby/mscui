<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="MicroPatientBanner.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.MicroPatientBanner"
    Title="Guidance - Micro Patient Banner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Micro Patient Banner.pdf"
            	Target="_blank" ToolTip="Links to Design Guidance - Micro Patient Banner documentation">
            
               	<img src="../images/SFTheme/pdf.png" alt="PDF Download" />
               	<span>Design Guidance &ndash; Micro Patient Banner (PDF format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
        	Introduction</h2>
        <p>
        	The <i>Design Guidance &ndash; Micro Patient Banner</i>
        	document provides you with guidance and recommendations for implementing micro patient banners in clinical applications running on handheld computers. </p>
        	<p>
		The 'patient banner' refers to the area of the clinical user interface (UI) that contains demographic information for a patient record. 
		A micro patient banner is a patient banner specifically designed for small screen, handheld devices such as Personal Digital 
		Assistants (PDAs). It displays patient demographic information in a consistent and unambiguous manner so that healthcare staff can 
		ensure the correct patient is identified, and the correct record is displayed. 
        </p>
        <p>The guidance is restricted to PDA usage and aims to:</p>
        <ul>
        	<li>Ensure patients are correctly identified and matched with their patient record and wristband by displaying data items consistently</li>
        	<li>Allow quick access to, and display of, other summary information for a patient (such as the address)</li>
        </ul>
    </div>
    <div class="last section">
        <h2>
            	Summary</h2>
        <p>
            	Guidance is given on implementing micro patient banners and focuses on:
        </p>
        <ul>
            <li>Application context, including where to position the micro patient banner in the application</li>
		    <li>Identifying what information to display in the micro patient banner, including the use of minimum data sets and information groupings</li>
		    <li>Identifying how to display information within the micro patient banner, including the alignment of information and the use of labels</li>
		    <li>Displaying patient names within the micro patient banner
		    <li>Providing a banner for deceased patients, including the identification of information to display and how to display it  	

        </ul>
    </div>
</asp:Content>
