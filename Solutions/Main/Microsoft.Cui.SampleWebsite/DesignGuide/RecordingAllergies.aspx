<%@ Page Title="" Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true" CodeBehind="RecordingAllergies.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.RecordingAllergies" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageSpecificHeadTags" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Guidance -- Recording Adverse Drug Reaction Risks.pdf"
            	Target="_blank" ToolTip="Links to Design Guidance - Recording Adverse Drug Reaction Risks documentation">
            
               	<img src="../images/SFTheme/pdf.png" alt="PDF Download" />
               	<span>Design Guidance &ndash; Recording Adverse Drug Reaction Risks (PDF format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>

        <h2>Introduction</h2>
	<p>
	   The <i>Design Guidance &ndash; Recording Adverse Drug Reaction Risks</i> document provides you with guidance and recommendations for recording adverse drug reaction risks. An adverse drug reaction is a negative physiological response to a pharmaceutical product, 
	   where, in normal cases, the dosage administered would be non toxic. It enhances patient safety and clinical usability by: 
	</p> 
	<ul>
	    <li>Ensuring clear and consistent recording of the presence, or absence, of past idiosyncratic reactions to a drug</li>
	    <li>Ensuring that sufficient information is recorded about a patient&#39;s adverse drug reactions to enable 
            the clinician to decide whether or not to prescribe a drug 
		or to determine whether a patient&#39;s symptoms are attributable to a reaction</li>
	    <li>Avoiding the likelihood of the offending drugs being administered to the patient again</li>
        </ul>
   </div>
 <div class="last section">
        <h2>Summary</h2>
        <p>
            Guidance is given on recording adverse drug reaction risks within the Adverse Drug Reaction Summary. It includes recommendations for: 
        </p>    
        <ul>
	    <li>The initiation, structure and labeling of dialogs for adding, editing and removing 
            adverse drug reaction risks, and recording &#39;no known&#39; adverse drug reaction risks</li>
	    <li>The system identifying potential adverse drug reaction risks based on 
            existing clinical notes, before presenting it for inclusion in the Adverse Drug 
            Reaction Summary</li>
	    <li>The provision of a visual cue for new or changed adverse drug reaction risks</li>
        </ul>    
    </div>
</asp:Content>
