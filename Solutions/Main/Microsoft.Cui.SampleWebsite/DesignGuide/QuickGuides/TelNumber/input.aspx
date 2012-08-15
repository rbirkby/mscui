<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="input.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.TelNumber.InputPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            Telephone number input can be supported by one of two designs. 
            Both designs will accept and correctly identify both UK and non-UK numbers.
            </p>
            <ol>
            	<li>
                A single unassisted entry input box, optimised for UK telephone number 
                entry. This design should be used when it is anticipated that the majority 
                of telephone numbers input will be UK numbers.<br />
                <img src="images/tidunassisted.png" alt="A text input box labelled &#39;Telephone Number&#39; and containing the prompt &#39;e.g. 01234 567890&#39;" />
                </li>
                <li>
                A single entry input box with country selector assistance, 
                optimised for non-UK telephone numbers.<br />
                <img src="images/tidassisted.png" alt="Two input controls side by side: a drop-down list control labelled &#39;Country Code&#39; with the text &#39;+44 (UK)&#39; displayed; a text input box labelled &#39;Telephone Number&#39; showing the prompt text &#39;e.g. 01234 567890&#39;" />
                </li>
         	</ol>
         </div>
    </div>
</asp:Content>