<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="currentpaststatus.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Status.CurrentPastStatus" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv025.png" alt="Rows and column headings from a list of medications each with &#39;Started&#39; in the Status column" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-172</p>
					<p>
                    Define medications with a status of either &#39;Started&#39;, 
                    &#39;Not Started&#39; or &#39;Suspended&#39; as current 
                    medications
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>

			<div class="line">
				<div class="landscape">
					<img src="../images/medv025b.png" alt="Column headings and three rows from a list of past medications with the status column showing: Completed, Discontinued, Completed" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-173</p>
					<p>
                    Define medications with a status of either &#39;Completed&#39; 
                    or &#39;Discontinued&#39; as past medications
                    </p>
					<p class="recommended">Recommended</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-025</p>
					<p>
                    Use visual design to distinguish a list of 
                    current medications from a list of past medications
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
