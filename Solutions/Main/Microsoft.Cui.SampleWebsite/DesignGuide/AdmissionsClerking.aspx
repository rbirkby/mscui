<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="AdmissionsClerking.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.AdmissionsClerking"
    Title="Guidance - Clinical Noting in Forms: Admissions Clerking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Guidance -- Clinical Noting in Forms -- Admissions Clerking.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Clinical Noting in Forms: Admissions Clerking documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Clinical Noting in Forms: Admissions Clerking (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <h2>
            Introduction
        </h2>
	<p>The <i>Design Guidance &ndash; Clinical Noting in Forms: Admissions Clerking</i> document provides guidance and recommendations for the
	design and functionality needed to enable electronic clinical noting, especially in the area of acute admissions clerking.
	</p>
	<p>The admissions clerking guidance shows not only how to feature standard user interface fields but also how to capture more specialized
	data entry structures. In this and other ways, the guidance exemplifies potential solutions for a wider set of clinical noting user interface
	challenges.</p>
	<p>The admissions clerking form is particularly significant for patient safety and clinical usability because:
	</p>
	<ul>
	    <li>It is typically the starting point for documenting a patient's stay in hospital</li>
	    <li>In some cases, it acts as the cover sheet for the patient's progress notes</li>
	    <li>It is often the first point of reference for clinicians unfamiliar with the patient (sections such as &#145;Presenting Complaint&#146; and &#145;Past
	    Medical History&#146; provide an important overview)</li>
	</ul>
	</div>
	<div class="last section">
        <h2>
            Summary</h2>
	    <p>The guidance describes how to make entries on the admissions clerking form, the methods for displaying and arranging sets of fields 
	    and outlines general principles for structuring forms. It includes recommendations for:</p>
	    <ul>
		    <li>Entering, editing or deleting clinical concepts and other details within the clinician's current entry session:
		        <ul>
		            <li>As part of this topic, the guidance addresses how to capture lists of notes, using the &#145;Past Medical History&#146; list as an example</li>
		        </ul>
		    </li>
		    <li>Hiding and revealing sections of a form</li>
		    <li>Indicating where fields require data</li>
		    <li>Displaying automatically calculated data</li>
		    <li>Displaying previously captured values for a data field</li>
		    <li>Allowing the clinician to add free text to fixed choice data fields</li>
		    <li>General form design, including the use of standard fields, labels and prompts</li>
	    </ul>
   </div>
</asp:Content>
