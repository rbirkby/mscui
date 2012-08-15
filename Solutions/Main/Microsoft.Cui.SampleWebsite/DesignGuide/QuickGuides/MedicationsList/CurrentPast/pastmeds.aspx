<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="pastmeds.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.CurrentPast.PastMeds" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv100.png" alt="Column headings and rows from a list of past medications. The columns are: Status, Drug Details, End Date. Two callouts are labelled &#39;First Column: Status&#39; and &#39;Second Column: Drug Details&#39; respectively. The last column &#39;End Date&#39; is shown in a selected state and includes a triangle pointing upwards" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-175</p>
					<p>
                    When displaying past medications, place 
                    the status column first (furthest left) 
                    and the Drug Details column second
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-100</p>
					<p>
                    By default, sort medications reverse 
                    chronologically by end date (or equivalent) 
                    such that the most recent is first (top) when 
                    the filter is set to &#39;Past&#39; in the 
                    Medications List View
					<p class="recommended">Recommended</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
