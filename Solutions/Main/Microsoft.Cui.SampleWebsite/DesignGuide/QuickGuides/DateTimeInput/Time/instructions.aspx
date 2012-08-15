<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="instructions.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Time.Instructions" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">
	
			<div class="line">
				<div class="illustration">
					<img src="../images/dtc0024.png" alt="A time input control in which the text &#39;hh:mm:22&#39; is displayed and a mouse cursor with tooltip containing instructions for using the control" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0024</p>
					<p>
                    Provide access to instructions on how to use the control, 
                    for example, via the use of tooltips.  The instructions 
                    must contain details of different time types that can be 
                    entered
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Tc-0043</p>
					<p>
                    Provide indication to the user that the 24-hour clock is in use
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>
