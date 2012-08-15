<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="default.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Overview" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
<div id="page">
        <div id="overview">
            <p>
            This guide contains all the Date and Time Display 
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
            This guidance specifies a user interface for the display of short format dates, 
            long format dates, times and duration. Guidance includes rules for syntax, 
            separators, null values and approximate values.
            </p>
            <h3>Date Formats</h3>
            <img src="images/dates.png" alt="Example date formats, including &#39;01-Jan-1994&#39; and &#39;01 January 1994&#39;" />
            <h3>Time Formats</h3>          
            <img src="images/times.png" alt="Example time formats, including &#39;23:59&#39; and &#39;09:43:22&#39;" />
            <h3>Duration Formats</h3>
            <img src="images/duration.png" alt="Example duration formats, including &#39;4hr 32min 16sec&#39; and &#39;8hr 10se&#39;" />
            <p>
            This version of the Date and Time Display guide is based on the following full 
            guidance documents:
            <ul>
                <li>
                <a href="../../Pdfs/Design%20Guidance%20--%20Date%20Display.pdf" target="_blank" title="Links to Design Guidance - Date Display documentation">
                    Design Guidance - Date Display (v3.0.0.0) - PDF </a>
                </li>
                <li>
                <a href="../../Pdfs/Design%20Guidance%20--%20Time%20Display.pdf" target="_blank" title="Links to Design Guidance - Time Display documentation">
                    Design Guidance - Time Display (v3.0.0.0) - PDF </a>
                </li>
            </ul> 
            </p>
        </div>
    </div>
</asp:Content>
