<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="approx.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Time.Approx" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">		
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0014.png" alt="Approx 02:00" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0014</p>
					<p>
					Precede the display of an approximate time value with the word 'Approx' 
					</p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Tb-0015</p>
					<p>
					Display the time value using the guidance for exact time
					</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtb0016.png" alt="&#39;Approx 13:00&#39; with an arrow pointing at the space between the word &#39;Approx&#39; and the time" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0016</p>
					<p>
					Leave a white space between the 'Approx' and the HH element of the time display 
					</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>
