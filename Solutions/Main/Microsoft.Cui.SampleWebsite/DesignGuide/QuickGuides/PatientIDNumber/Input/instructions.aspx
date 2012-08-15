<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="instructions.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientIDNumber.Input.Instructions" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">           
            
            <p>
            The input control may provide a hint, prompt or tooltip. Hints 
            are instructional text placed outside but adjacent to the text 
            input box. Prompts are commonly known as watermarks and comprise 
            instructional text placed within the text input box. Tooltips are 
            instructional text that appear when the mouse pointer is placed 
            over the text input box.
            </p>
            
            <p>
            The wording of hints and prompts is left to the designers of 
            clinical applications. Examples of hints, prompts, and tooltips are 
            shown below.
            </p>
            
            <img src="../images/examplehint.png" alt="Instructional text &#39;e.g. 123 456 7890&#39; to the right of a Patient ID Number text input box" />
            <img src="../images/exampleprompt.png" alt="Instructional text &#39;e.g. 123 456 7890&#39; displayed within a Patient ID Number text input box" />
            <img src="../images/exampletooltip.png" alt="A Patient ID Number with a mouse pointer (an arrow) and instructional text &#39;e.g. 123 456 7890&#39; displayed within a tooltip" />
            
        </div>
    </div>
</asp:Content>
