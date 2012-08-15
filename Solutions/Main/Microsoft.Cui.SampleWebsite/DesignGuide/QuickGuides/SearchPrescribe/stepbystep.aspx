<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="stepbystep.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.StepByStepPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            Prescribing begins with the selection of a drug to prescribe and continues with 
            further selections (from cascading lists and template prescriptions) to define 
            the required attributes of the prescription. Once sufficient information has 
            been defined to determine the type of medication being prescribed (and thus 
            determine which other attributes will be needed), a more detailed prescription 
            form can be displayed.
            </p>
            <p>
            In most cases, a template prescription can be selected and the set of required 
            fields can then be displayed. However, in the absence of template prescriptions, 
            a step-by-step process can be used to encourage the selection of important 
            attributes (such as dose) from predefined, limited lists.
            </p>
        </div>
    </div>      
</asp:Content>
