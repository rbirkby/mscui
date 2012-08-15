<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="approx.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Duration.Approx" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="pagenav">
		</div>
		<div id="guidance">			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0030.png" alt="Approx 2hr 40min" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0030</p>
					<p>
					Precede the display of an approximate 
					duration value with the word 'Approx'
					</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtb0031.png" alt="&#39;Approx 2hr 40min&#39;, with an arrow pointing at the space between &#39;Approx&#39; and &#39;2hr 40min&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0031</p>
					<p>
					Leave a white space between the 'Approx' and the first 
					element of an approximate duration value
					</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
		</div>
	</div>
</asp:Content>