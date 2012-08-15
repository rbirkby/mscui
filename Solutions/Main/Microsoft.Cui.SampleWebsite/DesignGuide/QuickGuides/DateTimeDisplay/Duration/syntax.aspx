<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="syntax.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Duration.Syntax" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="pagenav">
		</div>
		<div id="guidance">			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0018.png" alt="4hr 32min 16sec" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0018</p>
					<p>
					Use whole numbers for time duration, for example, 1, 5 and 60.  
					Do not use decimals or fractions, for example, 0.5, 1.5, &#190;
					</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtb0029.png" alt="&#39;5y 5m 5d 5min 55sec&#39;: &#39;Years, months, days, minutes, seconds&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0029</p>
					<p>Display time duration units in decreasing order of significance</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
		</div>
	</div>
</asp:Content>