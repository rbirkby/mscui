<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="notificationcontents.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.CurrentPast.CurrentMeds.NotificationContents" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">   
         
    		<div class="midline">
				<div class="landscape">
					<img src="../../images/medv075.png" alt="A recent past notification containing the text &#39;2 past medications were completed or discontinued in the last 48 hours&#39;. A callout labelled &#39;Time interval&#39; points to the text &#39;in the last 48 hours&#39;" />
				</div>
				<div class="guidealone">
					<p class="number">MEDv-075</p>
					<p>
                    Clearly display the time interval 
                    within the recent past notification
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
 			<div class="line">
				<div class="landscape">
					<img src="../../images/medv077.png" alt="A recent past notification containing the text &#39;2 past medications were completed or discontinued in the last 48 hours&#39;. An arrow labelled &#39;count&#39; points to the number &#39;2&#39; in the text" />
				</div>
				<div class="guidealone">
					<p class="number">MEDv-077</p>
					<p>
                    Display a count of the number of 
                    recently past medications within the recent 
                    past notification in the medication list
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>	
     
        </div>
    </div>   
    
</asp:Content>
