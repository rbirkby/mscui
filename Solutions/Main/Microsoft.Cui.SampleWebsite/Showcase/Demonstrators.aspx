<%@ Page Language="C#" MasterPageFile="~/Navigational.master" AutoEventWireup="true"
    Inherits="ShowcaseDemonstrators" EnableViewState="false" Title="Untitled Page"
    CodeBehind="Demonstrators.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="navigationPageContent" runat="Server">
    <div class="first section" style="font-family: Verdana; height: 180px;">
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The Demonstrators show how the Microsoft Health CUI Design Guidance can be used
            in clinical applications.
            <br />
            <br />
            A number of both practical and innovative scenarios are provided, where the implementation
            of the Design Guidance has allowed the healthcare professional to easily access
            the patient information that they needed, when and where it was required.
            <br />
            <br />
            The following Demonstrators were produced from the Design Guidance that was, in
            turn, developed for the
            <br />
            NHS CUI Programme in collaboration with healthcare professionals. It uses a variety
            of technologies, including
            <br />
            <a href="http://www.microsoft.com/silverlight/default.aspx" target="_blank" title="Link to Silverlight page on Microsoft.com (New Window)">
                Microsoft&reg; Silverlight&trade;</a>, <a target="_blank" href="http://windowsclient.net/WPF/"
                title="Link to Windows Presentation Foundation page on WindowsClient.NET (New Window)">
                Windows&reg; Presentation Foundation (WPF)</a> and ASP.NET.
        </p>
    </div>
    <br />
    <div>
        <object id="TestimonialsPlugin" data="data:application/x-silverlight," type="application/x-silverlight-2"
            width="100%" height="1700px">
            <param name="source" value="../ClientBin/Microsoft.Cui.ShowcaseControls.xap" />
            <param name="initParams" value="<%=InitParameters%>" />
            <param name="minRuntimeVersion" value="3.0.40818.0" />
            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none;">
                <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                    style="border-style: none" />
            </a>
        </object>
        <iframe style='visibility: hidden; height: 0; width: 0; border: 0px'></iframe>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="navigationSpecificHeadTags">
    <style type="text/css">
        .section
        {
            height: 109px;
        }
    </style>
</asp:Content>
