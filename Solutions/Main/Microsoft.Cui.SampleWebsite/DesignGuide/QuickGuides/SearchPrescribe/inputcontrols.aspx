<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="inputcontrols.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControlsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            The contents of a prescription form depend on the type of medication being 
            prescribed and this determines which attributes are required and thus which 
            input controls will be displayed. 
            </p>
            <p>
            There are four aspects of the prescription 
            form that can be considered to be dynamic:
            </p>
            <ul>
                <li>
                The display of a set of input controls. 
                The controls displayed will depend on the type of medication being prescribed
                </li>
                <li>
                Input controls that may appear when a value is defined in another input control
                </li>
                <li>
                Input controls that may be pre-filled depending on selections elsewhere in the form
                </li>
                <li>
                Input controls whose dimensions may change as the form or field is completed
                </li>
            </ul>
        </div>
    </div>       
</asp:Content>
