<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="recentpast.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.CurrentPast.CurrentMeds.RecentPast" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">

			<div class="line">
				<div class="landscape">
					<img src="../../images/medv074.png" alt="Column headings and rows from a list of medications in which the last row is a recent past notification containing the text &#39;2 past medications were completed or discontinued in the last 48 hours&#39;" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-074</p>
					<p>
                    When displaying current medications, display 
                    a notification for medications that have been 
                    completed or discontinued within a specified 
                    time interval from the current time
                    </p>
					<p class="mandatory">Mandatory</p>
					</div>
					<div class="guidetext">
					<p class="number">MEDv-174</p>
					<p>
                    Use formatting to distinguish the recent 
                    past notifications from medications in the list
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>	
     
        </div>
    </div>
</asp:Content>
