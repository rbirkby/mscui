<%@ Page Language="C#" MasterPageFile="~/Navigational.master" AutoEventWireup="true"
    CodeBehind="ControlsAndSamples.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.ControlsAndSamples"
    Title="Untitled Page" %>

<asp:Content ID="controlsAndSamplesLandingContent" ContentPlaceHolderID="navigationPageContent"
    runat="server">
    <div class="first section">
        <div class="downloadBoxBig codeplex">
            <h1>
                Download</h1>
            <a target="_blank" href="http://www.codeplex.com/mscui" title="Link to download the Controls Library from CodePlex (New Window)">
                <img src="images/SFTheme/cplex.png" alt="" title="Link to download the Controls Library from CodePlex" />
                <span>Download the Controls Library from CodePlex</span> </a>
        </div>
        <h1>
            Controls and Samples Overview</h1>
        <p>
            The development of Toolkit controls is a fundamental part of Microsoft Health
            CUI. The Toolkit controls developed for this release conform to the recommendations
            contained in the Design Guidance documents. You can ensure your own clinical applications
            conform to the Design Guidance by using these Toolkit controls and samples.
        </p>
        <br />
        <p>
            The controls are free to use under the Microsoft Public license (Ms-PL) the
            terms of which can be found here: <a target="_blank" href="http://mscui.codeplex.com/license"
                title="Link to MSCUI license page on codeplex.com (New Window)">
                http://mscui.codeplex.com/license</a>
        </p>
    </div>
    <div class="update section">
        <label>Note:</label>
        <p>
            The Controls and Samples are updated regularly. See the <a href="About.aspx" title="Link to the Change Record &ndash; Guidance and Controls">
                Change Record &ndash; Guidance and Controls</a> and the <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx"
                    target="_blank" title="Link to releases page on the CodePlex site (New Window)">
                    Releases page on the CodePlex site</a> for a list of the controls and samples
            updated in the latest release.</p>
    </div>
    <div class="section">
        <h2>
            Controls</h2>
        <p>
            The Controls section contains examples of the individual Toolkit controls. The controls
            are demonstrated and their properties described. Relevant additional information
            and code snippets are also included.
        </p>
        <p>
            <strong>Note:</strong> As detailed in the <a href="Roadmap/architecture.aspx" title="Link to Architecture page in the Roadmap section">
                Architecture</a> section, controls created in releases prior to June 2008 were created
            using ASP.NET AJAX and .NET WinForms. Controls created since then
            have used <a target="_blank" href="http://windowsclient.net/WPF/"
                title="Link to Windows Presentation Foundation page on WindowsClient.NET (New Window)">
                Windows&reg; Presentation Foundation (WPF)</a> and <a target="_blank" href="http://www.microsoft.com/silverlight/default.aspx"
                title="Link to Silverlight page on Microsoft.com (New Window)">Microsoft&reg; Silverlight&trade;</a>. While this
            Website demonstrates an interactive version of the Silverlight&trade; and WebForms controls, the WPF and WinForms controls are shown as images only. You will need to install
            the Toolkit from <a target="_blank" href="http://www.codeplex.com/mscui" title="Link to download the Controls Library from CodePlex (New Window)">
                CodePlex</a> to access the interactive WPF and WinForms controls.
        </p>
    </div>
    <div class="last section">
        <h2>
            Samples</h2>
        <p>
            The Samples section provides examples of the controls in specific scenarios and
            demonstrates how different controls can be combined and different styling applied.
        </p>
    </div>
</asp:Content>
