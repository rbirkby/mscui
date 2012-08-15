<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="sentencelayout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.SentenceLayoutPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            Sentence layout is the display of input fields as if they were words in a sentence. 
            Display rules (such as those for width and wrapping) that might apply to words in 
            a sentence are applied to the dynamic display of input fields. This means that input 
            fields can grow in width as values are entered into them and wrap onto a new line as 
            necessary:
            </p>
            <img src="images/sentencelayout.png" alt="Diagram of a series of fields displayed one after the other like words in a sentence and wrapping onto new lines accordingly" />
        </div>
    </div>       
</asp:Content>
