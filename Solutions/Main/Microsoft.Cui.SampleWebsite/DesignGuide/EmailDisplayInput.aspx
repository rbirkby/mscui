<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="EmailDisplayInput.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.EmailDisplayInput"
    Title="Email Input and Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Email Input and Display.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Email Input and Display documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Email Input and Display (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
            	Introduction
        </h2>
	<p>	The <i>Design Guidance &ndash; Email Input and Display</i> document describes guidance and recommendations for the input and display of email 
		addresses in clinical applications. It enables the email address to be entered and displayed in a complete, clear and unambiguous manner, 
		while enhancing patient safety and clinical application usability, by:
	</p>
	<ul>
		<li>Providing an efficient mechanism for inputting email addresses</li>
		<li>Ensuring a consistent, clear and easily recognizable visual representation of email addresses</li>
		<li>Conforming to international standards and best practice</li>
	</ul>
    </div>
    <div class="last section">
        <h2>
            	Summary</h2> 
	<p>	The guidance describes:
	</p>
	<ul>
		<li>Input controls for entering email address elements</li>
		<li>The display of fully-specified email addresses, which comprises of a string of characters known 
		as a local-part, followed by an &lsquo;@&rsquo; symbol, and an Internet domain name</li>
		<li>The formatting of email addresses </li>
		<li>The display of email addresses as links</li>
		<li>The provision of labels, hints, prompts and tooltips</li>
	</ul>      
    </div>
</asp:Content>
