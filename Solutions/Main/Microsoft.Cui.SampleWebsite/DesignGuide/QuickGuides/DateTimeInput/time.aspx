<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="time.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.TimePage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <div>
            <p>
            The entry of date and time information is a fundamental function within 
            clinical and administrative processes. Therefore, healthcare professionals 
            should have a clear, quick mechanism for entry of an accurate time 
            value, and be assisted to disambiguate entries when necessary.
            </p>
            <p>
            The basic time input control should comprise:
            </p>
            <ul>
            	<li>An input area</li>
                <li>A default input dependent on the context in which the control is used</li>
                <li>A &#39;spin&#39; control</li>
                <li>A tick box to indicate if the time is approximate (where supported by the 
                specific clinical application)</li>
            </ul>
            <img src="images/dtctime.png" alt="A diagram of a time input control with callouts labelling: a default input; an input area; a spin control; a tick box to indicate if the time is approximate" />
            <p>
            The time input control can be used to enter fully specified times, 
            approximate times and arithmetic shortcuts.  In addition the &#39;seconds&#39; 
            element of time can be entered if required. The inclusion of seconds is at 
            the discretion of the clinical application.
            </p>
            </div>
        </div>
    </div>
</asp:Content>