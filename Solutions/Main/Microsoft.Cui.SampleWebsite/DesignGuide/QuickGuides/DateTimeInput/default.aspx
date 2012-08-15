<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="default.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Overview" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
   <div id="page">
        <div id="overview">
            <p>
            This guide contains all the Date and Time Input 
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
            The entry of date and time information is a fundamental function within 
            clinical and administrative processes. Therefore, healthcare professionals 
            should have a clear mechanism for entry of an accurate date or time value, 
            and be assisted to disambiguate entries when necessary.
            </p>
            <p>
            The basic date input control should comprise:
            </p>
            <ul>
                <li>A free text input area</li>
                <li>A calendar control</li>
                <li>A default input, dependent on the context in which the control is used</li>
                <li>A facility to disambiguate the date entered</li>
            </ul>
            <img src="images/dateinput.png" class="captioned" alt="Diagram of a date input control with callouts: a default input, a free text input area, a calendar control" />
            <p class="caption">Elements of a basic date input control</p>
            <p>
            This version of the Date and Time Display guide is based on the following full 
            guidance document 
            <a href="../../Pdfs/Design%20Guidance%20--%20Date%20and%20Time%20Input.pdf" target="_blank" title="Links to Design Guidance - Date and Time Input documentation">
            Design Guidance - Date and Time Input (v3.0.0.0) - PDF </a>
            </ul> 
            </p>
        </div>
    </div>
</asp:Content>
