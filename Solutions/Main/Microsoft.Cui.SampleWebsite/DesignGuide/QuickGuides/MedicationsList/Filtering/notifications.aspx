<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="notifications.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Filtering.Notifications" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv068.png" alt="An example of a notification in which the text is followed by a button labelled &#39;Remove Filter&#39; and a callout indicating the button" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-068</p>
					<p>
                    When a filter notification is displayed, include 
                    a control for removing the filter within that notification
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-071</p>
					<p>
                    Display a description of the filter in use within 
                    the filter notification in the Medications List View
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv072.png" alt="An example of a notification in which the following text is displayed: &#39;The total list (54) is filtered to show: Past 2 months (3)&#39;. Arrows label the two counts 1. (54) &#39;count of unfiltered total&#39; 2. (3) &#39;count of filtered total&#39;"  />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-072</p>
					<p>
                    Include a count of the number of medications 
                    displayed and a count of the total (unfiltered) 
                    number of past medications in a filter notification
                    </p>
					<p class="recommended">Recommended</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-176</p>
					<p>
                    Clearly label the counts (number of medications 
                    displayed and total unfiltered number) with text 
                    that allows them to be differentiated
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>            
        </div>
    </div>
</asp:Content>
