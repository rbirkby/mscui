<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="suspended.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Status.Suspended" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="illustration">
					<img src="../images/medv166.png" alt="A column heading (Status) with a single cell below containing &#39;Suspended on &#91;new line&#93; 30-May-2008&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-166</p>
					<p>
                    Support a status of &#39;suspended&#39; 
                    and include medications with this 
                    status in current medications
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-169</p>
					<p>
                    Assign a status of &#39;Suspended&#39; 
                    to medications that are marked as not 
                    to be administered, but which are 
                    intended to be resumed at a later date
                    </p>
					<p class="recommended">Recommended</p>
					<p class="number">MEDv-024</p>
					<p>
                    Use visual design to draw 
                    attention to suspended medications
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
        </div>
    </div>
</asp:Content>
