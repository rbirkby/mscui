<%@ Page Language="C#" MasterPageFile="~/DefaultMaster.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.About"
    Title="About this Website" %>

<asp:Content runat="server" ContentPlaceHolderID="pageSpecificHeadTags">
    <style type="text/css">
        .section
        {
            overflow: hidden; /* New block formatting context */
        }
        .section ul
        {
            font-weight: bold;
        }
        .section ul ul
        {
            font-weight: normal;
        }
        .section ul ul ul
        {
            list-style-type: square;
            font-weight: normal;
            margin-left: 20px;
        }
        .section ul ul ul li
        {
            margin-bottom: 6px;
        }
        .videoSection
        {
            float: right;
            background-color: #052844;
            position: relative; /* Provide a positioned ancestor for the image */
            min-height: 176px;
            overflow: hidden;
        }
        .videoHeading
        {
            width: 163px;
            margin: 0 2px 0 0;
            color: #fff;
            text-align: center;
            cursor: pointer;
            display: block;
            padding: 125px 0 10px 65px;
            position: relative; /* Make positioned so it draw above the image */
        }
        .videoSection a
        {
            text-decoration: none;
            display: block;
        }
        .videoSection a img
        {
            border: none;
            position: absolute; /* Take the image out of the flow */
        }
        h1
        {
            margin: 0 0 8px 8px;
        }
        .relatedResources
        {            
            border-bottom-style: none;
            border: solid 1px #CCC;
            margin:0 4px 0 0;
        }
        .relatedResources p
        {
            background-color: #385484;
        }
        .relatedResources div
        {
            padding: 4px 4px 4px 8px;
        }
        .relatedResources table
        {
            border-collapse: collapse;
            width: 100%;
        }
        .relatedResources table thead
        {
            background-color: #57739B;
            color: White;
            border: solid 1px #888;
            text-transform: none;
            text-align: left;
        }
        .toptitle
        {
            background-color: #385484;
        }
        .blankheader
        {
            height: 2px;
            background-color: White;
        }
        .relatedResources table tbody td
        {
            background-color: #e4e4e4;
            border: solid 1px #ccc;
            padding: 2px 10px 2px 10px;
        }
        .relatedResources table th
        {
            color: White;
            padding-left: 10px;
            font-weight: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="siteContentPlaceHolder" runat="server">
    <h1>
        About this Website</h1>
    <div class="section">
        <h2>
            Community Technology Preview (CTP)</h2>
        <p>
            This release contains CTP versions of the following controls:
            <p>
            <ul>
                <li>MedicationsListView</li>
            </ul>
                As changes may be made between CTP and release versions, no official support or
                statement of quality can be offered for CTP controls prior to the development and
                distribution of release versions.</p>
    </div>
    <div class="section">
        <h2>
            Contact Us</h2>
        <p>
            We welcome feedback on the Design Guidance, the controls and samples and all other
            aspects of this Website. Please use the feedback facilities on our <a href="http://www.codeplex.com/mscui"
                target="_blank" title="Links to the CodePlex Website (New Window)">CodePlex</a> pages
            to let us know what you think.
        </p>
    </div>
    <div class="section">
        <h2>
            Change Record &ndash; Guidance and Controls</h2>
        <p>
            The Change Record acts as a mechanism for tracking the availability of the <a href="#designGuidance"
                title="Links to the Guidance Overview page">Design Guidance</a> documents, <a href="#controls"
                    title="Links to the Controls and Samples Overview page">Controls</a> and
            <a href="#samples" title="Links to the Controls and Samples Overview page">Samples</a>
            for the current Release of the Microsoft Health Common User Interface.
        </p>
        <p>
            <div id="designGuidance" class="relatedResources">
		<!-- Start of table for updates of MSCUI Design Guidance (ordered by date and then alphabetically, most recent at end of table)-->
		<table summary="This table lists changes to the Microsoft CUI Design Guidance.">
                    <col width="30%" />
                    <col width="15%" />
                    <col width="10%" />
                    <col width="45%" />
                    <thead>
                        <tr>
                            <th colspan="4" class="toptitle">
                                DESIGN GUIDANCE
                            </th>
                        </tr>
                        <tr class="blankheader">
                            <th>
                            </th>
                        </tr>
                        <tr>
                            <th>
                                Document Title
                            </th>
                            <th style="padding-left: 0px;">
                                <center>
                                    Release Date</center>
                            </th>
                            <th style="padding-left: 0px;">
                                <center>
                                    Action</center>
                            </th>
                            <th>
                                Comment
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                Design Guidance &ndash; Date Display
                            </td>
                            <td>
                                <center>
                                    December 2007</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Date Display</i>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Medications Management &ndash; Drug Administration
                            </td>
                            <td>
                                <center>
                                    December 2007</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Medications Management &ndash;
                                    Drug Administration</i>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Medications Management &ndash; Medications Views
                            </td>
                            <td>
                                <center>
                                    December 2007</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Medications Management &ndash;
                                    Medications Views</i>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Patient Banner
                            </td>
                            <td>
                                <center>
                                    December 2007</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Patient Banner</i>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Advice &ndash; Abbreviations and Acronyms
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Advice &ndash; Abbreviations and Acronyms in Free Text Input
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Address Information Display
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Removed</center>
                            </td>
                            <td>
                                Replaced with <a href="DesignGuide/AddressDisplay.aspx" title="Links to the Address Input and Display Guidance page">
                                    <i>Design Guidance &ndash; Address Input and Display</i></a>
                            </td>
		                </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Address Input and Display
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                Replaces <i>Design Guidance &ndash; Address Information Display</i>, which is no
                                longer available on the Website
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Advice &ndash; Alert Symbol Design
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Decision Support
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Email Address Input and Display
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Icons and Symbology
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
			            <tr>
                            <td>
                                Design Guidance &ndash; Medications Line ID
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Patient Identification Number Display
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Removed</center>
                            </td>
                            <td>
                                Replaced with <a href="DesignGuide/PatientNoDisplay.aspx" title="Links to the Patient Identification Number Input and Display">
                                    <i>Design Guidance &ndash; Patient Identification Number Input and Display</i></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Patient Identification Number Input and Display
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                Replaces <i>Design Guidance &ndash; Patient Identification Number Display</i>, which
                                is no longer available on this Website
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Patient Name Input and Display
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr> 
                        <tr>
                            <td>
                                Design Guidance &ndash; Search and Prescribe
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Telephone Number Display
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Removed</center>
                            </td>
                            <td>
                                Replaced with <a href="DesignGuide/TelephoneNoDisplay.aspx" title="Links to the Telephone Number Input and Display Guidance page">
                                    <i>Design Guidance &ndash; Telephone Number Input and Display</i></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Telephone Number Input and Display
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                Replaces <i>Design Guidance &ndash; Telephone Number Display</i>, which is no longer
                                available on this Website
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Groundwork Exploration &ndash; Abbreviations and Acronyms
                            </td>
                                <td>
                                    <center>
                                        June 2008</center>
                                </td>
                                <td>
                                    <center>
                                        Updated</center>
                                </td>
                                <td>
                                    The guidance for Abbreviations and Acronyms has now been renamed to "<i>Design Guidance
                                        Exploration &ndash; Abbreviations and Acronyms</i>"
                                </td>
                         </tr>
                        <tr>
                                <td>
                                    Design Groundwork Exploration &ndash; Abbreviations and Acronyms in Free Text
                                </td>
                                <td>
                                    <center>
                                        June 2008</center>
                                </td>
                                <td>
                                    <center>
                                        Updated</center>
                                </td>
                                <td>
                                    The guidance for Abbreviations and Acronyms in Free Text has now been renamed to
                                    "<i>Design Guidance Exploration &ndash; Abbreviations and Acronyms in Free Text</i>"
                                </td>
                         </tr>
                            <tr>
                                <td>
                                    Design Groundwork Exploration &ndash; Alert Symbol Design
                                </td>
                                <td>
                                    <center>
                                        June 2008</center>
                                </td>
                                <td>
                                    <center>
                                        Updated</center>
                                </td>
                                <td>
                                    The guidance for Alert Symbol Design has now been renamed to "<i>Design Guidance Exploration
                                        &ndash; Alert Symbol Design</i>"
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Design Groundwork Exploration &ndash; Decision Support
                                </td>
                                <td>
                                    <center>
                                        June 2008</center>
                                </td>
                                <td>
                                    <center>
                                        Updated</center>
                                </td>
                                <td>
                                    The guidance for Decision Support has now been renamed to "<i>Design Guidance Exploration
                                        &ndash; Decision Support</i>"
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Design Groundwork Exploration &ndash; Icons and Symbology
                                </td>
                                <td>
                                    <center>
                                        June 2008</center>
                                </td>
                                <td>
                                    <center>
                                        Updated</center>
                                </td>
                                <td>
                                    The guidance for Icons and Symbology has now been renamed to "<i>Design Guidance Exploration
                                        &ndash; Icons and Symbology</i>"
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Design Groundwork Exploration &ndash; Search and Prescribe
                                </td>
                                <td>
                                    <center>
                                        June 2008</center>
                                </td>
                                <td>
                                    <center>
                                        Updated</center>
                                </td>
                                <td>
                                    The guidance for Search and Prescribe has now been renamed to "<i>Design Guidance Exploration
                                        &ndash; Search and Prescribe</i>"
                                </td>
			                </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Date and Time Input
                            </td>
                            <td>
                                <center>
                                    August 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Date and Time Input</i>
                            </td>
                        </tr> 
			                <tr>
				                <td>
                                    Design Guidance &ndash; Displaying Graphs and Tables
                                </td>
                                <td>
                                    <center>
                                        August 2008</center>
                                </td>
                                <td>
                                    <center>
                                        Added</center>
                                </td>
                                <td>
                                </td>
			                </tr>
			                <tr>
				                <td>
                                    Design Guidance &ndash; Micro Patient Banner
                                </td>
                                <td>
                                    <center>
                                        August 2008</center>
                                </td>
                                <td>
                                    <center>
                                        Added</center>
                                </td>
                                <td>
                                </td>
                            </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Time Display
                            </td>
                            <td>
                                <center>
                                    August 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Time Display</i>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Displaying Adverse Drug Reaction Risks
                            </td>
                            <td>
                                <center>
                                    February 2009</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Displaying Graphs and Tables
                            </td>
                            <td>
                                <center>
                                    February 2009</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Displaying Graphs and Tables</i>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Medication Line
                            </td>
                            <td>
                                <center>
                                    February 2009</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Medication Line</i> 
                            </td>
                        </tr>
			<tr>
                            <td>
                                Design Guidance &ndash; Medications List
                            </td>
                            <td>
                                <center>
                                    April 2009</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                Supersedes <i>Design Guidance &ndash; Medications Views</i>, 
				which will remain available until the next MSCUI release 
                            </td>
                        </tr>

			<tr>
                            <td>
                                Design Guidance &ndash; Filtering, Sorting and Grouping
                            </td>
                            <td>
                                <center>
                                    April 2009</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>                                
                            </td>
                        </tr>
			<tr>
                            <td>
                                Design Guidance &ndash; Recording Adverse Drug Reaction Risks
                            </td>
                            <td>
                                <center>
                                    April 2009</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Admissions Clerking
                            </td>
                            <td>
                                <center>
                                    September 2009</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Timeline View
                            </td>
                            <td>
                                <center>
                                    September 2009</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Guidance &ndash; Medications Views
                            </td>
                            <td>
                                <center>
                                    September 2009</center>
                            </td>
                            <td>
                                <center>
                                    Removed</center>
                            </td>
                            <td>
                                Superseded by <i>Design Guidance &ndash; Medications List</i>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Groundwork Exploration &ndash; Display of Clinical Statements
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Groundwork Exploration &ndash; Noting Using Templates
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Design Groundwork Exploration &ndash; Truncation of Clinical Terms
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Patient List View
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Address Input and Display
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Address Input and Display</i> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Date and Time Input
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Date and Time Input</i> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Date Display
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Date Display</i> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Drug Administration
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Drug Administration</i> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Micro Patient Banner
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Micro Patient Banner</i> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Patient Banner
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Patient Banner</i> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Patient Identification Number Input and Display
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Patient Identification Number Input and Display</i> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Patient Name Input and Display
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Patient Name Input and Display</i> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Search and Prescribe
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Groundwork Exploration &ndash; Search and Prescribe</i> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Sex and Current Gender Input and Display
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Sex and Current Gender Input and Display</i> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Telephone Number Input and Display
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Telephone Number Input and Display</i> 
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                Design Guidance &ndash; Time Display
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Replaces previous version of <i>Design Guidance &ndash; Time Display</i> 
                            </td>
                        </tr>
                                                                                                                                                                                                                            
                    </tbody>
                </table>
		<!-- End of table for updates of MSCUI Design Guidance-->
		</div>
            <div id="controls" class="relatedResources">
		<!-- Start of table for updates of MSCUI Toolkit Controls (ordered by date and then alphabetically, most recent at end of table)-->
		<table summary="This table lists changes to the associated Microsoft CUI Toolkit Controls.">
                    <col width="30%" />
                    <col width="15%" />
                    <col width="10%" />
                    <col width="45%" />
                    <thead>
                        <tr>
                            <th colspan="4" class="toptitle">
                                CONTROLS
                            </th>
                        </tr>
                        <tr class="blankheader">
                            <th>
                            </th>
                        </tr>
                        <tr>
                            <th>
                                Control Name
                            </th>
                            <th style="padding-left: 0px;">
                                <center>
                                    Release Date</center>
                            </th>
                            <th style="padding-left: 0px;">
                                <center>
                                    Action</center>
                            </th>
                            <th>
                                Comment
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                AddressLabel
                            </td>
                            <td>
                                <center>
                                    January 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This control replaces the previous version of the <i>AddressLabel</i> control
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ContactLabel
                            </td>
                            <td>
                                <center>
                                    January 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This control replaces the previous version of the <i>ContactLabel</i> control
                            </td>
                        </tr>
                        <tr>
                            <td>
                                DateInputBox
                            </td>
                            <td>
                                <center>
                                    January 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This control replaces the previous version of the <i>DateInputBox</i> control
                            </td>
                        </tr>
                        <tr>
                            <td>
                                MonthCalendar
                            </td>
                            <td>
                                <center>
                                    January 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PatientBanner
                            </td>
                            <td>
                                <center>
                                    January 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This control replaces the previous version of the <i>PatientBanner</i> control
                            </td>
                        </tr>
                        <tr>
                            <td>
                                TimeInputBox
                            </td>
                            <td>
                                <center>
                                    January 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This control replaces the previous version of the <i>TimeInputBox</i> control
                            </td>
                        </tr>
                        <tr>
                            <td>
                                TimeLabel
                            </td>
                            <td>
                                <center>
                                    January 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This control replaces the previous version of the <i>TimeLabel</i> control
                            </td>
                        </tr>
                        <tr>
                            <td>
                                MedicationGrid
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Removed</center>
                            </td>
                            <td>
                                Replaced with the <a href="Components/MedicationsListView.aspx" title="Links to the MedicationsListView Control page">
                                    <i>MedicationsListView</i></a> control. The AJAX <i>MedicationGrid</i> control
                                has been deprecated in this release because it no longer conforms with the latest
                                Design Guidance. Rather than update the AJAX control, it was decided that a brand
                                new version should be built using Silverlight. The code for the old AJAX control
                                is still available in the previous releases section of the <a href="https://www.codeplex.com/Release/ProjectReleases.aspx?ProjectName=mscui&ReleaseId=9696"
                                    target="_blank" title="Links to CodePlex (New Window)">CodePlex</a> Website.
                                ISVs are free to modify the AJAX control if they wish, however it does not conform to the
                                latest Design Guidance
                            </td>
                        </tr>
                        <tr>
                            <td>
                                MedicationLine
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Removed</center>
                            </td>
                            <td>
                                Replaced with the <a href="Components/MedicationsListView.aspx" title="Links to the MedicationsListView Control page">
                                    <i>MedicationsListView</i></a> control. The AJAX <i>MedicationLine</i> control
                                has been deprecated in this release because it no longer conforms with the latest
                                Design Guidance. Rather than update the AJAX control, it was decided that a brand
                                new version should be built using Silverlight. The code for the old AJAX control
                                is still available in the previous releases section of the <a href="https://www.codeplex.com/Release/ProjectReleases.aspx?ProjectName=mscui&ReleaseId=9696"
                                    target="_blank" title="Links to CodePlex (New Window)">CodePlex</a> Website.
                                ISVs are free to modify the AJAX control if they wish, however it does not conform to the
                                latest Design Guidance
                            </td>
                        </tr>
                        <tr>
                            <td>
                                MedicationNameLabel
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Removed</center>
                            </td>
                            <td>
                                Replaced with the <a href="Components/MedicationsListView.aspx" title="Links to the MedicationsListView Control page">
                                    <i>MedicationsListView</i></a> control. The AJAX <i>MedicationNameLabel</i>
                                control has been deprecated in this release because it no longer conforms with the
                                latest Design Guidance. Rather than update the AJAX control, it was decided that
                                a brand new version should be built using Silverlight. The code for the old AJAX
                                control is still available in the previous releases section of the <a href="https://www.codeplex.com/Release/ProjectReleases.aspx?ProjectName=mscui&ReleaseId=9696"
                                    target="_blank" title="Links to CodePlex (New Window)">CodePlex</a> Website.
                                ISVs are free to modify the AJAX control if they wish, however it does not conform to the
                                latest Design Guidance
                            </td>
                        </tr>
                        <tr>
                            <td>
                                MedicationsListView
                            </td>
                            <td>
                                <center>
                                    June 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                This control has been updated to work with Silverlight 2 Beta 2. It replaces the
                                <i>MedicationNameLabel</i>, <i>MedicationLine</i> and <i>MedicationGrid</i> controls
                            </td>
                        </tr>
			            <tr>
                            <td>
                                AddressLabel, ContactLabel, GenderLabel, IdentifierLabel, NameLabel, PatientBanner
                            </td>
                            <td>
                                <center>
                                    August 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Silverlight and Windows Presentation Foundation (WPF) versions of these controls 
				                have been added to ensure compatibility with new technologies. The AJAX and WinForms 
				                versions are still available
                            </td>
                        </tr>
			            <tr>
                            <td>
                                MedicationsListView
                            </td>
                            <td>
                                <center>
                                    August 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Silverlight and Windows Presentation (WPF) versions of this control have been reissued 
                                to be reusable and easily customizable, with enhanced usability, performance and 
                                rich styling capabilities
                            </td>
                        </tr>
			            <tr>
                            <td>
                                AddressLabel, ContactLabel, GenderLabel, IdentifierLabel, NameLabel, PatientBanner
                            </td>
                            <td>
                                <center>
                                    October 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                These controls have been updated to work with Silverlight 2 RTW. Both the Silverlight and WPF versions have been updated due to their common codebase. The AJAX and WinForms 
				                versions are still available
                            </td>
                        </tr>
			            <tr>
                            <td>
                                MedicationsListView
                            </td>
                            <td>
                                <center>
                                    October 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This control has been updated to work with Silverlight 2 RTW. Both the Silverlight and WPF versions have been updated due to their common codebase 
                            </td>
                        </tr>
			            <tr>
                            <td>
                                Graphing
                            </td>
                            <td>
                                <center>
                                    February 2009</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr> 
			            <tr>
                            <td>
                                MedicationsListView
                            </td>
                            <td>
                                <center>
                                    February 2009</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This control has been enhanced and now includes support for large numbers of rows
                            </td>
                        </tr>
		
			<tr>
                            <td>
                                SingleConceptMatching
                            </td>
                            <td>
                                <center>
                                    April 2009</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
			<tr>
                            <td>
                                PatientBanner
                            </td>
                            <td>
                                <center>
                                    April 2009</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This control has been updated with the removal of styling properties 
				to align with other controls and accepted best practices
                            </td>
                        </tr>
                        <tr>
                            <td>
                                DateLabel, TimeLabel
                            </td>
                            <td>
                                <center>
                                    September 2009</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                Silverlight and Windows Presentation Foundation (WPF) versions of these controls have been added to ensure compatibility with new technologies. 
                                The AJAX and WinForms versions are still available 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                AddressLabel, ContactLabel, GenderLabel, IdentifierLabel, NameLabel, PatientBanner
                            </td>
                            <td>
                                <center>
                                    September 2009</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                These controls have been updated to work with Silverlight 3. Both the Silverlight and WPF versions have been updated due to their common codebase. The AJAX and WinForms 
				                versions are still available
                            </td>
                        </tr>
			            <tr>
                            <td>
                                Graphing, MedicationsListView, SingleConceptMatching
                            </td>
                            <td>
                                <center>
                                    September 2009</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                These controls have been updated to work with Silverlight 3. Both the Silverlight and WPF versions have been updated due to their common codebase 
                            </td>
                        </tr>

                        <tr>
                            <td>
                                SearchAndPrescribe, Timeline
                            </td>
                            <td>
                                <center>
                                    February 2010</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                                                                   
                    </tbody>
                </table>
		<!-- End of table for updates of MSCUI Toolkit Controls-->
            </div>
            <div id="samples" class="relatedResources">
		<!-- Start of table for updates of MSCUI Samples (ordered by date and then alphabetically, most recent at end of table)-->
                <table summary="The table below lists the associated Microsoft CUI Samples.">
                    <col width="30%" />
                    <col width="15%" />
                    <col width="10%" />
                    <col width="45%" />
                    <thead>
                        <tr>
                            <th colspan="4" class="toptitle">
                                SAMPLES
                            </th>
                        </tr>
                        <tr class="blankheader">
                            <th>
                            </th>
                        </tr>
                        <tr>
                            <th>
                                Sample Name
                            </th>
                            <th style="padding-left: 0px;">
                                <center>
                                    Release Date</center>
                            </th>
                            <th style="padding-left: 0px;">
                                <center>
                                    Action</center>
                            </th>
                            <th>
                                Comment
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                Date and Time (all variants)
                            </td>
                            <td>
                                <center>
                                    January 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This sample replaces the previous version of the <i>Date and Time (all variants)</i>
                                sample
                            </td>
                        </tr>
			            <tr>
                            <td>
                                Date (CSS Styling)
                            </td>
                            <td>
                                <center>
                                    January 2008</center>
                            </td>
                            <td>
                                <center>
                                    Added</center>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Input Validation (all variants)
                            </td>
                            <td>
                                <center>
                                    January 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This sample replaces the previous version of the <i>Input Validation (all variants)</i>
                                sample
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PatientBanner (all variants)
                            </td>
                            <td>
                                <center>
                                    January 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This sample replaces the previous version of the <i>PatientBanner (all variants)</i>
                                sample
                            </td>
                        </tr>
 			            <tr>
                            <td>
                                MedicationGrid
                            </td>
                            <td>
                                <center>
                                    April 2008</center>
                            </td>
                            <td>
                                <center>
                                    Removed</center>
                            </td>
                            <td>
                                This sample has been deprecated in this release because it no longer conforms with
                                the latest Design Guidance
                            </td>
                        </tr> 
                        <tr>
                            <td>
                                PatientBanner (all variants)
                            </td>
                            <td>
                                <center>
                                    August 2008</center>
                            </td>
                            <td>
                                <center>
                                    Updated</center>
                            </td>
                            <td>
                                This sample replaces the previous version of the <i>PatientBanner (all variants)</i>
                                sample
                            </td>
                        </tr>                                              
                    </tbody>
                </table>
		<!-- End of table for updates of MSCUI Samples-->		
            </div>
        </p>
    </div>
    <div class="section">
        <h2>
            Accessibility
        </h2>
        <p>
            This Website, the Design Guidance and the Toolkit controls and samples have been
            designed with consideration to usability and accessibility for users who utilize
            alternative methods of interacting with computer systems.
        </p>
        <p>
            Testing was performed on all Winforms and ASP.NET with Version 8.0 of the JAWS&reg;
            for Windows screen reader. The tooltips for the Toolkit controls appear only on
            first focus and are not read by the screen reader.
        </p>
        <p>
            The Design Guidance documents are provided in PDF format and consist of sequential
            pages starting with a title page and a table of contents.
        </p>
        <p>
            The Web pages and controls based on Silverlight technology have more limited accessibility functionality.
        </p>
    </div>
    <div class="section">
        <a id="KnownIssues"></a>
        <h2>
            Known Issues
        </h2>
        <ul>
            <li>Browser Compatibility
                <ul>
                    <li>This Website has been designed to work optimally on Internet Explorer and Firefox browsers running on Windows.
                         If other browsers, such as Safari, are used, certain elements of this Website may not display or operate correctly, such as the demonstrators and controls.
                    </li>
                </ul>
            </li>
            <li>CSS Styling
                <ul>
                    <li>The ASP.NET versions of the controls have been designed to work with Cascading Style
                        Sheets so the controls can easily fit in with ISV applications. Many styles for
                        the controls are exposed, allowing developers to alter them without any constraints.
                        If the controls are styled outside the parameters set by the Design Guidance, they
                        can no longer be considered to conform to the Microsoft Health CUI Design Guidance.</li>
                </ul>
            </li>
            <li>DateInputBox Can Throw an Exception When Used on Multiple Threads
                <ul>
                    <li>If multiple threads are using DateInputBox, an 'An entry with the same key already
                        exists' exception is sometimes thrown. This appears to be a threading issue with
                        the ASP.NET AJAX Control Toolkit. </li>
                </ul>
            </li>
            <li>Namespaces
                <ul>
                    <li>We are currently changing our root namespace to <b><i>Microsoft.Cui</i></b>. Controls
                        under the previous namespace will be updated as they are revisited in accordance
                        with the roadmap.</li>
                </ul>
            </li>
            <li>Some Controls Cannot Be Created Dynamically Using Client-Side Code
                <ul>
                    <li>The DateInputBox, TimespanInputBox, MonthCalendar and TimeInputBox controls can
                        be instantiated through JavaScript on the client PC. Implementing this functionality
                        for other controls may be planned for a future release.</li>
                </ul>
            </li>
            <li>Telephone Number Format in PatientBanner Guidance and Control Does Not Match Format
                in Design Guidance for Telephone Number Input and Display
                <ul>
                    <li>The Design Guidance for Telephone Number Input and Display requires area codes to
                        be enclosed in brackets, but the Design Guidance for Patient Banner and the PatientBanner
                        control do not currently use this format. This will be resolved in a future release.</li>
                </ul>
            </li>            
            <li>ViewState Security Best Practice
                <ul>
                    <li>You should avoid storing sensitive data in ViewState. If you need to manage sensitive
                        data, maintain it on the server in, for example, a session state. If your ViewState
                        does contain sensitive data, you should perform encryption. For further information,
                        see <a href="http://msdn2.microsoft.com/en-us/library/ms998288.aspx" target="_blank"
                            title="Links to the Configure MachineKey in ASP.NET 2.0 page on Microsoft.com (New Window)">
                            How To: Configure MachineKey in ASP.NET 2.0</a>. </li>
                </ul>
            </li>
            <li>Web.config Configuration
                <ul>
                    <li>There are different versions of the AJAX Control Toolkit JavaScript libraries for
                        development and production. In development parameter validation is desirable to
                        ensure that errors are detected in code. Deployed applications normally omit this
                        checking; this is done by setting the flag &lt;compilation debug="false" /&gt; in
                        the web.config file for an application. Failure to do this may result in performance
                        problems in the client.</li>
                </ul>
            </li>
            <li>Web.config Security Best Practice
                <ul>
                    <li>You should set the customErrors mode tag of Web.config to "off" to prevent detailed
                        errors returning to the client. Microsoft recommends you follow the best practice
                        guidance given in the Exception Management section of <a href="http://msdn2.microsoft.com/en-us/library/ms998372.aspx"
                            target="_blank" title="Links to the ASP.NET 2.0 Security Practices at a Glance page on Microsoft.com (New Window)">
                            Security Practices: ASP.NET 2.0 Security Practices at a Glance</a>. </li>
                </ul>
            </li>
        </ul>
    </div>
</asp:Content>
