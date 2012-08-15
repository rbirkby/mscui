<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="calendar.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Date.CalendarPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">		

			<div class="midline">
				<div class="illustration">
					<img src="../images/dtc0002-1.png" alt="A date control with a calendar displayed as a drop-down" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0004</p>
					<p>
                    Include the calendar icon within the boundary of the date input field
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

			<div class="line">
				<div class="illustration">
					<img src="../images/dtc0010.png" alt="A two step diagram showing: 1. A date control with a mouse cursor over the calendar control; 2. A calendar displayed as a drop-down from the date control" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0010</p>
					<p>
                    Provide access to the calendar control via a calendar icon
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Tc-0017</p>
					<p>
                    Allow the calendar to be closed either when the user clicks 
                    away from the calendar or clicks on the calendar icon
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>
