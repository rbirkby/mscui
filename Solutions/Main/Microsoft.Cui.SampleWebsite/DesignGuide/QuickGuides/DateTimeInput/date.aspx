<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="date.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.DatePage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <div>
            <p>
            The entry of date information is a fundamental function within 
            clinical and administrative processes. Therefore, healthcare professionals 
            should have a clear, quick mechanism for entry of an accurate date  
            value, and be assisted to disambiguate entries when necessary.
            </p>
            <p>
            The basic date input control should comprise:
            </p>
            <ul>
            	<li>A free text input area</li>
                <li>A calendar control</li>
                <li>A default input, dependent on the context in which the control is used</li>
                <li>A facility to disambiguate the date entered</li>
            </ul>
            <img src="images/dtcdate.png" alt="A diagram of a date control with callouts indicating: the free text input area; a default input; a calendar control" />
            <p>
            Free text date input should be possible as fully specified dates, 
            partially specified dates and arithmetic shortcuts.
            </p>
            <p>
            The entry of partially specified dates will not be appropriate for 
            every situation. Therefore, application designers should decide what 
            types of dates are allowed for a given situation, based on the specific 
            clinical context.
            </p>
            </div>
        </div>
    </div>
</asp:Content>
