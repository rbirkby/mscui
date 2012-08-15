<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="currentmeds.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.CurrentPast.CurrentMedsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv099.png" alt="Column headings and rows from a list of medications. The columns are: Drug Details, Status and Start Date (in that order). The column heading &#39;Start Date&#39; (furthest right) is shown in a selected state and includes a triangle pointing downwards" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-099</p>
					<p>
                    By default, present current medications sorted 
                    reverse chronologically by a starting date, such 
                    that the most recent is first (top) in the list
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-173</p>
					<p>
                    When displaying current medications, place the 
                    drug details in the first (furthest left) column
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
