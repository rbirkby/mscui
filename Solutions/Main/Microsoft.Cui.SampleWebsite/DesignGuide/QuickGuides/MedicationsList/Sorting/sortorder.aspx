<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="sortorder.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Sorting.SortOrder" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="landscape">
					<img src="../images/medv104.png" alt="Diagram showing a list of three medications with callouts indicating the column heading (Start Date) and the sort order symbol (a triangle pointing downwards)" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-104</p>
					<p>
                    Use an icon or symbol in the column heading to 
                    indicate the column by which the data is sorted 
                    and the direction of the sort
                    </p>
					<p class="mandatory">Mandatory</p>
					</div>
					<div class="guidetext">
					<p class="number">MEDv-103</p>
					<p>
                    Use formatting of the column heading to clearly 
                    indicate the column to which the sort order is 
                    currently applied
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
                         
			<div class="line">
				<div class="illustration">
					<img src="../images/medv105.png" alt="A two step diagram showing two medications lists: 1. Sorted by &#39;Start Date&#39; 2. Sorted by &#39;Drug Details&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-105</p>
					<p>
                    When the sort order is changed from the default 
                    to another attribute in the Medications List View, 
                    retain the default as a secondary sort order
					<p class="mandatory">Mandatory</p>
				</div>
			</div>           
        </div>
    </div>      
</asp:Content>
