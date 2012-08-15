<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="separators.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Time.Separators" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">		

			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0006.png" alt="Two example times, with an arrow pointing to the colon between the hours and the minutes" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0006</p>
					<p>Separate the hours and minutes with a colon</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0007.png" alt="Two example times, with an arrow pointing to the colon between the minutes and the seconds" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0007</p>
					<p>Separate the minutes and seconds with a colon</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtb0008.png" alt="&#39;Tue 05-Jan-2009 09:43:22&#39; with an arrow pointing at the space between the end of the date (the year) and the time" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0008</p>
					<p>Separate date and time values with a white space</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>