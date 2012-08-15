<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="structure.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.StructurePage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            This section introduces the two zones of the patient banner, describes the 
            contents of each zone and how they should be presented.  It also provides 
            generic guidance for visual styling (including the display of data and labels) 
            that applies to all parts of the patient banner.
            </p>
            <div class="landscape">
                <img src="images/zones.png" alt="Patient Banner with Zones demarcated and labelled. Zone 1 is at the top and Zone 2 below" />
            </div>
            <p>See 
            <a href="zone1.aspx" title="Links to the introduction page for Zone 1">
            Zone 1
            </a> and 
            <a href="zone2.aspx" title="Links to the introduction page for Zone 2">
            Zone 2
            </a> for 
            detailed guidance specific to each zone.</p>
        </div>
    </div>
</asp:Content>