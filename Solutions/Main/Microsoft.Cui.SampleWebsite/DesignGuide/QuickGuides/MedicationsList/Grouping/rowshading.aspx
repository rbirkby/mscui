<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="rowshading.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Grouping.RowShading" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv192.png" alt="Outline diagram of a grouped list with alternate row shading such that the first row in each group is unshaded" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-192</p>
					<p>
                    Re-start alternate row shading at the beginning 
                    of each group. (Alternate row shading is not needed 
                    if there is only one medication in each group)
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/medv193.png" alt="Outline diagram of a grouped list with a group heading at the top of the view and many rows below" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-193</p>
					<p>
                    When a grouping is selected in the grouping control, 
                    ensure that at least one group heading is visible 
                    in the newly grouped list
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>      
</asp:Content>
