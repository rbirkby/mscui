<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="24hrclock.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Time.Clock24Hr" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">

			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0001.png" alt="Example date:&#39;23:54&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0001</p>
					<p>Display time using the 24-hour clock only</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtb0032.png" alt="Example date:&#39;04:28&#39; with mouse cursor and tooltip in which the text &#39;24-hour clock&#39; is displayed." />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0032</p>
					<p>Provide indication to the user that the 24-hour clock is in use</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

		</div>
	</div>
</asp:Content>