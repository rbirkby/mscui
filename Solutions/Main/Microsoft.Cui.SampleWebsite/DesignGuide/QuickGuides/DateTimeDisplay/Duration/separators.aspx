<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="separators.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Duration.Separators" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">
	
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtb0027.png" alt="&#39;8hrs 5mins&#39; with arrows pointing to just after each digit but before each unit" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0027</p>
					<p>
					Display duration values and their respective units 
					as pairs, with no intervening whitespace between the value and unit
					</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtb0028.png" alt="&#39;8hrs 5mins&#39;, with an arrow pointing to the space between &#39;8hrs&#39; and &#39;5mins&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0028</p>
					<p>
					Use a white space as the separator when displaying a 
					duration composed of more than one unit
					</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>