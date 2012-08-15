<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="templateprescriptions.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.TemplatePrescriptionsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            The definition of the minimum set of required attributes can be made easier by 
            presenting options that are only relevant to the selected drug type. Template 
            prescriptions (order sentences) are predefined and partially completed prescriptions 
            that allow several attributes to be defined with a single selection from a list.
            </p>
            <img src="images/templates.png" alt="A diagram of a selected drug name (zolmitriptan), a selection from a cascading list (oral) and a list of template prescriptions (for oral zolmitriptan)" />
        </div>
    </div>     
</asp:Content>
