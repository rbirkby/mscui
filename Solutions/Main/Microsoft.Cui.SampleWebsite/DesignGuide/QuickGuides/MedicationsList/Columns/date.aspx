<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="date.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Columns.Date" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
			<div class="line">
				<div class="landscape">
					<img src="../images/medv153.png" alt="Extract from a Medications List View with callouts labelled &#39;fixed width&#39; indicating the last two columns (Start Date and End Date)" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-151</p>
					<p>
                    When an end date column is displayed, place a start date column 
                    before (to the left of) the end date column
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-152</p>
					<p>
                    When an end date column is displayed, and there is no duration 
                    column, place a start date column adjacent to the end date column
                    </p>
					<p class="recommended">Recommended</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-153</p>
					<p>
                    Use fixed width columns for dates
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-154</p>
					<p>
                    Maintain consistent placement of date columns relative to one 
                    another and relative to the Drug Details column in both current 
                    and past medications
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
          
        </div>
    </div>
</asp:Content>
