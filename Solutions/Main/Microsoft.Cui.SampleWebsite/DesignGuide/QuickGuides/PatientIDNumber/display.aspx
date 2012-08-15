<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="display.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientIDNumber.DisplayPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">
            <p>
            Patient identification numbers are used to identify patients and must 
            therefore be displayed in full.
            </p>
            <p>
            The following diagram illustrates a Patient identification Number 
            (in this case an NHS Number) and summarises guidance for the display 
            of Patient ID Numbers:
            </p>
            <img src="images/num-display.png" alt="Diagram showing an NHS Number with callouts indicating key guidance points" />
        </div>
    </div>
</asp:Content>
