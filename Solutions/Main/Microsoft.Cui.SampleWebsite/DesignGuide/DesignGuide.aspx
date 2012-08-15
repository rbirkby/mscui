<%@ Page Language="C#" MasterPageFile="~/Navigational.master" AutoEventWireup="true"
    Inherits="DesignGuidanceDesignGuide" EnableViewState="false" Title="Untitled Page" Codebehind="DesignGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="navigationPageContent" runat="Server">
    <div class="first section">
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <p>
            The aim of the Microsoft Health CUI Design Guidance is to support the delivery of safe patient care 
            by providing detailed guidance for the standardization of clinical application user interfaces.
        </p>
        <p>
            The Design Guidance is targeted at application providers whose healthcare applications are currently in use, as well as at those 
            developing new healthcare applications in the future. With the primary focus on patient safety, the Design Guidance has been produced 
            through a rigorous user-centred design process that incorporates primary and secondary research, usability testing and consultation with 
            software providers. Patient Safety Assessments (PSAs) are continually performed to ensure the Design Guidance meets safety concerns. 
        </p>
        <p>
            The guidance documents are all free to use under the Microsoft Public license (Ms-PL) the
            terms of which can be found here: <a target="_blank" href="http://mscui.codeplex.com/license"
                title="Link to MSCUI license page on codeplex.com (New Window)">
                http://mscui.codeplex.com/license</a>
        </p>
        <p>
            The following areas of focus are the first areas to be released as part of the Design
            Guidance.
        </p>
    </div>
    <div class="update section">
       <label>Note:</label>
	 <p>The Design Guidance is updated regularly. See the <a href="../About.aspx" title="Link to About page">Change Record &ndash; Guidance and Controls</a> for a list of the documents updated in the latest release.</p>
        
    </div>
    <div class="section">
        <h2>
            Clinical Noting and Terminology</h2>
        <p>
            The Design Guidance provides recommendations on how clinical concepts should be entered and displayed. It includes 
	    guidance for clinical noting (for example, the display of clinical terms within forms). Recommendations for matching <a target="_blank" href="http://www.ihtsdo.org/snomed-ct/" title="Link to SNOMED Clinical Terms website (New Window)" style="white-space:nowrap">SNOMED&nbsp;CT</a><sup>&reg;</sup> 
	    clinical terms is also covered.

        
        </p>
    </div>
    <div class="section">
        <h2>
            Consistent Navigation</h2>
        <p>
            The Design Guidance provides recommendations for the display and interaction in several areas as they apply to healthcare applications. Examples of topics covered include sorting and filtering, standard forms and labels, tablet personal computer design considerations, and icons and symbology.     
        </p>
    </div>
    <div class="section">
        <h2>
            Medications</h2>
        <p>
            The Design Guidance provides recommendations for the display of, and interaction with, a patient's medications. It includes guidance for lists of medications, schedules and events relating to drug administrations,
	    searching for drugs to prescribe, and defining courses of medication. The guidance also defines generic display rules for the formatting and layout of individual medication items.
        </p>
    </div>
    <div class="section">
        <h2>
            Patient Identification</h2>
        <p>
            The Design Guidance provides recommendations for the input and display of patient information for safe identification within clinical applications, including patient names, date of birth, patient identification numbers, and postal addresses.
	    Guidance is also given for patient searching, implementing patient banners, and entering and displaying dates.    
        </p>
    </div>
    <div class="last section">
        <h2>
            Miscellaneous</h2>
        <p>
            This area provides guidance for more diverse areas of clinical application user interface design. For example, the 
	    identification of accessibility requirements for specific interface components, and checkpoints for testing these.
        </p>
    </div>
  
</asp:Content>
