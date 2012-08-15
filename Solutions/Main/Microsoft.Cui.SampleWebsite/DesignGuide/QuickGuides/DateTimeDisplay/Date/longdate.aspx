<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="longdate.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Date.LongDatePage" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">			
            
			<div class="line">
				<div class="illustration">
					<img src="../images/dta0011.png" alt="Three examples of long dates: &#39;01 January 1999&#39;, &#39;5th January 2009&#39; and &#39;14 August 1980&#39;, plus an example showing the date format &#39;DD-Month-YYYY&#39;." />
				</div>
				<div class="guidetext">
					<p class="number">D+Ta-0011</p>
					<p>Use the long date format when communicating with the patient</p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Ta-0014</p>
					<p>
					When using the long date format, follow the user's default regional settings 
					ignoring any changes made by the user to these default settings
					</p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Ta-0015</p>
					<p>Use the long date format when interacting with screen readers</p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>