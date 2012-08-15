<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="syntax.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Time.Syntax" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
	<div id="Div1">
		<div id="pagenav">
		</div>
		<div id="guidance">			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0002.png" alt="An example time: &#39;04:28&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0002</p>
					<p>Display an exact time as HH:mm</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0003.png" alt="Two times: &#39;09:43:22&#39; with an arrow pointing at &#39;09&#39; and &#39;15:55:03&#39; with an arrow pointing at &#39;15&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0003</p>
					<p>Display <strong>hours</strong> using two digits
                    (values less than 10 should appear with a zero in the first position)</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0004.png" alt="Two times: &#39;09:43:22&#39; with an arrow pointing at &#39;43&#39; and &#39;15:55:03&#39; with an arrow pointing at &#39;55&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0004</p>
					<p>Display <strong>minutes</strong> using two digits
                    (values less than 10 should appear with a zero in the first position)</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtb0005.png" alt="Two times: &#39;09:43:22&#39; with an arrow pointing at &#39;22&#39; and &#39;15:55:03&#39; with an arrow pointing at &#39;03&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0005</p>
					<p>Display <strong>seconds</strong> as two digits
                    (values less than 10 should appear with a zero in the first position)</p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Tb-0012</p>
					<p>Display seconds only if required</p>
					<p class="recommended">Recommended</p>
					</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>