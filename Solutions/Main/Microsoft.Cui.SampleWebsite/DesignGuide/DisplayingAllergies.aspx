<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="DisplayingAllergies.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.DisplayingAllergies"
    Title="Guidance - Displaying Adverse Drug Reaction Risks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>            
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Displaying Adverse Drug Reaction Risks.pdf"
            	Target="_blank" ToolTip="Links to Design Guidance - Displaying Adverse Drug Reaction Risks documentation">
            
               	<img src="../images/SFTheme/pdf.png" alt="PDF Download" />
               	<span>Design Guidance &ndash; Displaying Adverse Drug Reaction Risks (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
        	Introduction</h2>
        <p>
        	The <i>Design Guidance &ndash; Displaying Adverse Drug Reaction Risks</i> document provides 
        	you with guidance and recommendations for indicating which drugs present a risk to a patient, based upon
		adverse drug reactions the patient has had in the past. It addresses how to display a summary of these drugs
		and the nature of the reactions the patient has suffered, so that clinicans can decide whether or not to 
		prescribe these in the future.      	
        </p>
	<p>This guidance should ensure that clinical applications enhance patient safety and clinical application usability by:
	</p>        
        <ul>
            <li>Clearly displaying the presence, or absence, of any known adverse drug reaction risks for a patient</li>
            <li>Indicating the type of reaction that the patient has experienced in the past</li>
	    <li>Presenting links to existing notes which descrive the reaction events that justify why the patient is deemed
		at risk of reacting to the drug</li>         
        </ul>
    </div>
    <div class="last section">
        <h2>
            	Summary</h2>
        <p>
            	Guidance is given on displaying adverse drug reaction risks and focuses on: 
        </p>    
        <ul>
            <li>Displaying adverse drug reaction risks as a list</li>
            <li>Defining the structure and the content of the list </li>
            <li>Formatting of the elements within the list</li>
            <li>Displaying 'no known' adverse drug reaction risks</li>
        </ul>    
    </div>
</asp:Content>
