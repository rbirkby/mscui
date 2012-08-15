<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="purpose.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.LookAhead.Purpose" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
    		<div class="line">
				<div class="landscape">
                    <img src="../images/lasb.png" alt="Diagram of a Medications List View with a look-ahead scroll bar. Callouts indicate: the look-ahead scroll bar, the two look-ahead notifications, the medications in the list and an alert in the lower look-ahead scroll bar notification" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-177</p>
					<p>
                    When displaying a list of current or past medications, 
                    and the scroll bar is active because the list is longer 
                    than the space available to display them, provide a 
                    clear indication that there are medications out of view
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-178</p>
					<p>
                    When displaying current medications, supplement the 
                    standard scroll bar with notifications that display 
                    the names of drugs that are out of view. This guide 
                    refers to this kind of scroll bar as a look-ahead 
                    scroll bar (LASB)
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
				
        </div>

     </div>   
</asp:Content>
