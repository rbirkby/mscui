<%@ Page Language="C#" MasterPageFile="~/Navigational.master" AutoEventWireup="true"
    Codebehind="Architecture.aspx.cs" EnableViewState="false" Inherits="RoadmapArchitecture" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="navigationPageContent" runat="server">
    <div class="first section">
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <p>
            The Microsoft Health CUI Toolkit is a set of .NET controls that help Independent Software Vendors (ISVs) build safe, consistent user interfaces for healthcare applications. Full source code can be downloaded from our <a href="http://www.codeplex.com/mscui" title="Link to CodePlex site (New Window)" target="_blank">CodePlex</a> site, and includes sample applications that illustrate how the controls can be used in a variety of contexts. Unit tests that can be used to verify that the controls are working correctly are also included. The controls are designed to be used in conjunction with the Design Guidance documentation, but ISVs are free to extend the controls to suit their own requirements using the source code provided as a starting point. Once installed into the Visual Studio Toolbox, full IntelliSense and context-sensitive help are available to help developers understand how to use the various control features. 
        </p>
        <br />
        <h2>Technology Roadmap</h2>
	<p>
            In the releases prior to June 2008, Toolkit controls were created using ASP.NET AJAX and .NET WinForms. For future releases, all new controls will be developed in Windows Presentation Foundation (WPF) and Silverlight. The decision to change our technology focus is based on feedback we have received from healthcare ISVs, who are keen to start working with these new technologies.
        </p>
        <p>
          It is important to emphasise that the move to new technologies doesn't make the controls we have produced so far obsolete. Whether you are building a brand new application, or incorporating the Toolkit controls into an existing one, the new technologies are designed to integrate seamlessly with ASP.NET and WinForms, so any investment you have already made has not been wasted. WPF controls can be hosted in WinForms applications using the
	<a target="_blank" href="http://msdn2.microsoft.com/en-us/library/system.windows.forms.integration.elementhost.aspx" title=".NET ElementHost control (New Window)">.NET ElementHost control</a>, and Silverlight controls are designed to be used within ASP.NET web pages alongside ASP.NET AJAX controls.
        </p>
	<p>
	Before you can use the toolkit you will need to install a number of prerequisites. These are described in more detail in the ReadMe document that can be downloaded from our <a href="http://www.codeplex.com/mscui" title="Link to CodePlex site (New Window)" target="_blank">CodePlex</a> site.
	<img src="../Images/SFTheme/arch.jpg" title="'Architecture Diagram' Image" alt="This diagram shows a hierarchy consisting of Controls at the top, Framework underneath, and then supporting technologies (e.g. ASP.NET AJAX) at the bottom. In the Controls area, there are 2 lists ? Basic and Specific. In the Framework area, there are also 2 lists ? Integration and Guidance. In both Controls and Frameworks areas there are arrows indicating future items." />
	</p>
    </div>
</asp:Content>
