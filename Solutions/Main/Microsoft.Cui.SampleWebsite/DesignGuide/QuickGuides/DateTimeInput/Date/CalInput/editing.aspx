<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="editing.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Date.CalInput.Editing" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">
			
			<div class="line">
				<div class="illustration">
					<img src="../../images/dtc0011.png" alt="A diagram of a calendar with callouts indicating where the month and year are displayed" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0011</p>
					<p>
                    Provide the ability to select a month independently, 
                    and a year independently. Signify the interactivity 
                    of these elements by suitable styling, for example as 
                    buttons or links, and ensure that they have descriptive 
                    tooltips
                    </p>
                    <p class="mandatory">Mandatory</p>
                    <p class="note">
                    <strong>Note</strong> A partially sepcified date can be entered using the calendar 
                    control by clicking either the month or the year
                    </p>
				</div>
			</div>

		</div>
	</div>
</asp:Content>
