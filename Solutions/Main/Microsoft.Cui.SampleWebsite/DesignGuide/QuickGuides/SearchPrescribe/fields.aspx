<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="fields.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.FieldsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            When a detailed prescription form is presented, the required fields are 
            displayed by default. Also displayed are controls for accessing optional 
            fields and, when those controls are selected, the optional fields appear 
            alongside the required fields. This approach is based on the assumption 
            that the majority of prescriptions will be completed 
            using template prescriptions and that most of the time only a few optional 
            fields may be needed. However, in some cases, additional specific fields 
            or more detailed prescriptions may be needed and forms with a larger number 
            of fields would be required to support these.
            </p>
            <img src="images/popupviabutton.png" alt="A two step diagram showing that a pop-up is accessed by clicking on a button within the prescribing form" />
            <img src="images/detailsviatab.png" alt="A two step diagram showing the selection of a tab to access additional input controls" />
            <img src="images/detailsviabutton.png" alt="A two step diagram showing a window of input controls that is accessed by clicking on a button in the prescribing form" />
        </div>
    </div>       
</asp:Content>
