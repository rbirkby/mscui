<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="control.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.Control" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>This guidance supports two patient name input control layouts:</p>
            <img src="../images/nidinform.png" class="captioned" alt="An input form with the following input controls each displayed on a new line: Title, FAMILY name, Given name, Middle name(s), Suffix, Known as." />
            <p class="caption">In-Form Layout</p>
            <p>
            The in-form layout is considered the most desirable layout from a patient 
            safety and usability perspective and should therefore be the default choice 
            for an application&#39;s Patient Name input control
            </p>
            <img src="../images/nidinline.png" class="captioned" alt="An input form with the following input controls laid out horizontally with labels above each control: Title, FAMILY name, Given name, Middle name(s), Suffix, Known as." />
            <p class="caption">In-Line Layout</p>
            <p>
            The in-line layout is included for instances where vertical space on the 
            input form is constrained, or where a horizontal layout style is a 
            convention in the application's other input forms
            </p>
        </div>
    </div>
</asp:Content>
