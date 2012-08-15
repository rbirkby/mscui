<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="midnight.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Time.Midnight" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">		
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0009.png" alt="&#39;00:00&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0009</p>
					<p>Display midnight as 00:00</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0010.png" alt="&#39;23:59&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0010</p>
					<p>Display the last minute in the day as 23:59</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtb0011.png" alt="Two boxes displaying examples of null times: &#39;Unknown&#39; and &#39;Not recorded&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0011</p>
					<p>Display null times using an appropriate value, 
                    for example, 'Unknown' and 'Not recorded'</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>