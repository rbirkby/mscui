<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="shortdate.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Date.ShortDatePage" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="pagenav">
		</div>
		<div id="guidance">			
			<div class="line">
				<div class="illustration">
					<img src="../images/dta0006.png" alt="Three examples of short dates: &#39;01-Jan-1999&#39;, &#39;Tue 05-Jan-2009&#39; and &#39;14-Aug-1980&#39;, plus an example showing the date format &#39;DD-Mmm-YYYY&#39; with arrows pointing to the two hyphens." />
				</div>
				<div class="guidetext">
					<p class="number">D+Ta-0006</p>
					<p>
					Display dates using the short date format in all instances of clinical 
					usage affecting patient treatment, including patient identification
					</p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Ta-0010</p>
					<p>When using the short date format, ignore the user's regional settings</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
		</div>
	</div>
</asp:Content>