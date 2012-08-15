<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="order.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.LookAhead.Notifications.Order" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="illustration">
					<img src="../../images/medv049.png" alt="Outline diagram of a list of medications in which numbers are used to represent drugs in the look-ahead notifications. The top notification contains the numbers 1 to 5 (from left to right) with an arrow pointing right and the bottom notification contains the numbers 12 to 16 (from right to left) and an arrow pointing left" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-049</p>
					<p>
                    The order of both the items in the look-ahead 
                    notification and the medications list should 
                    always be the same
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-048</p>
					<p>
                    The look-ahead notification is populated from 
                    right to left such that the next drug in the list 
                    appears closest to the scroll bar
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			            
        </div>
    </div>
</asp:Content>
