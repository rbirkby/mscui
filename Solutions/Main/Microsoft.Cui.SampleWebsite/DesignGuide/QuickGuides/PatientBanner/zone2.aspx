<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="zone2.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone2Page" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>Zone 2 contains information that either supports patient 
            identification or assists patient care. This includes contact 
            details and allergy propensities. 
            </p>
            <p>
            Zone 2 is displayed in a collapsed format by default. When 
            collapsed, it shows a single line of information for each 
            of the five sections.
            </p>
            <img src="images/zone2collapsed.png" alt="Diagram of a patient banner in which the lower section, which is high enough to display one line of text, is labelled &#39;Zome 2 collapsed&#39;" />
            <p>When expanded, Zone 2 displays more information for each 
            of the five sections.</p>
            <img src="images/zone2expanded.png" alt="Diagram of a patient banner in which the lower section extends down far enough to display several lines of text and is labelled &#39;Zone 2 Expanded &#39;" />
        </div>
    </div>
</asp:Content>