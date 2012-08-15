<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="default.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.GenderSex.Overview" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
     <div id="page">
        <div id="overview">
            <p>
            This guide contains all the Sex and Current Gender 
            design guidelines (mandatory and recommended) 
            accompanied by example diagrams to illustrate their application. Although offered 
            as a quick reference to already familiar guidance, you may also find this guide 
            useful for exploring areas of the guidance that are new to you.
            </p>
            <p>
            Remember that this guide is not a replacement for the definitive version which 
            includes further usage examples (showing both correct and incorrect application) 
            and details the rationale behind the guidance.
            </p>
            <h2>Purpose of this Guidance</h2>
            <p>
            This guidance specifies a user interface for the display and entry of sex and 
            current gender. The 
            <a href="http://www.cabinetoffice.gov.uk/govtalk/schemasstandards/e-gif/datastandards/person_information/person_sex/person_gender_current.aspx" title="Links to the UK Government Data Standards Catalogue (New Window)" target="_blank">
            UK Government Data Standards Catalogue</a> (GDSC)
            categorises a person's gender in two ways:
            </p>
            <ul>
            	<li>
                    <strong>Person Gender Current</strong>, which refers to a person&#39;s 
                    current gender classification.<br />
                    <span class="subtitle">For brevity and clarity, this document uses the term 
                    <strong>Current Gender</strong> for this concept</span>
                </li>
                <li>
                    <strong>Person Gender at Registration</strong>, which refers to the record of a person&#39;s gender 
                    classification at the point of birth registration.<br />
                    <span class="subtitle">For brevity and clarity, this document uses the term 
                    <strong>Sex</strong> for this concept</span>
                </li>
            </ul>
            <p>
            Healthcare professionals and patients may confuse the terms &#39;Current Gender&#39; and &#39;Sex&#39;, 
            or assume that they are synonymous. Therefore, all clinical applications should display and 
            explain Current Gender and Sex terminology and values in a clear and consistent manner, 
            both on input of the values and subsequent display.
            </p>
            <p>
            Allowable values for Current Gender 
            and Sex need to be from a predefined, restricted set in order to ensure legal compliance, 
            patient-sensitivity and data interoperability.
            </p>
            <p>
            This version of the Sex and Current Gender Input and Display guide is based on the full 
            guidance document 
            <a href="../../Pdfs/Design%20Guidance%20--%20Sex%20and%20Current%20Gender%20Input%20and%20Display.pdf" target="_blank" title="Links to Design Guidance - Sex and Current Gender Input and Display documentation">
                Design Guidance - Sex and Current Gender Input and Display (v2.0.0.0) - PDF
            </a>
            </p>
        </div>
    </div>
</asp:Content>
