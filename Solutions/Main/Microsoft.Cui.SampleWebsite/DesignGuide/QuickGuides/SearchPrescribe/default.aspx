<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="default.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.Overview" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            This guide contains all the Medications Search and Prescribe
            design guidelines (mandatory and recommended) 
            accompanied by example diagrams to illustrate their application. Although offered 
            as a quick reference to already familiar guidance, you may also find this guide 
            useful for exploring areas of the guidance that are new to you.
            </p>
            <p class="note">
            <strong>Note</strong>
            The visual representations in this guide are illustrative only. They are simplified 
            in order to facilitate understanding of the guidance points. Stylistic choices, 
            such as colours, fonts or icons are not part of the guidance and unless otherwise 
            specified are not mandatory requirements for compliance with the guidance.
            </p>
            <p>
            Remember that this guide is not a replacement for the definitive version which 
            includes further usage examples (showing both correct and incorrect application) 
            and details the rationale behind the guidance.
            </p>
            <h2>Purpose of this Guidance</h2>
            <p>
            The guidance advocates a dose-based precribing system in which a prescriber specifies 
            a drug by its generic name plus dose, route and frequency.  A nurse (for example) then 
            selects the correct quantity of actual product to give the patient.
            </p>
            <p>
            The guidance assumes a flexible prescribing process that can support both a quick 
            prescribing process for the most commonly prescribed drugs and a more detailed 
            prescribing process for less common prescribing practices, whilst mitigating known 
            risks and meeting a high standard of patient safety.
            </p>
            <p class="note">
            <strong>Note</strong> 
            The usage examples in this document include examples of sets of fields, some of 
            which are shown as required and some as optional. These examples are illustrative only 
            and are not intended to provide guidance on which fields should be available for 
            specific types of medication nor which fields should be required or optional.
            </p>
            <p>
            This version of the Search and Prescribe guide is based on the full guidance document 
            <a href="../../Pdfs/Design%20Guidance%20--%20Search%20and%20Prescribe.pdf" target="_blank" title="Links to Design Guidance - Search and Prescribe documentation">
                Design Guidance - Medications Management - Search and Prescribe (v3.0.0.0) - PDF
            </a>
            </p>
        </div>
    </div>
</asp:Content>
