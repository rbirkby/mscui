<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="counts.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.LookAhead.Notifications.Counts" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="illustration">
					<img src="../../images/medv052.png" alt="Illustration of the bottom left of a list of medications in which the text &#39;4 more&#39; is visible in the look-ahead notification" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-052</p>
					<p>
                    When there are more items than can be displayed 
                    in the <span class="nowrap">look-ahead</span> notification for current medications, 
                    display as many as possible and end the list with a 
                    count of the remaining items that could not be displayed
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
			<div class="midline">
				<div class="illustration">
					<img src="../../images/medv053.png" alt="Illustration of the bottom left hand corner of a list of medications in which a yellow triangle is displayed in the look-ahead notification before the text &#39;4 more &#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-053</p>
					<p>
                    When a count is displayed in a <span class="nowrap">
                    look-ahead</span> notification 
                    and one or more of the medications included in that count 
                    have decision support alerts, display a decision support 
                    alert icon next to the count
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../../images/medv050.png" alt="Illustration of the top right hand corner of a list of medications with a look-ahead notification that contains the text &#39;1 more&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-050</p>
					<p>
                    When exceptionally long drug names require more space 
                    than is available in the <span class="nowrap">look-ahead</span> 
                    notification, 
                    display a count instead (as for past medications)
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

			            
        </div>
    </div>
</asp:Content>
