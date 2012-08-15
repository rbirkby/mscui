<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="AccessibilityPrinciples.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.AccessibilityPrinciples"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>Download</h1>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Accessibility Principles.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Accessibility Principles documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Accessibility Principles (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
            Introduction</h2>
        <p>
            The <i>Design Guidance &ndash; Accessibility Principles</i> document provides you
            with a framework for achieving accessibility within clinical applications. It enhances
            patient safety and clinical application usability by providing accessibility requirements
            for specific interface components that are system independent.
        </p>
        </div><div class="last section">
        <h2>
            Summary</h2>
        <p>
            The guidance focuses on accessibility principles and includes recommendations for:</p>
        <ul>
            <li>Supporting standard system size, color, font, input settings, and accessibility
                options</li>
            <li>Enabling programmatic access to user interface elements and text</li>
            <li>Providing keyboard access to all features</li>
            <li>Exposing the location of the keyboard focus</li>
            <li>Providing equivalents for non-text elements</li>
            <li>Conveying information that does not rely on a single representation that utilizes
                a single sense (or perceptual capability)</li>
            <li>Avoiding flashing elements</li>
            <li>Enabling user control of timed information presentation and responses</li>
            <li>Ensuring consistency between interface elements and display items</li>
            <li>Creating accessible documentation about accessibility features</li>
        </ul>
    </div>
</asp:Content>
