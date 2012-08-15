<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="notifications.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.LookAhead.NotificationsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv044.png" alt="An illustration of a look-ahead notification containing the text &#39;aspirin &#149; paracetamol&#39; and an arrow indicating the height of the notification" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-044</p>
					<p>
                    Restrict the look-ahead notifications 
                    to a single line each
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv043.png" alt="Illustration of the top right hand corner of a list of medications with callouts indicating the notification and the up arrow of the scroll bar" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-043</p>
					<p>
                    The look-ahead notifications should be clearly 
                    joined to the &#39;up&#39; and &#39;down&#39; 
                    arrow controls of the scroll bar respectively
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
			<div class="line">
				<div class="illustration">
					<img src="../images/medv045.png" alt="Illustration of the left hand side of a list of medications with two callouts, one indicating the look-ahead notification and one indicating the medications below" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-045</p>
					<p>
                    Do not place controls or other notifications such 
                    that they separate the look-ahead notification from 
                    the medications in the Medications List View
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			            
        </div>
    </div>   
</asp:Content>
