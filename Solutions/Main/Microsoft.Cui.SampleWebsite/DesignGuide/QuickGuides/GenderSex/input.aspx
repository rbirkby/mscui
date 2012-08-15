<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="input.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.GenderSex.InputPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            The guidance supports two control styles for input of Current Gender and Sex: 
            Option Button Groups (also known as Radio Button Groups) and Drop-Down List Boxes.
            </p>
            <p>
            The Option Button group design occupies more screen space but provides 
            better instruction to the user. This control style should be used if 
            there is room on the input form.
            </p>
                <img src="images/cds-input.png" alt="Current Gender and Sex input controls.  Each shown as both a group of radio buttons and a drop-down list." />
            
        </div>
    </div>
</asp:Content>