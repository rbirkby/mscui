<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="interaction.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.LookAhead.Interaction" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv058.png" alt="Outline diagram emphasising the look-ahead scroll bar in which the top notification has an arrow pointing left, the scroll bar has an arrow pointing down and the bottom notification has an arrow pointing right" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-058</p>
					<p>
                    Update the look-ahead notifications 
                    dynamically in response to scrolling
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv059.png" alt="Illustration of the top right hand corner of a list of medications with an arrow pointing left from the left edge of the notification" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-059</p>
					<p>
                    Allow the look-ahead notification to change 
                    width dynamically to accommodate its contents 
                    up to the available width
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
			<div class="line">
				<div class="illustration">
					<img src="../images/medv182.png" alt="Illustration of the top right hand corner of a list of medications in which a mouse cursor (pointer) is displayed over the look-ahead notification" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-182</p>
					<p>
                    Do not allow the look-ahead notification to 
                    be used for navigation by clicking on areas 
                    of the notification, such as drug names or 
                    counts
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			            
        </div>
    </div>
</asp:Content>
