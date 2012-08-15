<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="contents.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.LookAhead.Notifications.Contents" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
			<div class="line">
				<div class="landscape">
					<img src="../../images/medv183.png" alt="Illustration of the lower half of a list of medications in which the look-ahead notification obscures the text of a medication line and contains the names of seven drugs plus the text &#39;4 more&#39;" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-183</p>
					<p>
                    If any of the drug name text (other than the letter 
                    ascenders and descenders) is obscured by the boundaries 
                    of the list, include that drug in the look-ahead notification
                    </p>
					<p class="recommended">Recommended</p>
					</div>
					<div class="guidetext">
					<p class="number">MEDv-051</p>
					<p>
                    Do not truncate or abbreviate drug names 
                    in the look-ahead notification
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			            
        </div>
    </div>    
</asp:Content>
