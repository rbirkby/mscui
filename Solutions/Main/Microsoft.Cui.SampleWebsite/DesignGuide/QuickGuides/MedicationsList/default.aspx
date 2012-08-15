<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="default.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Overview" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            This guide contains all the Medications List 
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
            A list is one way of displaying medications information for a single patient. 
            That medications list would be only one of many potential views and tools for 
            managing medications in a clinical application.
            </p>
            <p>
            The guidance focuses on a subset of the user interface needed for managing medications. 
            A full framework might include: 
            </p>
            <ul>
                <li>More than one medication view with differing presentation styles</li>
                <li>Specialist views for specific contexts</li>
                <li>
                Medications information integrated into views that are not just for 
                managing medications
                </li>
                <li>Alternative presentation styles for a list of medications</li>
            </ul>
            <p>
            This version of the Medications List guide is based on the full guidance document<br /> 
            <a href="../../Pdfs/Design%20Guidance%20--%20Medications%20List.pdf" target="_blank" title="Links to Design Guidance - Medications List documentation">
                Design Guidance - Medications List (v1.0.0.0) - PDF
            </a>
            </p>
        </div>
    </div>
</asp:Content>