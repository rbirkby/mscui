<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Microsoft Health CUI - Error </title>
    <link rel="stylesheet" title="Main stylesheet" href="CSS/SFTheme.css" type="text/css" />
    <style type="text/css">
        #siteSectionNav
        {
            top: -30px;
            font-size: 12pt;
            float: left;
        }
    </style>
</head>
<body>
    <form name="aspnetForm" action="error.aspx" id="aspnetForm">
    <div id="siteHeader">
        <asp:ImageMap ID="mscuiImageMap" runat="server" ImageUrl="~/images/SFTheme/cuibann.png"
            HotSpotMode="Navigate">
            <asp:RectangleHotSpot Top="20" Left="20" Right="216" Bottom="148" NavigateUrl="~/Default.aspx"
                AlternateText="Microsoft Health Common User Interface" />
        </asp:ImageMap>
        <table id="siteSectionNav">
            <tr>
                <td class="first">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" title='Links to Home page'>Home</asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Introduction/Introduction.aspx"
                        title='Links to Introduction section'>Introduction</asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/DesignGuide/DesignGuide.aspx"
                        title='Links to Guidance section'>Guidance</asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/ControlsAndSamples.aspx"
                        title='Links to Controls and Samples section'>Controls</asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Showcase/Showcase.aspx"
                        title='Links to Showcase section'>Showcase</asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/roadmap/roadmap.aspx"
                        title='Links to Roadmap section'>Roadmap</asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="blogLink" runat="server" NavigateUrl="~/Blog/Default.aspx" title='Links to Team Blog section'>Team Blog</asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
    <div id="siteContent">
        <div class="first section" style="height: 500px">
            <h1>
                An error has occurred
            </h1>
            We apologise but an error
            <asp:Label runat="server" ID="lblErrorInfo"></asp:Label>
            has occurred.<br />
            Please try your previous action again and if this error re-occurs <a href="http://www.codeplex.com/mscui"
                title="Opens the CodePlex website in this window">report</a> it on our CodePlex
            site.
        </div>
    </div>
    <div id="siteFooter">
        <span>&copy; 2008 Microsoft Corporation. All rights reserved.</span>
        <div id="links">
            <table>
                <tr>
                    <td class="first">
                        <asp:HyperLink runat="server" NavigateUrl="~/about.aspx" title='Links to About this Website'>About</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink runat="server" NavigateUrl="~/sitemap.aspx" title='Links to Sitemap'>Sitemap</asp:HyperLink>
                    </td>
                    <td>
                        <a href="http://www.microsoft.com/info/cpyright.mspx" title='Links to Terms of Use page on Microsoft.com'>
                            Terms of Use</a>
                    </td>
                    <td>
                        <a href="http://www.microsoft.com/library/toolbar/3.0/trademarks/en-us.mspx" title='Links to Trademarks page on Microsoft.com'>
                            Trademarks</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
