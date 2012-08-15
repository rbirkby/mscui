<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="controls.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.CurrentPast.ControlsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv063.png" alt="Two buttons in a row, the first labelled &#39;Current&#39; (shown in a selected state) and the second labelled &#39;Past&#39; (which has a drop-down control)" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-063</p>
					<p>
                    Provide buttons for displaying current and past 
                    medications respectively in the Medications List 
                    View and label the buttons &#39;Current&#39; and 
                    &#39;Past&#39;
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv065.png" alt="Two buttons in a row, the first labelled &#39;Current&#39; (shown in a selected state) and the second labelled &#39;Past&#39; (which has a drop-down control). A callout indicating both buttons is labelled &#39;one is selected&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-065</p>
					<p>
                    Do not allow <strong>Current</strong> and <strong>Past</strong> 
                    buttons to be selected simultaneously
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-174</p>
					<p>
                    Ensure that either the <strong>Current</strong> 
                    or the <strong>Past</strong> button is selected at any one time
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

			<div class="line">
				<div class="illustration">
					<img src="../images/medv066.png" alt="Two buttons in a row, the first labelled &#39;Current&#39; and the second labelled &#39;Past&#39; is shown in a selected state with a drop-down list in which &#39;Past week&#39; is highlighted. The first entry in the drop-down list is &#39;Show All Past&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-066</p>
					<p>
                    Supplement the <strong>Past</strong> button in the Medications 
                    List View with a <span class="nowrap">drop-down</span> control for displaying, 
                    selecting and applying a filter on the past 
                    medications view
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-067</p>
					<p>
                    Include an option for displaying all past 
                    medications in the <span class="nowrap">drop-down</span> control
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
 
        </div>
    </div>
</asp:Content>
