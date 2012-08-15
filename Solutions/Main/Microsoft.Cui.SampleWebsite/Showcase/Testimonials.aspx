<%@ Page Language="C#" MasterPageFile="~/Navigational.master" AutoEventWireup="true"
    Inherits="ShowcaseTestimonials" EnableViewState="false" Title="Untitled Page"
    CodeBehind="Testimonials.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="navigationPageContent" runat="Server">
    <div class="first section" style="font-family: Verdana">
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            Testimonials provide working examples of how the Microsoft Health Common User Interface
            Design Guidance has enhanced patient safety and application usability within the
            healthcare industry.
            <br />
            <br />
            Testimonials are given from clinical application and healthcare providers who have
            adopted the Design Guidance. These are featured using high-definition, on-demand
            video streamed interviews, provided by <a target="_blank" href="http://www.microsoft.com/silverlight/default.aspx"
                title="Link to Silverlight page on Microsoft.com (New Window)">Microsoft&reg;&nbsp;Silverlight&trade;</a>.
        </p>
    </div>
    <br />
    <div>
        <object id="TestimonialsPlugin" data="data:application/x-silverlight," type="application/x-silverlight-2"
            width="100%" height="1250px">
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
            height: 125px;
        }
    </style>
</asp:Content>
