<%@ Page Language="C#" MasterPageFile="~/Navigational.master" AutoEventWireup="true"
    Inherits="Showcase" EnableViewState="false" Title="Untitled Page" CodeBehind="Showcase.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="navigationPageContent" runat="Server">
    <div class="first section" style="font-family: Verdana">
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <img align="right" src="images/showcaseimage.png" alt="Showcase image" title="Showcase image"
            style="padding: 0px 10px 0px 0px" />
        <p>
            The Showcase demonstrates how the adoption of the Microsoft Health Common User Interface
            Design Guidance can benefit both clinical applications providers and healthcare
            providers, by making patient care better, faster and safer.
        </p>
        <p>
            The Showcase also delivers an insight into the approach and technologies that will
            be deployed in future releases of the Design Guidance.
        </p>
        <p>
            This is provided in various formats including on-demand, video-streamed interviews in 
            high definition and demonstrations using Microsoft Silverlight.
        </p>
        <p>
            <a href="http://www.microsoft.com/silverlight/default.aspx" target="_blank" title="Link to Silverlight page on Microsoft.com (New Window)">
                Microsoft&reg; Silverlight&trade;</a> is a cross-browser, cross-platform, and cross-device
            plug-in for delivering the next generation of .NET based media experiences and rich
            interactive applications for the Web.
        </p>
    </div>
    <div class="first section" style="font-family: Verdana">
        <h2>
            Testimonials</h2>
        <p>
            Testimonials are given from clinical application and healthcare providers who have
            adopted the Design Guidance; this provides working examples of how the Design Guidance
            has enhanced patient safety and application usability within the healthcare industry.
        </p>
    </div>
    <div class="first section" style="font-family: Verdana">
        <h2>
            Demonstrators</h2>
        <p>
            The Demonstrators provide a number of both practical and innovative scenarios where
            the implementation of the Design Guidance has allowed the healthcare professional
            to easily access the patient information that they needed, when and where it was
            required.
        </p>
    </div>
    <div class="last section">
        <h2>
            Solution Accelerators</h2>
        <p>
            A significant addition to MSCUI.net, Solution Accelerators integrate some of this site's CUI-compliant controls 
            into an information-handling toolkit based on the Microsoft Office System. This innovative use of 
            existing technologies broadens the Microsoft Health Common User Interface offering to encapsulate quick-to-market 
            solutions for many clinical information management issues within healthcare today.
        </p>
    </div>
</asp:Content>
