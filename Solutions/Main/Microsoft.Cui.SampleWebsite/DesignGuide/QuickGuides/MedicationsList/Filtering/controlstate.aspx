<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="controlstate.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Filtering.ControlState" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv069.png" alt="A list of past medications with buttons above labelled &#39;Current&#39; and &#39;Past&#39; and the &#39;Past&#39; button active.  The list starts with a pale yellow full width line containing the text &#39;The total list (54) is filtered to show: Past 2 months (3)&#39; followed by a button labelled &#39;Remove Filter&#39;" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-069</p>
					<p>
                    When a filter is applied to past medications in 
                    Medications List View, the <strong>Past</strong> button should 
                    indicate that it is currently selected
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-070</p>
					<p>
                    When a filter is applied to past medications in 
                    the Medications List View, display a filter 
                    notification at the top of the list below the 
                    column headings and above the scroll bar (thus 
                    &#39;pushing&#39; the list of medications down a line)
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>    
</asp:Content>
