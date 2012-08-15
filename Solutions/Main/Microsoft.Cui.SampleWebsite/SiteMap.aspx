<%@ Page Language="C#" MasterPageFile="~/DefaultMaster.master" EnableViewState="false" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.ToolkitSiteMap" Title="Sitemap" %>
<%@ Register Assembly="Microsoft.Cui.SampleWebsite" Namespace="Microsoft.Cui.SampleWebsite" TagPrefix="SS" %>   
<asp:Content ID="Content1" ContentPlaceHolderID="siteContentPlaceHolder" runat="server">
    <asp:SiteMapDataSource runat="server" ID="completeSiteMapDS" ShowStartingNode="true">
    </asp:SiteMapDataSource>
    <div id="sitemap">
        <SS:SiteMapListControl runat="server" id="siteMapListControl" depth="4"></SS:SiteMapListControl>
    </div>
</asp:Content>
