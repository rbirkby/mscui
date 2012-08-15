<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="input.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.InputPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">
            <p>
            A Patient Name input control can consist of up to six constituent fields 
            with labels. <br />The minimum data required to make the name useful is considered 
            to be: Title, Family Name and Given Name. Middle Name(s), Preferred Name and Suffix 
            are considered to improve data quality, however they are not mandatory.
            </p>
            <img src="images/nid-input.png" alt="A diagram of a text input form for family name with callouts text describing key guidance points for each element of a patient name." />
        </div>
    </div>
</asp:Content>