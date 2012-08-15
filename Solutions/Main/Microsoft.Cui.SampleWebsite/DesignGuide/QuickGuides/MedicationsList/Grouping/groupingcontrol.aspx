<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="groupingcontrol.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Grouping.GroupingControl" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="illustration">
					<img src="../images/medv084.png" alt="A grouping control with &#39;Status&#39; selected in a drop-down list that has &#39;None&#39; at the top" />
				</div>
				<div class="guide">
					<p class="number">MEDv-084</p>
					<p>
                    Provide a standard drop-down list for displaying, 
                    selecting and applying grouping to the medications list
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-085</p>
					<p>
                    Label the grouping control &#39;Group by&#39;
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-190</p>
					<p>
                    Include an option in the 
                    <span class="nowrap">drop-down</span> 
                    list to set the grouping to &#39;None&#39;
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>    
</asp:Content>
