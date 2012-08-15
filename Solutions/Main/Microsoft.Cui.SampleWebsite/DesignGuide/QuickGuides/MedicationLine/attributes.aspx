<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="attributes.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.AttributesPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            The following diagram illustrates key guidance points that are specific to drug name, dose and strength:    
            </p>
            <img src="images/medline.png" alt="Diagram illustrating key guidance points that are specific to drug name, dose and strength" />
            <p>
            Even though different attributes will be displayed for different types of drug, the overall order of attributes remains the same:
            </p>
            <img src="images/attributeorder.png" class="captioned" alt="Diagram illustrating the order of attributes in a medication line" />
            <p class="caption">Order in which to display the attributes in a medication line</p>
        </div>
    </div>
</asp:Content>