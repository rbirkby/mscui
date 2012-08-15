<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="display.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.DisplayPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
     <div id="page">
        <div id="overview">
            <p>
            This section provides guidance for the display of a Patient Name 
            with enough information to distinguish it for identification purposes.
            </p>
            <img src="images/nid-display.png" alt="An example patient name: &#39;EVANS-WEST, Mary Jane (Mrs)&#39; with callouts indicating key guidance points" />           
            <p class="note"><strong>Note</strong><br />
            This document refers to Patient Name elements using the descriptors 
            &#39;Family Name&#39; and &#39;Given Name&#39;. This includes the labels used within 
            the visual representations. However, use of these terms is not 
            Mandatory but only Recommended 
            (see <a href="Input/Instructions/fieldlabels.aspx">Instructional Text</a>). 
            An example of alternative terms would be &#39;Last Name&#39; and &#39;First Name&#39;.
            </p>
        </div>
    </div>
</asp:Content>