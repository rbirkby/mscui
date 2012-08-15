<%@ Page Title="" Language="C#" MasterPageFile="~/QIGs.Master" AutoEventWireup="true" CodeBehind="instructionaltext.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.UKInput.InstructionalText" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
<div id="page">
        <div id="overview">    
				<p>
				Though the guidance does not define it, the input control should provide 
				instructional text to guide input. This could be by using hints (text 
				outside the text input box), prompts (watermarks inside the text input 
				box), or tool tips.
				</p>
					<img src="../../images/adr-instructions1.png" alt="Two input fields with instructional text to the right of the text input box" />
					<img src="../../images/adr-instructions2.png" alt="Two input fields with instructional text within the text input box" />
					<img src="../../images/adr-instructions3.png" alt="Two input fields, one with a mouse pointer (an arrow) and instructional text displayed within a tooltip" />
				</div>
			</div>
        </div>
    </div>
</asp:Content>