<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="contextmenu.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Selection.ContextMenu" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
			<div class="midline">
				<div class="landscape">
					<img src="../images/medv128.png" alt="Diagram of a list of three medications with two rows selected and a context menu displayed" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-128</p>
					<p>
                    Support the display of a context menu for selected 
                    medications in the Medications List View (for 
                    example, by right-clicking)
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-129</p>
					<p>
                    In the context menu, provide appropriate actions and options
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-130</p>
					<p>
                    In the context menu, support actions with icons where 
                    appropriate
                    </p>
					<p class="recommended">Recommended</p>
					<p class="number">MEDv-131</p>
					<p>
                    In the context menu, grey out actions that are unavailable 
                    or disallowed for one or more of the current selections
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv133.png" alt="Diagram of a context menu split into two sections. The first section, labelled &#39;information&#39; contains the list item &#39;Details...&#39;. The second section labelled &#39;actions&#39; contains list items &#39;Suspend&#39; and &#39;Edit&#39; (greyed out)" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-133</p>
					<p>
                    In the context menu, group similar options so that direct 
                    actions, actions that permit addition of information, and 
                    actions that display more information, are each grouped 
                    together
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
			<div class="line">
				<div class="illustration">
					<img src="../images/medv136.png" alt="Diagram of a context menu in which a callout labelled &#39;frequently used action&#39; indicates the first list item (Details...)" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-132</p>
					<p>
                    In the context menu, define a consistent and static order 
                    of menu items in which frequently used actions are prioritised 
                    by placing them higher in the list
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-135</p>
					<p>
                    In the context menu for selections in the Medications 
                    List View, provide an option for displaying all details 
                    for the selected medication
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-136</p>
					<p>
                    Include an option to access all details for 
                    one medication in the context menu
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			            
        </div>
    </div>   
</asp:Content>
